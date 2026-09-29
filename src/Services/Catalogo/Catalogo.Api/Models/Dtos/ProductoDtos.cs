using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Catalogo.Api.Models.Enums;

namespace Catalogo.Api.Models.Dtos;

public record ProductoCreateDto
{
    [Required]
    [JsonPropertyName("id_tienda")]
    public Guid IdTienda { get; init; }

    [Required]
    [JsonPropertyName("id_categoria_producto")]
    public long IdCategoriaProducto { get; init; }

    [Required]
    [MaxLength(255)]
    [JsonPropertyName("nombre")]
    public string Nombre { get; init; } = null!;

    [MaxLength(255)]
    [JsonPropertyName("slug")]
    public string? Slug { get; init; }

    [Required]
    [MaxLength(100)]
    [JsonPropertyName("sku")]
    public string Sku { get; init; } = null!;

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; init; }

    [Range(0, int.MaxValue)]
    [JsonPropertyName("cantidad_disponibles")]
    public int CantidadDisponibles { get; init; }

    [Range(0, (double)decimal.MaxValue)]
    [JsonPropertyName("precio")]
    public decimal Precio { get; init; }

    [Range(0, (double)decimal.MaxValue)]
    [JsonPropertyName("precio_original")]
    public decimal? PrecioOriginal { get; init; }

    [Range(0, 100)]
    [JsonPropertyName("descuento_porcentaje")]
    public int DescuentoPorcentaje { get; init; }

    [JsonPropertyName("peso_kg")]
    public decimal? PesoKg { get; init; }

    [MaxLength(500)]
    [JsonPropertyName("imagen_principal")]
    public string? ImagenPrincipal { get; init; }

    [JsonPropertyName("estado")]
    public EstadoProductoEnum Estado { get; init; } = EstadoProductoEnum.Activo;
}

public record ProductoUpdateDto
{
    [JsonPropertyName("id_producto")]
    public Guid? IdProducto { get; init; }

    [JsonPropertyName("id_tienda")]
    public Guid? IdTienda { get; init; }

    [JsonPropertyName("id_categoria_producto")]
    public long? IdCategoriaProducto { get; init; }

    [MaxLength(255)]
    [JsonPropertyName("nombre")]
    public string? Nombre { get; init; }

    [MaxLength(255)]
    [JsonPropertyName("slug")]
    public string? Slug { get; init; }

    [MaxLength(100)]
    [JsonPropertyName("sku")]
    public string? Sku { get; init; }

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; init; }

    [Range(0, int.MaxValue)]
    [JsonPropertyName("cantidad_disponibles")]
    public int? CantidadDisponibles { get; init; }

    [Range(0, int.MaxValue)]
    [JsonPropertyName("cantidad_reservadas")]
    public int? CantidadReservadas { get; init; }

    [Range(0, int.MaxValue)]
    [JsonPropertyName("cantidad_vendidos")]
    public int? CantidadVendidos { get; init; }

    [Range(0, (double)decimal.MaxValue)]
    [JsonPropertyName("precio")]
    public decimal? Precio { get; init; }

    [Range(0, (double)decimal.MaxValue)]
    [JsonPropertyName("precio_original")]
    public decimal? PrecioOriginal { get; init; }

    [Range(0, 100)]
    [JsonPropertyName("descuento_porcentaje")]
    public int? DescuentoPorcentaje { get; init; }

    [JsonPropertyName("peso_kg")]
    public decimal? PesoKg { get; init; }

    [MaxLength(500)]
    [JsonPropertyName("imagen_principal")]
    public string? ImagenPrincipal { get; init; }

    [JsonPropertyName("estado")]
    public EstadoProductoEnum? Estado { get; init; }
}

public record ProductoResponseDto
{
    [JsonPropertyName("id_producto")]
    public Guid IdProducto { get; init; }

    [JsonPropertyName("id_tienda")]
    public Guid IdTienda { get; init; }

    [JsonPropertyName("id_categoria_producto")]
    public long IdCategoriaProducto { get; init; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; init; } = null!;

    [JsonPropertyName("slug")]
    public string Slug { get; init; } = null!;

    [JsonPropertyName("sku")]
    public string Sku { get; init; } = null!;

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; init; }

    [JsonPropertyName("cantidad_disponibles")]
    public int CantidadDisponibles { get; init; }

    [JsonPropertyName("cantidad_reservadas")]
    public int CantidadReservadas { get; init; }

    [JsonPropertyName("cantidad_vendidos")]
    public int CantidadVendidos { get; init; }

    [JsonPropertyName("precio")]
    public decimal Precio { get; init; }

    [JsonPropertyName("precio_original")]
    public decimal? PrecioOriginal { get; init; }

    [JsonPropertyName("descuento_porcentaje")]
    public int DescuentoPorcentaje { get; init; }

    [JsonPropertyName("peso_kg")]
    public decimal? PesoKg { get; init; }

    [JsonPropertyName("imagen_principal")]
    public string? ImagenPrincipal { get; init; }

    [JsonPropertyName("estado")]
    public EstadoProductoEnum Estado { get; init; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; init; }
}
