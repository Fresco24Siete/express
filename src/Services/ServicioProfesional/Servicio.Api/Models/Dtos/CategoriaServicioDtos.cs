using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Servicio.Api.Models.Dtos;

public record CategoriaServicioCreateDto
{
    [Required(ErrorMessage = "El nombre de la categoría es requerido.")]
    [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
    [JsonPropertyName("nombre")]
    public string Nombre { get; init; } = null!;

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "La duración promedio en horas debe ser al menos 1.")]
    [JsonPropertyName("duracion_promedio_horas")]
    public int? DuracionPromedioHoras { get; init; }

    [JsonPropertyName("requiere_ubicacion")]
    public bool? RequiereUbicacion { get; init; } = true;

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El precio base sugerido no puede ser negativo.")]
    [JsonPropertyName("precio_base_sugerido")]
    public decimal? PrecioBaseSugerido { get; init; } = 0.00m;

    [JsonPropertyName("activa")]
    public bool? Activa { get; init; } = true;
}

public record CategoriaServicioUpdateDto
{
    [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
    [JsonPropertyName("nombre")]
    public string? Nombre { get; init; }

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "La duración promedio en horas debe ser al menos 1.")]
    [JsonPropertyName("duracion_promedio_horas")]
    public int? DuracionPromedioHoras { get; init; }

    [JsonPropertyName("requiere_ubicacion")]
    public bool? RequiereUbicacion { get; init; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El precio base sugerido no puede ser negativo.")]
    [JsonPropertyName("precio_base_sugerido")]
    public decimal? PrecioBaseSugerido { get; init; }

    [JsonPropertyName("activa")]
    public bool? Activa { get; init; }
}

public record CategoriaServicioResponseDto
{
    [JsonPropertyName("id_categoria_servicio")]
    public long IdCategoriaServicio { get; init; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; init; } = null!;

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; init; }

    [JsonPropertyName("duracion_promedio_horas")]
    public int? DuracionPromedioHoras { get; init; }

    [JsonPropertyName("requiere_ubicacion")]
    public bool RequiereUbicacion { get; init; }

    [JsonPropertyName("precio_base_sugerido")]
    public decimal PrecioBaseSugerido { get; init; }

    [JsonPropertyName("activa")]
    public bool Activa { get; init; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; init; }
}
