using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Servicio.Api.Config;
using Servicio.Api.Interfaces;
using Servicio.Api.Repositories;
using Servicio.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// --- Base de datos PostgreSQL ---
builder.Services.AddDbContext<ServicioContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- Inyección de dependencias (Repositories & Services) ---
builder.Services.AddScoped<ICategoriaServicioRepository, CategoriaServicioRepository>();
builder.Services.AddScoped<ICategoriaServicioService, CategoriaServicioService>();

builder.Services.AddScoped<IServicioRepository, ServicioRepository>();
builder.Services.AddScoped<IServicioService, ServicioService>();

// --- Controladores y serialización JSON ---
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// --- CORS ---
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
        var db = scope.ServiceProvider.GetRequiredService<ServicioContext>();
        if (await db.Database.CanConnectAsync())
        {
            logger.LogInformation("Conexión a PostgreSQL establecida con éxito para Servicio.Api");
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
