using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Catalogo.Api.Models.Enums;

namespace Catalogo.Api.Models.Entities;

[Table("tienda")]
public class TiendaEntity
{
    [Key]
    [Column("id_tienda")]
    public Guid IdTienda { get; set; }

    [Column("id_administrador")]
    public Guid IdAdministrador { get; set; }

    [Column("id_direccion_tienda")]
    public long? IdDireccionTienda { get; set; }

    [Column("nombre")]
    [MaxLength(255)]
    public string Nombre { get; set; } = null!;

    [Column("nit")]
    [MaxLength(50)]
    public string Nit { get; set; } = null!;

    [Column("seguidores")]
    public int Seguidores { get; set; }

    [Column("categoria_principal")]
    [MaxLength(100)]
    public string CategoriaPrincipal { get; set; } = null!;

    [Column("estado_certificacion")]
    public EstadoCertificacionEnum EstadoCertificacion { get; set; } = EstadoCertificacionEnum.Pendiente;

    [Column("cantidad_productos")]
    public int CantidadProductos { get; set; }

    [Column("terminos_aceptados")]
    public bool TerminosAceptados { get; set; }

    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [Column("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Navigation properties

}
