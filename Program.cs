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
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining(typeof(LoginValidator));
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMicroseconds(30);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
    });
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Dev Boards and Single Board Computer API",
        Description = "An API for presenting the specifications and features of microcontroller development boards and single board computers",
    });
});

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
app.UseHttpsRedirection();


app.MapGet("/", () => "Hello world");
app.MapAccountsEndpoints();
app.MapDevboardsEndpoints();
app.MapSBCsEndpoints();

app.Run();
