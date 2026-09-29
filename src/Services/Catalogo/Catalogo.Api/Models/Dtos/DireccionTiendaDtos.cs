using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Catalogo.Api.Models.Dtos;

public record DireccionTiendaCreateDto
{
    [Required]
    [MaxLength(500)]
    [JsonPropertyName("numero_calle")]
    public string NumeroCalle { get; init; } = null!;

    [Required]
    [MaxLength(100)]
    [JsonPropertyName("ciudad")]
    public string Ciudad { get; init; } = null!;

    [Required]
    [MaxLength(100)]
    [JsonPropertyName("departamento")]
    public string Departamento { get; init; } = null!;

    [MaxLength(20)]
    [JsonPropertyName("codigo_postal")]
    public string? CodigoPostal { get; init; }

    [JsonPropertyName("latitud")]
    public decimal? Latitud { get; init; }

    [JsonPropertyName("longitud")]
    public decimal? Longitud { get; init; }

    [MaxLength(20)]
    [JsonPropertyName("numero_contacto")]
    public string? NumeroContacto { get; init; }
}

public record DireccionTiendaUpdateDto
{
    [JsonPropertyName("id_direccion_tienda")]
    public long? IdDireccionTienda { get; init; }

    [MaxLength(500)]
    [JsonPropertyName("numero_calle")]
    public string? NumeroCalle { get; init; }

    [MaxLength(100)]
    [JsonPropertyName("ciudad")]
    public string? Ciudad { get; init; }

    [MaxLength(100)]
    [JsonPropertyName("departamento")]
    public string? Departamento { get; init; }

    [MaxLength(20)]
    [JsonPropertyName("codigo_postal")]
    public string? CodigoPostal { get; init; }

    [JsonPropertyName("latitud")]
    public decimal? Latitud { get; init; }

    [JsonPropertyName("longitud")]
    public decimal? Longitud { get; init; }

    [MaxLength(20)]
    [JsonPropertyName("numero_contacto")]
    public string? NumeroContacto { get; init; }
}

public record DireccionTiendaResponseDto
{
    [JsonPropertyName("id_direccion_tienda")]
    public long IdDireccionTienda { get; init; }

    [JsonPropertyName("numero_calle")]
    public string NumeroCalle { get; init; } = null!;

    [JsonPropertyName("ciudad")]
    public string Ciudad { get; init; } = null!;

    [JsonPropertyName("departamento")]
    public string Departamento { get; init; } = null!;

    [JsonPropertyName("codigo_postal")]
    public string? CodigoPostal { get; init; }

    [JsonPropertyName("latitud")]
    public decimal? Latitud { get; init; }

    [JsonPropertyName("longitud")]
    public decimal? Longitud { get; init; }

    [JsonPropertyName("numero_contacto")]
    public string? NumeroContacto { get; init; }
}
