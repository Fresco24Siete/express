using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Identity.Api.Models.Enums;

namespace Identity.Api.Models.Entities;

[Table("direccion")]
public class DireccionEntity
{
    [Key]
    [Column("id_direccion")]
    public Guid IdDireccion { get; set; }

    [Column("id_usuario")]
    public Guid IdUsuario { get; set; } 

    [Column("numero_calle")]
    [MaxLength(500)]
    public string NumeroCalle { get; set; } = null!;

    [Column("apto_suite_numero")]
    [MaxLength(50)]
    public string? AptoSuiteNumero { get; set; }

    [Column("tipo_domicilio")]
    public TipoDomicilioEnum TipoDomicilio { get; set; }

    [Column("ciudad")]
    [MaxLength(100)]
    public string Ciudad { get; set; } = null!;

    [Column("departamento")]
    [MaxLength(100)]
    public string Departamento { get; set; } = null!;

    [Column("pais")]
    [MaxLength(2)]
    public string Pais { get; set; } = "CO";

    [Column("codigo_postal")]
    [MaxLength(20)]
    public string? CodigoPostal { get; set; }

    [Column("latitud", TypeName = "decimal(9,6)")]
    public decimal? Latitud { get; set; }

    [Column("longitud", TypeName = "decimal(9,6)")]
    public decimal? Longitud { get; set; }

    [Column("referencia_adicional")]
    public string? ReferenciaAdicional { get; set; }

    [Column("validado_geolocalizacion")]
    public bool ValidadoGeolocalizacion { get; set; }

    [Column("is_predeterminada")]
    public bool IsPredeterminada { get; set; }

    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

}