using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalogo.Api.Models.Entities;

[Table("direccion_tienda")]
public class DireccionTiendaEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_direccion_tienda")]
    public long IdDireccionTienda { get; set; }

    [Column("numero_calle")]
    [MaxLength(500)]
    public string NumeroCalle { get; set; } = null!;

    [Column("ciudad")]
    [MaxLength(100)]
    public string Ciudad { get; set; } = null!;

    [Column("departamento")]
    [MaxLength(100)]
    public string Departamento { get; set; } = null!;

    [Column("codigo_postal")]
    [MaxLength(20)]
    public string? CodigoPostal { get; set; }

    [Column("latitud", TypeName = "decimal(9,6)")]
    public decimal? Latitud { get; set; }

    [Column("longitud", TypeName = "decimal(9,6)")]
    public decimal? Longitud { get; set; }

    [Column("numero_contacto")]
    [MaxLength(20)]
    public string? NumeroContacto { get; set; }


}
