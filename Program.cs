using Valkyrie.Extensions;
using Valkyrie.Entities;
using Valkyrie.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddIdentityCore<IdentityUser>(opt => {
    opt.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ValkDbContext>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    SeedData.AddDevboards(app);

    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello world");
app.MapDevboardsEndpoints();
app.MapSBCsEndpoints();

app.Run();
