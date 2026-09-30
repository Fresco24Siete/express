using Microsoft.EntityFrameworkCore;
using Resena.Api.Models.Entities;

namespace Resena.Api.Config;

public class ResenaContext : DbContext
{   

    public DbSet<ResenaEntity> Resena {get; set;}
    public ResenaContext(
        DbContextOptions<ResenaContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ResenaEntity>()
            .Property(t => t.tipoActividadEnum)
            .HasConversion<string>();

        modelBuilder.Entity<ResenaEntity>()
            .Property(p => p.estadoEnum)
            .HasConversion<string>();
    }
}