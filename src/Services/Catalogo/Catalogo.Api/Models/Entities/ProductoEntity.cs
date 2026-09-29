using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Catalogo.Api.Models.Enums;

namespace Catalogo.Api.Models.Entities;

[Table("producto")]
public class ProductoEntity
{
    [Key]
    [Column("id_producto")]
    public Guid IdProducto { get; set; }

    [Column("id_tienda")]
    public Guid IdTienda { get; set; }

    [Column("id_categoria_producto")]
    public long IdCategoriaProducto { get; set; }

    [Column("nombre")]
    [MaxLength(255)]
    public string Nombre { get; set; } = null!;

    [Column("slug")]
    [MaxLength(255)]
    public string Slug { get; set; } = null!;

    [Column("sku")]
    [MaxLength(100)]
    public string Sku { get; set; } = null!;

    [Column("descripcion", TypeName = "text")]
    public string? Descripcion { get; set; }

    [Column("cantidad_disponibles")]
    public int CantidadDisponibles { get; set; }

    [Column("cantidad_reservadas")]
    public int CantidadReservadas { get; set; }

    [Column("cantidad_vendidos")]
    public int CantidadVendidos { get; set; }

    [Column("precio", TypeName = "decimal(12,2)")]
    public decimal Precio { get; set; }

    [Column("precio_original", TypeName = "decimal(12,2)")]
    public decimal? PrecioOriginal { get; set; }

    [Column("descuento_porcentaje")]
    public int DescuentoPorcentaje { get; set; }

    [Column("peso_kg", TypeName = "decimal(8,2)")]
    public decimal? PesoKg { get; set; }

    [Column("imagen_principal")]
    [MaxLength(500)]
    public string? ImagenPrincipal { get; set; }

    [Column("estado")]
    public EstadoProductoEnum Estado { get; set; } = EstadoProductoEnum.Activo;

    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [Column("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;


}
