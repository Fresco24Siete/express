using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ventas.Api.Models.Enums;

namespace Ventas.Api.Models.Dtos;

public record DevolucionCreateDto
{
    [Required(ErrorMessage = "El id_orden es obligatorio.")]
    [JsonPropertyName("id_orden")]
    public Guid IdOrden { get; init; }

    [Required(ErrorMessage = "El id_detalle_orden es obligatorio.")]
    [JsonPropertyName("id_detalle_orden")]
    public Guid IdDetalleOrden { get; init; }

    [Required(ErrorMessage = "La cantidad devuelta es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad devuelta debe ser al menos 1.")]
    [JsonPropertyName("cantidad_devuelta")]
    public int CantidadDevuelta { get; init; } = 1;

    [Required(ErrorMessage = "La razón de la devolución es obligatoria.")]
    [JsonPropertyName("razon")]
    public RazonDevolucionEnum Razon { get; init; } = RazonDevolucionEnum.defectuoso;

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El monto de reembolso no puede ser negativo.")]
    [JsonPropertyName("monto_reembolso")]
    public decimal MontoReembolso { get; init; } = 0.00m;
}

public record DevolucionUpdateDto
{
    [JsonPropertyName("estado")]
    public EstadoDevolucionEnum? Estado { get; init; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El monto de reembolso no puede ser negativo.")]
    [JsonPropertyName("monto_reembolso")]
    public decimal? MontoReembolso { get; init; }

    [JsonPropertyName("fecha_resolucion")]
    public DateTimeOffset? FechaResolucion { get; init; }
}

public record DevolucionResponseDto
{
    [JsonPropertyName("id_devolucion")]
    public Guid IdDevolucion { get; init; }

    [JsonPropertyName("id_orden")]
    public Guid IdOrden { get; init; }

    [JsonPropertyName("id_detalle_orden")]
    public Guid IdDetalleOrden { get; init; }

    [JsonPropertyName("cantidad_devuelta")]
    public int CantidadDevuelta { get; init; }

    [JsonPropertyName("razon")]
    public RazonDevolucionEnum Razon { get; init; }

    [JsonPropertyName("estado")]
    public EstadoDevolucionEnum Estado { get; init; }

    [JsonPropertyName("monto_reembolso")]
    public decimal MontoReembolso { get; init; }

    [JsonPropertyName("fecha_solicitud")]
    public DateTimeOffset FechaSolicitud { get; init; }

    [JsonPropertyName("fecha_resolucion")]
    public DateTimeOffset? FechaResolucion { get; init; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; init; }
}
