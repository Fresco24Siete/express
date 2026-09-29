using Catalogo.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.config;

public class CatalogoContext : DbContext
{   
    public DbSet<TiendaEntity> Tienda { get; set; }
    public DbSet<DireccionTiendaEntity> DireccionTienda { get; set; }
    public DbSet<CategoriaProductoEntity> CategoriaProducto { get; set; }
    public DbSet<ProductoEntity> Producto { get; set; }
    
    public CatalogoContext(
        DbContextOptions<CatalogoContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TiendaEntity>()
            .Property(t => t.EstadoCertificacion)
            .HasConversion<string>();

        modelBuilder.Entity<ProductoEntity>()
            .Property(p => p.Estado)
            .HasConversion<string>();
    }
}