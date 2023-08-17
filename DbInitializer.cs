using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Valkyrie.Entities;

namespace Valkyrie;

public static class DbInitializer
{
    public static async Task Initialize(WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ValkDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Ensure the database is created and all migrations are applied
        context.Database.Migrate();

        // Create the admin role if it doesn't exist
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // Create the user role if it doesn't exist
        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        // Create the admin user if it doesn't exist
        if (await userManager.FindByNameAsync("admin") == null)
        {
            var user = new IdentityUser { UserName = "admin", Email = "admin@example.com" };
            var result = await userManager.CreateAsync(user, "Admin@123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
