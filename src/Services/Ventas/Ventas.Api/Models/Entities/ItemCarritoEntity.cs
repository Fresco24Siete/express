using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ventas.Api.Models.Entities;

[Table("item_carrito")]
public class ItemCarritoEntity
{
    [Key]
    [Column("id_item_carrito")]
    public Guid IdItemCarrito { get; set; }

    [Column("id_carrito")]
    public long IdCarrito { get; set; }

    [Column("id_producto")]
    public Guid IdProducto { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; } = 1;

    [Column("precio_unitario", TypeName = "decimal(12,2)")]
    public decimal PrecioUnitario { get; set; }

    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [Column("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Relaciones
    [ForeignKey(nameof(IdCarrito))]
    public CarritoEntity? Carrito { get; set; }
}
