using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using Valkyrie.Entities;
using Valkyrie.Filters;
using Valkyrie.Models;

namespace Valkyrie.Extensions;

public static class UserAccountsEndpoints
{
    public static void MapAccountsEndpoints(this WebApplication app)
    {
        var accounts = app.MapGroup("accounts").RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });

        _ = accounts.MapPost("/signin", async (LoginModel loginModel) =>
        {
            var scope = app.Services.CreateScope();
            var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<IdentityUser>>();
            var username = loginModel.Username;
            var password = loginModel.Password;
            var result = await signInManager.PasswordSignInAsync(username!, password!, false, false);

            if (result.Succeeded)
            {
                return Results.Ok();
            }

            return Results.Problem(
                title: "Invalid login attempt",
                statusCode: StatusCodes.Status400BadRequest,
                extensions: new Dictionary<string, object?>
                {
            { "errors", new {messages = new List<string> { "Incorrect username or password" } } }
                });


        }).AllowAnonymous().AddEndpointFilter<ValidationFilter<LoginModel>>();

        _ = accounts.MapPost("/signout", async () =>
        {
            var scope = app.Services.CreateScope();
            var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<IdentityUser>>();

            await signInManager.SignOutAsync();

            return Results.Ok();
        });

        _ = accounts.MapPost("/manage/create", async (CreateUserModel userModel) =>
        {
            var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ValkDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var username = userModel.Username;
            var password = userModel.Password;
            var phoneNumber = userModel.PhoneNumber;
            var email = userModel.Email;

            // Check if username already exists
            if (await userManager.FindByNameAsync(username) != null)
            {
                return Results.Problem(
                title: "Unable to create user",
                statusCode: StatusCodes.Status409Conflict,
                extensions: new Dictionary<string, object?>
                {
                    { "errors", new {messages = new List<string> { "Username already exist" } } }
                });
            }

            else
            {
                var user = new IdentityUser 
                { 
                    UserName = username, 
                    Email = email, 
                    PhoneNumber = phoneNumber 
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    return Results.Ok(new { username, email, phoneNumber });
                }

                else
                {
                    return Results.BadRequest(result.Errors);
                }
            }

        }).AddEndpointFilter<ValidationFilter<CreateUserModel>>();

        _ = accounts.MapPost("/manage/change-password", async (ChangePasswordModel cpModel, HttpContext context) =>
        {
            var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var userId = context?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await userManager.FindByIdAsync(userId!);

            if (cpModel.NewPassword != cpModel.ConfirmedNewPassword)
            {
                return Results.Problem("Password does not match");
            }

            var result = await userManager.ChangePasswordAsync(user!, cpModel.CurrentPassword, cpModel.NewPassword);

            if (result.Succeeded)
            {
                return Results.Ok();
            }

            return Results.BadRequest(result.Errors);
        });

        _ = accounts.MapGet("/manage/info", async (HttpContext context) =>
        {
            var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var userId = context?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await userManager.FindByIdAsync(userId!);

            return Results.Ok(
                new
                {
                    username = user?.UserName,
                    email = user?.Email,
                    phoneNumber = user?.PhoneNumber
                });


        });
    }
}
