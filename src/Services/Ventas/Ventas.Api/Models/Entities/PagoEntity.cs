using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ventas.Api.Models.Enums;

namespace Ventas.Api.Models.Entities;

[Table("pago")]
public class PagoEntity
{
    [Key]
    [Column("id_pago")]
    public Guid IdPago { get; set; }

    [Column("id_orden")]
    public Guid IdOrden { get; set; }

    [Column("monto", TypeName = "decimal(12,2)")]
    public decimal Monto { get; set; } = 0.00m;

    [MaxLength(3)]
    [Column("moneda")]
    public string Moneda { get; set; } = "COP";

    [Column("metodo")]
    public MetodoPagoEnum Metodo { get; set; } = MetodoPagoEnum.tarjeta_credito;

    [Column("estado")]
    public EstadoPagoEnum Estado { get; set; } = EstadoPagoEnum.pendiente;

    [MaxLength(255)]
    [Column("referencia_externo")]
    public string? ReferenciaExterno { get; set; }

    [MaxLength(500)]
    [Column("token_encriptado")]
    public string? TokenEncriptado { get; set; }

    [MaxLength(4)]
    [Column("ultimos_4_digitos")]
    public string? Ultimos4Digitos { get; set; }

    [Column("fecha_intento")]
    public DateTimeOffset FechaIntento { get; set; } = DateTimeOffset.UtcNow;

    [Column("fecha_completado")]
    public DateTimeOffset? FechaCompletado { get; set; }

    [Column("razon_fallo", TypeName = "text")]
    public string? RazonFallo { get; set; }

    [Column("created_by")]
    public Guid CreatedBy { get; set; }

    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [Column("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Relaciones
    [ForeignKey(nameof(IdOrden))]
    public OrdenEntity? Orden { get; set; }
}
