using Microsoft.EntityFrameworkCore;
using Servicio.Api.Models.Entities;

namespace Servicio.Api.Config;

public class ServicioContext : DbContext
{
    public DbSet<CategoriaServicioEntity> CategoriasServicio { get; set; } = null!;
    public DbSet<ServicioEntity> Servicios { get; set; } = null!;

    public ServicioContext(DbContextOptions<ServicioContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- Categoria Servicio ---
        modelBuilder.Entity<CategoriaServicioEntity>(entity =>
        {
            entity.HasKey(c => c.IdCategoriaServicio);
            entity.Property(c => c.Nombre).HasMaxLength(100).IsRequired();
            entity.Property(c => c.PrecioBaseSugerido).HasPrecision(12, 2);
            entity.HasIndex(c => c.Activa);
        });

        // --- Servicio ---
        modelBuilder.Entity<ServicioEntity>(entity =>
        {
            entity.HasKey(s => s.IdServicio);
            entity.HasIndex(s => s.IdCliente);
            entity.HasIndex(s => s.IdEmprendedor);
            entity.HasIndex(s => s.IdCategoriaServicio);
            entity.HasIndex(s => s.Estado);
            entity.HasIndex(s => s.FechaEstimadaInicio);

            entity.Property(s => s.UbicacionLat).HasPrecision(9, 6);
            entity.Property(s => s.UbicacionLon).HasPrecision(9, 6);
            entity.Property(s => s.DuracionRealHoras).HasPrecision(4, 2);
            entity.Property(s => s.CostoBase).HasPrecision(12, 2);
            entity.Property(s => s.CostoAdicional).HasPrecision(12, 2);
            entity.Property(s => s.CostoTotal).HasPrecision(12, 2);

            entity.Property(s => s.Estado)
                  .HasConversion<string>();
        });
    }
}
