using Identity.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Data;

public class IdentidadContext : DbContext
{   
    public DbSet<UsuarioEntity> Usuarios { get; set; }
    public DbSet<DireccionEntity> Direccion { get; set; }
    public DbSet<DomiciliarioEntity> Domiciliario { get; set; }
    public DbSet<IntermediarioEntity> Intermediario { get; set; }
    public DbSet<ClienteEntity> Cliente { get; set; }
    public DbSet<EmprendedorEntity> Emprendedor { get; set; }
    public DbSet<AdministradorTiendaEntity> AdministradorTienda { get; set; }
    public IdentidadContext(DbContextOptions<IdentidadContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UsuarioEntity>()
            .Property(u => u.Estado)
            .HasConversion<string>();

    }
}