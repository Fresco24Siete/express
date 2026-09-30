using System.Text.Json.Serialization;
using Asignacion.Api.Config;
using Asignacion.Api.Interfaces;
using Asignacion.Api.Repositories;
using Asignacion.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- Base de datos ---
builder.Services.AddDbContext<AsignacionContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- Inyección de dependencias ---
builder.Services.AddScoped<IAsignacionEnvioRepository, AsignacionEnvioRepository>();
builder.Services.AddScoped<IAsignacionEnvioService, AsignacionEnvioService>();

// --- Controladores y serialización JSON ---
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.MapControllers();

// --- Verificación opcional de conexión en arranque ---
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AsignacionContext>();
        if (await db.Database.CanConnectAsync())
        {
            logger.LogInformation("Conexión a PostgreSQL establecida con éxito para Asignacion.Api");
        }
        else
        {
            logger.LogWarning("No se pudo conectar a la base de datos PostgreSQL en el arranque");
        }
    }
    catch (Exception ex)
    {
        logger.LogWarning("Verificación de conexión a PostgreSQL omitida o falló: {Message}", ex.Message);
    }
}

app.Run();
