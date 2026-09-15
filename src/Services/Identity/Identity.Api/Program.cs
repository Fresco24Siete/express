using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using Identity.Api.Repositories.Interfaces;
using Identity.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- Registro de servicios ---
builder.Services.AddDbContext<IdentidadContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// --- Verificación de conexión al arrancar ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IdentidadContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    if (await db.Database.CanConnectAsync())
    {
        logger.LogInformation("Conexión a PostgreSQL establecida con éxito");
    }
    else
    {
        logger.LogError("No se pudo establecer conexión con PostgreSQL");
    }
}

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();


app.Run();
