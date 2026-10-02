using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ventas.Api.Models.Dtos;

public record CarritoCreateDto
{
    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El precio total debe ser mayor o igual a 0.")]
    [JsonPropertyName("precio_total")]
    public decimal? PrecioTotal { get; init; } = 0.00m;

    [Range(0, int.MaxValue, ErrorMessage = "El número de artículos no puede ser negativo.")]
    [JsonPropertyName("numero_articulos")]
    public int? NumeroArticulos { get; init; } = 0;

    [JsonPropertyName("expirado_en")]
    public DateTimeOffset? ExpiradoEn { get; init; }
}

public record CarritoUpdateDto
{
    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El precio total debe ser mayor o igual a 0.")]
    [JsonPropertyName("precio_total")]
    public decimal? PrecioTotal { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = "El número de artículos no puede ser negativo.")]
    [JsonPropertyName("numero_articulos")]
    public int? NumeroArticulos { get; init; }

    [JsonPropertyName("expirado_en")]
    public DateTimeOffset? ExpiradoEn { get; init; }
}

public record CarritoResponseDto
{
    [JsonPropertyName("id_carrito")]
    public long IdCarrito { get; init; }

    [JsonPropertyName("precio_total")]
    public decimal PrecioTotal { get; init; }

    [JsonPropertyName("numero_articulos")]
    public int NumeroArticulos { get; init; }

    [JsonPropertyName("expirado_en")]
    public DateTimeOffset? ExpiradoEn { get; init; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; init; }

    [JsonPropertyName("items")]
    public List<ItemCarritoResponseDto>? Items { get; init; }
}
