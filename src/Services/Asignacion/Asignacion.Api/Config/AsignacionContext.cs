using Asignacion.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Asignacion.Api.Config;

public class AsignacionContext : DbContext
{
    public DbSet<AsignacionEnvioEntity> AsignacionesEnvio { get; set; } = null!;

    public AsignacionContext(DbContextOptions<AsignacionContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AsignacionEnvioEntity>(entity =>
        {
            entity.Property(e => e.Estado)
                .HasConversion<string>();

            entity.Property(e => e.NumeroIntento)
                .HasDefaultValue(1);
        });
    }
}
