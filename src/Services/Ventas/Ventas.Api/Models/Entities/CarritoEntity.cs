using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ventas.Api.Models.Entities;

[Table("carrito")]
public class CarritoEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_carrito")]
    public long IdCarrito { get; set; }

    [Column("precio_total", TypeName = "decimal(12,2)")]
    public decimal PrecioTotal { get; set; } = 0.00m;

    [Column("numero_articulos")]
    public int NumeroArticulos { get; set; } = 0;

    [Column("expirado_en")]
    public DateTimeOffset? ExpiradoEn { get; set; }

    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [Column("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Relaciones
    public ICollection<ItemCarritoEntity> Items { get; set; } = new List<ItemCarritoEntity>();
}
