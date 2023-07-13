using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Valkyrie.Extensions;
using Valkyrie.Entities;
using Valkyrie.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var postgresDbSettings = builder.Configuration.GetSection(nameof(PostgresDbSettings)).Get<PostgresDbSettings>();
var connectionString = postgresDbSettings?.ConnectionString;

builder.Services.AddDbContext<ValkDbContext>(opt => opt.UseNpgsql(connectionString));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    SeedData.AddDevboards(app);

    app.UseSwagger();
    app.UseSwaggerUI();

}



app.MapGet("/", () => "Hello world");
app.MapDevboardsEndpoints();
app.MapSBCsEndpoints();

app.Run();
