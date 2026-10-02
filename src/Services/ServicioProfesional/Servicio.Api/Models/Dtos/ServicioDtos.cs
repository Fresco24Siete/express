using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Servicio.Api.Models.Enums;

namespace Servicio.Api.Models.Dtos;

public record ServicioCreateDto
{
    [Required(ErrorMessage = "El ID del cliente es requerido.")]
    [JsonPropertyName("id_cliente")]
    public Guid IdCliente { get; init; }

    [Required(ErrorMessage = "El ID del emprendedor es requerido.")]
    [JsonPropertyName("id_emprendedor")]
    public Guid IdEmprendedor { get; init; }

    [Required(ErrorMessage = "El ID de la categoría de servicio es requerido.")]
    [JsonPropertyName("id_categoria_servicio")]
    public long IdCategoriaServicio { get; init; }

    [Required(ErrorMessage = "La descripción de la solicitud es requerida.")]
    [JsonPropertyName("descripcion_solicitud")]
    public string DescripcionSolicitud { get; init; } = null!;

    [Range(-90.0, 90.0, ErrorMessage = "La latitud debe estar entre -90 y 90.")]
    [JsonPropertyName("ubicacion_lat")]
    public decimal? UbicacionLat { get; init; }

    [Range(-180.0, 180.0, ErrorMessage = "La longitud debe estar entre -180 y 180.")]
    [JsonPropertyName("ubicacion_lon")]
    public decimal? UbicacionLon { get; init; }

    [JsonPropertyName("fecha_estimada_inicio")]
    public DateTimeOffset? FechaEstimadaInicio { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "La duración estimada en horas debe ser al menos 1.")]
    [JsonPropertyName("duracion_estimada_horas")]
    public int? DuracionEstimadaHoras { get; init; }

    [Required(ErrorMessage = "El costo base es requerido.")]
    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El costo base no puede ser negativo.")]
    [JsonPropertyName("costo_base")]
    public decimal CostoBase { get; init; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El costo adicional no puede ser negativo.")]
    [JsonPropertyName("costo_adicional")]
    public decimal? CostoAdicional { get; init; } = 0.00m;
}

public record ServicioUpdateDto
{
    [JsonPropertyName("id_categoria_servicio")]
    public long? IdCategoriaServicio { get; init; }

    [JsonPropertyName("descripcion_solicitud")]
    public string? DescripcionSolicitud { get; init; }

    [Range(-90.0, 90.0, ErrorMessage = "La latitud debe estar entre -90 y 90.")]
    [JsonPropertyName("ubicacion_lat")]
    public decimal? UbicacionLat { get; init; }

    [Range(-180.0, 180.0, ErrorMessage = "La longitud debe estar entre -180 y 180.")]
    [JsonPropertyName("ubicacion_lon")]
    public decimal? UbicacionLon { get; init; }

    [JsonPropertyName("fecha_estimada_inicio")]
    public DateTimeOffset? FechaEstimadaInicio { get; init; }

    [JsonPropertyName("fecha_real_inicio")]
    public DateTimeOffset? FechaRealInicio { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "La duración estimada en horas debe ser al menos 1.")]
    [JsonPropertyName("duracion_estimada_horas")]
    public int? DuracionEstimadaHoras { get; init; }

    [Range(0, 99.99, ErrorMessage = "La duración real en horas debe ser positiva.")]
    [JsonPropertyName("duracion_real_horas")]
    public decimal? DuracionRealHoras { get; init; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El costo base no puede ser negativo.")]
    [JsonPropertyName("costo_base")]
    public decimal? CostoBase { get; init; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El costo adicional no puede ser negativo.")]
    [JsonPropertyName("costo_adicional")]
    public decimal? CostoAdicional { get; init; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El costo total no puede ser negativo.")]
    [JsonPropertyName("costo_total")]
    public decimal? CostoTotal { get; init; }

    [JsonPropertyName("estado")]
    public EstadoServicioEnum? Estado { get; init; }

    [JsonPropertyName("razon_cancelacion")]
    public string? RazonCancelacion { get; init; }
}

public record ServicioCambiarEstadoDto
{
    [Required(ErrorMessage = "El nuevo estado es requerido.")]
    [JsonPropertyName("estado")]
    public EstadoServicioEnum Estado { get; init; }

    [JsonPropertyName("razon_cancelacion")]
    public string? RazonCancelacion { get; init; }

    [JsonPropertyName("fecha_real_inicio")]
    public DateTimeOffset? FechaRealInicio { get; init; }

    [JsonPropertyName("duracion_real_horas")]
    public decimal? DuracionRealHoras { get; init; }
}

public record ServicioResponseDto
{
    [JsonPropertyName("id_servicio")]
    public Guid IdServicio { get; init; }

    [JsonPropertyName("id_cliente")]
    public Guid IdCliente { get; init; }

    [JsonPropertyName("id_emprendedor")]
    public Guid IdEmprendedor { get; init; }

    [JsonPropertyName("id_categoria_servicio")]
    public long IdCategoriaServicio { get; init; }

    [JsonPropertyName("descripcion_solicitud")]
    public string DescripcionSolicitud { get; init; } = null!;

    [JsonPropertyName("ubicacion_lat")]
    public decimal? UbicacionLat { get; init; }

    [JsonPropertyName("ubicacion_lon")]
    public decimal? UbicacionLon { get; init; }

    [JsonPropertyName("fecha_estimada_inicio")]
    public DateTimeOffset? FechaEstimadaInicio { get; init; }

    [JsonPropertyName("fecha_real_inicio")]
    public DateTimeOffset? FechaRealInicio { get; init; }

    [JsonPropertyName("duracion_estimada_horas")]
    public int? DuracionEstimadaHoras { get; init; }

    [JsonPropertyName("duracion_real_horas")]
    public decimal? DuracionRealHoras { get; init; }

    [JsonPropertyName("costo_base")]
    public decimal CostoBase { get; init; }

    [JsonPropertyName("costo_adicional")]
    public decimal CostoAdicional { get; init; }

    [JsonPropertyName("costo_total")]
    public decimal CostoTotal { get; init; }

    [JsonPropertyName("estado")]
    public EstadoServicioEnum Estado { get; init; }

    [JsonPropertyName("razon_cancelacion")]
    public string? RazonCancelacion { get; init; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; init; }
}
