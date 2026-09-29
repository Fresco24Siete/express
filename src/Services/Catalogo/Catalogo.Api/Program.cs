using Microsoft.EntityFrameworkCore;
using Identity.Api.config;
using Catalogo.Api.Interface;
using Catalogo.Api.Repositories;
using Catalogo.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CatalogoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection - Repositories & Services
builder.Services.AddScoped<IDireccionTiendaRepository, DireccionTiendaRepository>();
builder.Services.AddScoped<IDireccionTiendaService, DireccionTiendaService>();

builder.Services.AddScoped<ICategoriaProductoRepository, CategoriaProductoRepository>();
builder.Services.AddScoped<ICategoriaProductoService, CategoriaProductoService>();

builder.Services.AddScoped<ITiendaRepository, TiendaRepository>();
builder.Services.AddScoped<ITiendaService, TiendaService>();

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

