using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Catalogo.Api.Models.Dtos;

public record CategoriaProductoCreateDto
{
    [Required]
    [MaxLength(100)]
    [JsonPropertyName("nombre")]
    public string Nombre { get; init; } = null!;

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; init; }

    [JsonPropertyName("activa")]
    public bool Activa { get; init; } = true;
}

public record CategoriaProductoUpdateDto
{
    [JsonPropertyName("id_categoria_producto")]
    public long? IdCategoriaProducto { get; init; }

    [MaxLength(100)]
    [JsonPropertyName("nombre")]
    public string? Nombre { get; init; }

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; init; }

    [JsonPropertyName("activa")]
    public bool? Activa { get; init; }
}

public record CategoriaProductoResponseDto
{
    [JsonPropertyName("id_categoria_producto")]
    public long IdCategoriaProducto { get; init; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; init; } = null!;

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; init; }

    [JsonPropertyName("activa")]
    public bool Activa { get; init; }
}
