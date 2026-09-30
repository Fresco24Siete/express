using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Asignacion.Api.Models.Enums;

namespace Asignacion.Api.Models.Dtos;

public record AsignacionEnvioCreateDto
{
    [Required(ErrorMessage = "El id_orden es obligatorio.")]
    [JsonPropertyName("id_orden")]
    public Guid IdOrden { get; init; }

    [Required(ErrorMessage = "El id_domiciliario es obligatorio.")]
    [JsonPropertyName("id_domiciliario")]
    public Guid IdDomiciliario { get; init; }

    [JsonPropertyName("estado")]
    public EstadoAsignacionEnvioEnum Estado { get; init; } = EstadoAsignacionEnvioEnum.Asignado;

    [JsonPropertyName("fecha_asignacion")]
    public DateTimeOffset? FechaAsignacion { get; init; }

    [JsonPropertyName("fecha_estimada_entrega")]
    public DateTimeOffset? FechaEstimadaEntrega { get; init; }

    [JsonPropertyName("fecha_real_entrega")]
    public DateTimeOffset? FechaRealEntrega { get; init; }

    [Range(-90.0, 90.0, ErrorMessage = "La latitud debe estar entre -90 y 90 grados.")]
    [JsonPropertyName("latitud_actual")]
    public decimal? LatitudActual { get; init; }

    [Range(-180.0, 180.0, ErrorMessage = "La longitud debe estar entre -180 y 180 grados.")]
    [JsonPropertyName("longitud_actual")]
    public decimal? LongitudActual { get; init; }

    [Range(0, 99999999.99, ErrorMessage = "La distancia en km debe ser un valor positivo.")]
    [JsonPropertyName("distancia_km")]
    public decimal? DistanciaKm { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = "El tiempo estimado en minutos debe ser positivo.")]
    [JsonPropertyName("tiempo_estimado_min")]
    public int? TiempoEstimadoMin { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "El número de intento debe ser al menos 1.")]
    [JsonPropertyName("numero_intento")]
    public int NumeroIntento { get; init; } = 1;

    [JsonPropertyName("razon_fallo")]
    public string? RazonFallo { get; init; }

    [MaxLength(1000, ErrorMessage = "La firma del cliente no puede exceder 1000 caracteres.")]
    [JsonPropertyName("firma_cliente")]
    public string? FirmaCliente { get; init; }
}

public record AsignacionEnvioUpdateDto
{
    [JsonPropertyName("id_asignacion_envio")]
    public Guid? IdAsignacionEnvio { get; init; }

    [JsonPropertyName("id_orden")]
    public Guid? IdOrden { get; init; }

    [JsonPropertyName("id_domiciliario")]
    public Guid? IdDomiciliario { get; init; }

    [JsonPropertyName("estado")]
    public EstadoAsignacionEnvioEnum? Estado { get; init; }

    [JsonPropertyName("fecha_asignacion")]
    public DateTimeOffset? FechaAsignacion { get; init; }

    [JsonPropertyName("fecha_estimada_entrega")]
    public DateTimeOffset? FechaEstimadaEntrega { get; init; }

    [JsonPropertyName("fecha_real_entrega")]
    public DateTimeOffset? FechaRealEntrega { get; init; }

    [Range(-90.0, 90.0, ErrorMessage = "La latitud debe estar entre -90 y 90 grados.")]
    [JsonPropertyName("latitud_actual")]
    public decimal? LatitudActual { get; init; }

    [Range(-180.0, 180.0, ErrorMessage = "La longitud debe estar entre -180 y 180 grados.")]
    [JsonPropertyName("longitud_actual")]
    public decimal? LongitudActual { get; init; }

    [Range(0, 99999999.99, ErrorMessage = "La distancia en km debe ser un valor positivo.")]
    [JsonPropertyName("distancia_km")]
    public decimal? DistanciaKm { get; init; }

    [Range(0, int.MaxValue, ErrorMessage = "El tiempo estimado en minutos debe ser positivo.")]
    [JsonPropertyName("tiempo_estimado_min")]
    public int? TiempoEstimadoMin { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "El número de intento debe ser al menos 1.")]
    [JsonPropertyName("numero_intento")]
    public int? NumeroIntento { get; init; }

    [JsonPropertyName("razon_fallo")]
    public string? RazonFallo { get; init; }

    [MaxLength(1000, ErrorMessage = "La firma del cliente no puede exceder 1000 caracteres.")]
    [JsonPropertyName("firma_cliente")]
    public string? FirmaCliente { get; init; }
}

public record AsignacionEnvioResponseDto
{
    [JsonPropertyName("id_asignacion_envio")]
    public Guid IdAsignacionEnvio { get; init; }

    [JsonPropertyName("id_orden")]
    public Guid IdOrden { get; init; }

    [JsonPropertyName("id_domiciliario")]
    public Guid IdDomiciliario { get; init; }

    [JsonPropertyName("estado")]
    public EstadoAsignacionEnvioEnum Estado { get; init; }

    [JsonPropertyName("fecha_asignacion")]
    public DateTimeOffset FechaAsignacion { get; init; }

    [JsonPropertyName("fecha_estimada_entrega")]
    public DateTimeOffset? FechaEstimadaEntrega { get; init; }

    [JsonPropertyName("fecha_real_entrega")]
    public DateTimeOffset? FechaRealEntrega { get; init; }

    [JsonPropertyName("latitud_actual")]
    public decimal? LatitudActual { get; init; }

    [JsonPropertyName("longitud_actual")]
    public decimal? LongitudActual { get; init; }

    [JsonPropertyName("distancia_km")]
    public decimal? DistanciaKm { get; init; }

    [JsonPropertyName("tiempo_estimado_min")]
    public int? TiempoEstimadoMin { get; init; }

    [JsonPropertyName("numero_intento")]
    public int NumeroIntento { get; init; }

    [JsonPropertyName("razon_fallo")]
    public string? RazonFallo { get; init; }

    [JsonPropertyName("firma_cliente")]
    public string? FirmaCliente { get; init; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; init; }
}
