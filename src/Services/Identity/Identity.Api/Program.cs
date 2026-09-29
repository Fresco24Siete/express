using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Identity.Api.Interfaces;
using Identity.Api.Repositories;
using Identity.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// --- Validacion del token ---
var jwtConfig = builder.Configuration.GetSection("Jwt");
var signingKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(jwtConfig["Key"]!));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false; // conserva los nombres originales (sub, role)
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidIssuer              = jwtConfig["Issuer"],
            ValidateAudience         = true,
            ValidAudience            = jwtConfig["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey         = signingKey,
            ValidateLifetime         = true,
            ClockSkew                = TimeSpan.Zero, // por defecto son 5 min de gracia
            NameClaimType            = "sub",
            RoleClaimType            = "role"
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddSingleton<TokenService>();


// --- Registro de servicios ---
builder.Services.AddDbContext<IdentidadContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseSnakeCaseNamingConvention());


// --- INYECCIONES DE DEPENDENCIAS ---

// Auth & Usuarios
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Direccion
builder.Services.AddScoped<IDireccionRepository, DireccionRepository>();
builder.Services.AddScoped<IDireccionService, DireccionService>();

// Domiciliario
builder.Services.AddScoped<IDomiciliarioRepository, DomiciliarioRepository>();
builder.Services.AddScoped<IDomiciliarioService, DomiciliarioService>();

// Intermediario 
builder.Services.AddScoped<IIntermediarioRepository, IntermediarioRepository>();
builder.Services.AddScoped<IIntermediarioService, IntermediarioService>();

// Cliente
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

// Emprendedor
builder.Services.AddScoped<IEmprendedorRepository, EmprendedorRepository>();
builder.Services.AddScoped<IEmprendedorService, EmprendedorService>();

// Administrador Tienda
builder.Services.AddScoped<IAdministradorTiendaRepository, AdministradorTiendaRepository>();
builder.Services.AddScoped<IAdministradorTiendaService, AdministradorTiendaService>();

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

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowFrontend");
app.UseAuthentication();  // ← el orden importa
app.UseAuthorization();


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


app.MapControllers();
app.Run();
