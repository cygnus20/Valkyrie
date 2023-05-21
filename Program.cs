using Microsoft.EntityFrameworkCore;
using Valkyrie.Extensions;
using Valkyrie.Entities;
using Valkyrie.Settings;

var builder = WebApplication.CreateBuilder(args);
var postgresDbSettings = builder.Configuration.GetSection(nameof(PostgresDbSettings)).Get<PostgresDbSettings>();
var connectionString = postgresDbSettings?.ConnectionString;
builder.Services.AddDbContext<ValkDbContext>(opt => opt.UseNpgsql(connectionString));
//builder.Services.AddDbContext<ValkDbContext>(opt => opt.UseInMemoryDatabase("ValkDb"));
var app = builder.Build();
SeedData.AddDevboards(app);


app.MapGet("/", () => "Hello world");
app.MapDevboardsEndpoints();

app.Run();
