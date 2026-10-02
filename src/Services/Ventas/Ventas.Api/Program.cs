using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Ventas.Api.Config;
using Ventas.Api.Interfaces;
using Ventas.Api.Repositories;
using Ventas.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// --- Base de datos PostgreSQL ---
builder.Services.AddDbContext<VentasContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- Inyección de dependencias (Repositories & Services) ---
builder.Services.AddScoped<ICarritoRepository, CarritoRepository>();
builder.Services.AddScoped<ICarritoService, CarritoService>();

builder.Services.AddScoped<IItemCarritoRepository, ItemCarritoRepository>();
builder.Services.AddScoped<IItemCarritoService, ItemCarritoService>();

builder.Services.AddScoped<IOrdenRepository, OrdenRepository>();
builder.Services.AddScoped<IOrdenService, OrdenService>();

builder.Services.AddScoped<IDetalleOrdenRepository, DetalleOrdenRepository>();
builder.Services.AddScoped<IDetalleOrdenService, DetalleOrdenService>();

builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<IPagoService, PagoService>();

builder.Services.AddScoped<IDevolucionRepository, DevolucionRepository>();
builder.Services.AddScoped<IDevolucionService, DevolucionService>();

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
        var db = scope.ServiceProvider.GetRequiredService<VentasContext>();
        if (await db.Database.CanConnectAsync())
        {
            logger.LogInformation("Conexión a PostgreSQL establecida con éxito para Ventas.Api");
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
