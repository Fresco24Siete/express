using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ventas.Api.Models.Dtos;

public record ItemCarritoCreateDto
{
    [Required(ErrorMessage = "El id_carrito es obligatorio.")]
    [JsonPropertyName("id_carrito")]
    public long IdCarrito { get; init; }

    [Required(ErrorMessage = "El id_producto es obligatorio.")]
    [JsonPropertyName("id_producto")]
    public Guid IdProducto { get; init; }

    [Required(ErrorMessage = "La cantidad es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
    [JsonPropertyName("cantidad")]
    public int Cantidad { get; init; } = 1;

    [Required(ErrorMessage = "El precio unitario es obligatorio.")]
    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El precio unitario no puede ser negativo.")]
    [JsonPropertyName("precio_unitario")]
    public decimal PrecioUnitario { get; init; }
}

public record ItemCarritoUpdateDto
{
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
    [JsonPropertyName("cantidad")]
    public int? Cantidad { get; init; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El precio unitario no puede ser negativo.")]
    [JsonPropertyName("precio_unitario")]
    public decimal? PrecioUnitario { get; init; }
}

public record ItemCarritoResponseDto
{
    [JsonPropertyName("id_item_carrito")]
    public Guid IdItemCarrito { get; init; }

    [JsonPropertyName("id_carrito")]
    public long IdCarrito { get; init; }

    [JsonPropertyName("id_producto")]
    public Guid IdProducto { get; init; }

    [JsonPropertyName("cantidad")]
    public int Cantidad { get; init; }

    [JsonPropertyName("precio_unitario")]
    public decimal PrecioUnitario { get; init; }

    [JsonPropertyName("subtotal")]
    public decimal Subtotal => Cantidad * PrecioUnitario;

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; init; }
}
