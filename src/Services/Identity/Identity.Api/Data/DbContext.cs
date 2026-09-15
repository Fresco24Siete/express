using Identity.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Identity.Api.Data;

public class IdentidadContext : DbContext
{   
    public DbSet<UsuarioEntity> Usuarios {get; set;}
    public IdentidadContext(DbContextOptions<IdentidadContext> options) : base(options)
    {
    }
}