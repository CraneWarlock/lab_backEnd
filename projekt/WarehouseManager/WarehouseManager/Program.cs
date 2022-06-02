using System.Reflection;
using WarehouseManager;
using WarehouseManager.Entites;
using WarehouseManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<WarehauseManagerDbContext>();
builder.Services.AddScoped<dbSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<dbSeeder>();
    seeder.Seed();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
