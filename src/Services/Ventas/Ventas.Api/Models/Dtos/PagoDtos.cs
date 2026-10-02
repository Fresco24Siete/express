using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ventas.Api.Models.Enums;

namespace Ventas.Api.Models.Dtos;

public record PagoCreateDto
{
    [Required(ErrorMessage = "El id_orden es obligatorio.")]
    [JsonPropertyName("id_orden")]
    public Guid IdOrden { get; init; }

    [Required(ErrorMessage = "El monto es obligatorio.")]
    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El monto no puede ser negativo.")]
    [JsonPropertyName("monto")]
    public decimal Monto { get; init; }

    [MaxLength(3)]
    [JsonPropertyName("moneda")]
    public string Moneda { get; init; } = "COP";

    [Required(ErrorMessage = "El método de pago es obligatorio.")]
    [JsonPropertyName("metodo")]
    public MetodoPagoEnum Metodo { get; init; } = MetodoPagoEnum.tarjeta_credito;

    [JsonPropertyName("estado")]
    public EstadoPagoEnum Estado { get; init; } = EstadoPagoEnum.pendiente;

    [MaxLength(255)]
    [JsonPropertyName("referencia_externo")]
    public string? ReferenciaExterno { get; init; }

    [MaxLength(500)]
    [JsonPropertyName("token_encriptado")]
    public string? TokenEncriptado { get; init; }

    [MaxLength(4)]
    [JsonPropertyName("ultimos_4_digitos")]
    public string? Ultimos4Digitos { get; init; }

    [JsonPropertyName("fecha_completado")]
    public DateTimeOffset? FechaCompletado { get; init; }

    [JsonPropertyName("razon_fallo")]
    public string? RazonFallo { get; init; }

    [Required(ErrorMessage = "El created_by (usuario) es obligatorio.")]
    [JsonPropertyName("created_by")]
    public Guid CreatedBy { get; init; }
}

public record PagoUpdateDto
{
    [JsonPropertyName("estado")]
    public EstadoPagoEnum? Estado { get; init; }

    [MaxLength(255)]
    [JsonPropertyName("referencia_externo")]
    public string? ReferenciaExterno { get; init; }

    [MaxLength(500)]
    [JsonPropertyName("token_encriptado")]
    public string? TokenEncriptado { get; init; }

    [MaxLength(4)]
    [JsonPropertyName("ultimos_4_digitos")]
    public string? Ultimos4Digitos { get; init; }

    [JsonPropertyName("fecha_completado")]
    public DateTimeOffset? FechaCompletado { get; init; }

    [JsonPropertyName("razon_fallo")]
    public string? RazonFallo { get; init; }
}

public record PagoResponseDto
{
    [JsonPropertyName("id_pago")]
    public Guid IdPago { get; init; }

    [JsonPropertyName("id_orden")]
    public Guid IdOrden { get; init; }

    [JsonPropertyName("monto")]
    public decimal Monto { get; init; }

    [JsonPropertyName("moneda")]
    public string Moneda { get; init; } = "COP";

    [JsonPropertyName("metodo")]
    public MetodoPagoEnum Metodo { get; init; }

    [JsonPropertyName("estado")]
    public EstadoPagoEnum Estado { get; init; }

    [JsonPropertyName("referencia_externo")]
    public string? ReferenciaExterno { get; init; }

    [JsonPropertyName("token_encriptado")]
    public string? TokenEncriptado { get; init; }

    [JsonPropertyName("ultimos_4_digitos")]
    public string? Ultimos4Digitos { get; init; }

    [JsonPropertyName("fecha_intento")]
    public DateTimeOffset FechaIntento { get; init; }

    [JsonPropertyName("fecha_completado")]
    public DateTimeOffset? FechaCompletado { get; init; }

    [JsonPropertyName("razon_fallo")]
    public string? RazonFallo { get; init; }

    [JsonPropertyName("created_by")]
    public Guid CreatedBy { get; init; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; init; }
}
