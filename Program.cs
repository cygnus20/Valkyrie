using FluentValidation;
using Valkyrie.Extensions;
using Valkyrie.Entities;
using Valkyrie.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Valkyrie.Validators;
using Valkyrie.Models;
using Valkyrie.Filters;
using Valkyrie;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining(typeof(LoginValidator));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMicroseconds(30);
        options.SlidingExpiration = true;
    });
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var postgresDbSettings = builder.Configuration.GetSection(nameof(PostgresDbSettings)).Get<PostgresDbSettings>();
var connectionString = postgresDbSettings?.ConnectionString;

builder.Services.AddDbContext<ValkDbContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt => {
    opt.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ValkDbContext>();
var app = builder.Build();

// Seed the admin user
await DbInitializer.Initialize(app);

if (app.Environment.IsDevelopment())
{
    SeedData.AddDevboards(app);

    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.ConfigureExceptionHandler());
app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/", () => "Hello world");
app.MapPost("/login", async (LoginModel loginModel) =>
{
    var userManager = app.Services.GetRequiredService<UserManager<IdentityUser>>();
    var username = loginModel.Username;
    var password = loginModel.Password;

    if (await userManager.FindByNameAsync(username!) != null)
    {

    }
}).AddEndpointFilter<ValidationFilter<LoginModel>>();
app.MapDevboardsEndpoints();
app.MapSBCsEndpoints();

app.Run();
