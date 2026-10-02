using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ventas.Api.Models.Dtos;

public record DetalleOrdenCreateDto
{
    [JsonPropertyName("id_orden")]
    public Guid? IdOrden { get; init; }

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

    [JsonPropertyName("subtotal")]
    public decimal? Subtotal { get; init; }
}

public record DetalleOrdenUpdateDto
{
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
    [JsonPropertyName("cantidad")]
    public int? Cantidad { get; init; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El precio unitario no puede ser negativo.")]
    [JsonPropertyName("precio_unitario")]
    public decimal? PrecioUnitario { get; init; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El subtotal no puede ser negativo.")]
    [JsonPropertyName("subtotal")]
    public decimal? Subtotal { get; init; }
}

public record DetalleOrdenResponseDto
{
    [JsonPropertyName("id_detalle_orden")]
    public Guid IdDetalleOrden { get; init; }

    [JsonPropertyName("id_orden")]
    public Guid IdOrden { get; init; }

    [JsonPropertyName("id_producto")]
    public Guid IdProducto { get; init; }

    [JsonPropertyName("cantidad")]
    public int Cantidad { get; init; }

    [JsonPropertyName("precio_unitario")]
    public decimal PrecioUnitario { get; init; }

    [JsonPropertyName("subtotal")]
    public decimal Subtotal { get; init; }
}
