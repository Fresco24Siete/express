using Microsoft.EntityFrameworkCore;
using Ventas.Api.Models.Entities;

namespace Ventas.Api.Config;

public class VentasContext : DbContext
{
    public DbSet<CarritoEntity> Carritos { get; set; } = null!;
    public DbSet<ItemCarritoEntity> ItemsCarrito { get; set; } = null!;
    public DbSet<OrdenEntity> Ordenes { get; set; } = null!;
    public DbSet<DetalleOrdenEntity> DetallesOrden { get; set; } = null!;
    public DbSet<PagoEntity> Pagos { get; set; } = null!;
    public DbSet<DevolucionEntity> Devoluciones { get; set; } = null!;

    public VentasContext(DbContextOptions<VentasContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- Carrito ---
        modelBuilder.Entity<CarritoEntity>(entity =>
        {
            entity.HasKey(c => c.IdCarrito);
            entity.HasMany(c => c.Items)
                  .WithOne(i => i.Carrito)
                  .HasForeignKey(i => i.IdCarrito)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // --- Item Carrito ---
        modelBuilder.Entity<ItemCarritoEntity>(entity =>
        {
            entity.HasKey(i => i.IdItemCarrito);
            entity.HasIndex(i => i.IdCarrito);
            entity.HasIndex(i => i.IdProducto);
        });

        // --- Orden ---
        modelBuilder.Entity<OrdenEntity>(entity =>
        {
            entity.HasKey(o => o.IdOrden);
            entity.HasIndex(o => o.NumeroOrden).IsUnique();
            entity.HasIndex(o => o.IdUsuario);
            entity.HasIndex(o => o.Estado);

            entity.Property(o => o.Estado)
                  .HasConversion<string>();

            entity.HasOne(o => o.Carrito)
                  .WithMany()
                  .HasForeignKey(o => o.IdCarrito)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasMany(o => o.Detalles)
                  .WithOne(d => d.Orden)
                  .HasForeignKey(d => d.IdOrden)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(o => o.Pago)
                  .WithOne(p => p.Orden)
                  .HasForeignKey<PagoEntity>(p => p.IdOrden)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(o => o.Devoluciones)
                  .WithOne(dev => dev.Orden)
                  .HasForeignKey(dev => dev.IdOrden)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // --- Detalle Orden ---
        modelBuilder.Entity<DetalleOrdenEntity>(entity =>
        {
            entity.HasKey(d => d.IdDetalleOrden);
            entity.HasIndex(d => d.IdOrden);
            entity.HasIndex(d => d.IdProducto);

            entity.HasMany(d => d.Devoluciones)
                  .WithOne(dev => dev.DetalleOrden)
                  .HasForeignKey(dev => dev.IdDetalleOrden)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // --- Pago ---
        modelBuilder.Entity<PagoEntity>(entity =>
        {
            entity.HasKey(p => p.IdPago);
            entity.HasIndex(p => p.IdOrden).IsUnique();

            entity.Property(p => p.Metodo)
                  .HasConversion<string>();

            entity.Property(p => p.Estado)
                  .HasConversion<string>();
        });

        // --- Devolucion ---
        modelBuilder.Entity<DevolucionEntity>(entity =>
        {
            entity.HasKey(d => d.IdDevolucion);
            entity.HasIndex(d => d.IdOrden);
            entity.HasIndex(d => d.IdDetalleOrden);
            entity.HasIndex(d => d.Estado);

            entity.Property(d => d.Razon)
                  .HasConversion<string>();

            entity.Property(d => d.Estado)
                  .HasConversion<string>();
        });
    }
}
