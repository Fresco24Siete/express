using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ventas.Api.Models.Enums;

namespace Ventas.Api.Models.Entities;

[Table("devolucion")]
public class DevolucionEntity
{
    [Key]
    [Column("id_devolucion")]
    public Guid IdDevolucion { get; set; }

    [Column("id_orden")]
    public Guid IdOrden { get; set; }

    [Column("id_detalle_orden")]
    public Guid IdDetalleOrden { get; set; }

    [Column("cantidad_devuelta")]
    public int CantidadDevuelta { get; set; } = 1;

    [Column("razon")]
    public RazonDevolucionEnum Razon { get; set; } = RazonDevolucionEnum.defectuoso;

    [Column("estado")]
    public EstadoDevolucionEnum Estado { get; set; } = EstadoDevolucionEnum.solicitada;

    [Column("monto_reembolso", TypeName = "decimal(12,2)")]
    public decimal MontoReembolso { get; set; } = 0.00m;

    [Column("fecha_solicitud")]
    public DateTimeOffset FechaSolicitud { get; set; } = DateTimeOffset.UtcNow;

    [Column("fecha_resolucion")]
    public DateTimeOffset? FechaResolucion { get; set; }

    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [Column("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Relaciones
    [ForeignKey(nameof(IdOrden))]
    public OrdenEntity? Orden { get; set; }

    [ForeignKey(nameof(IdDetalleOrden))]
    public DetalleOrdenEntity? DetalleOrden { get; set; }
}
