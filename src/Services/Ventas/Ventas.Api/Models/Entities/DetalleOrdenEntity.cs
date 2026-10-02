using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ventas.Api.Models.Entities;

[Table("detalle_orden")]
public class DetalleOrdenEntity
{
    [Key]
    [Column("id_detalle_orden")]
    public Guid IdDetalleOrden { get; set; }

    [Column("id_orden")]
    public Guid IdOrden { get; set; }

    [Column("id_producto")]
    public Guid IdProducto { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; } = 1;

    [Column("precio_unitario", TypeName = "decimal(12,2)")]
    public decimal PrecioUnitario { get; set; } = 0.00m;

    [Column("subtotal", TypeName = "decimal(12,2)")]
    public decimal Subtotal { get; set; } = 0.00m;

    // Relaciones
    [ForeignKey(nameof(IdOrden))]
    public OrdenEntity? Orden { get; set; }

    public ICollection<DevolucionEntity> Devoluciones { get; set; } = new List<DevolucionEntity>();
}
