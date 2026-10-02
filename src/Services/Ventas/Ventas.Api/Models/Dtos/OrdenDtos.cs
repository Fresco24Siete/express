using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ventas.Api.Models.Enums;

namespace Ventas.Api.Models.Dtos;

public record OrdenCreateDto
{
    [MaxLength(50)]
    [JsonPropertyName("numero_orden")]
    public string? NumeroOrden { get; init; }

    [Required(ErrorMessage = "El id_usuario es obligatorio.")]
    [JsonPropertyName("id_usuario")]
    public Guid IdUsuario { get; init; }

    [Required(ErrorMessage = "El id_direccion_envio es obligatorio.")]
    [JsonPropertyName("id_direccion_envio")]
    public Guid IdDireccionEnvio { get; init; }

    [JsonPropertyName("id_carrito")]
    public long? IdCarrito { get; init; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El subtotal no puede ser negativo.")]
    [JsonPropertyName("precio_subtotal")]
    public decimal PrecioSubtotal { get; init; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El descuento no puede ser negativo.")]
    [JsonPropertyName("precio_descuento")]
    public decimal PrecioDescuento { get; init; } = 0.00m;

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El costo de envío no puede ser negativo.")]
    [JsonPropertyName("precio_envio")]
    public decimal PrecioEnvio { get; init; } = 0.00m;

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El precio total no puede ser negativo.")]
    [JsonPropertyName("precio_total")]
    public decimal PrecioTotal { get; init; }

    [MaxLength(50)]
    [JsonPropertyName("codigo_descuento")]
    public string? CodigoDescuento { get; init; }

    [JsonPropertyName("notas_cliente")]
    public string? NotasCliente { get; init; }

    [JsonPropertyName("detalles")]
    public List<DetalleOrdenCreateDto>? Detalles { get; init; }
}

public record OrdenUpdateDto
{
    [JsonPropertyName("id_direccion_envio")]
    public Guid? IdDireccionEnvio { get; init; }

    [JsonPropertyName("estado")]
    public EstadoOrdenEnum? Estado { get; init; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El subtotal no puede ser negativo.")]
    [JsonPropertyName("precio_subtotal")]
    public decimal? PrecioSubtotal { get; init; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El descuento no puede ser negativo.")]
    [JsonPropertyName("precio_descuento")]
    public decimal? PrecioDescuento { get; init; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El costo de envío no puede ser negativo.")]
    [JsonPropertyName("precio_envio")]
    public decimal? PrecioEnvio { get; init; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "El precio total no puede ser negativo.")]
    [JsonPropertyName("precio_total")]
    public decimal? PrecioTotal { get; init; }

    [MaxLength(50)]
    [JsonPropertyName("codigo_descuento")]
    public string? CodigoDescuento { get; init; }

    [JsonPropertyName("notas_cliente")]
    public string? NotasCliente { get; init; }

    [JsonPropertyName("fecha_pagada")]
    public DateTimeOffset? FechaPagada { get; init; }

    [JsonPropertyName("fecha_cancelacion")]
    public DateTimeOffset? FechaCancelacion { get; init; }

    [JsonPropertyName("razon_cancelacion")]
    public string? RazonCancelacion { get; init; }
}

public record OrdenCancelarDto
{
    [Required(ErrorMessage = "La razón de cancelación es obligatoria.")]
    [JsonPropertyName("razon_cancelacion")]
    public string RazonCancelacion { get; init; } = null!;
}

public record OrdenResponseDto
{
    [JsonPropertyName("id_orden")]
    public Guid IdOrden { get; init; }

    [JsonPropertyName("numero_orden")]
    public string NumeroOrden { get; init; } = null!;

    [JsonPropertyName("id_usuario")]
    public Guid IdUsuario { get; init; }

    [JsonPropertyName("id_direccion_envio")]
    public Guid IdDireccionEnvio { get; init; }

    [JsonPropertyName("id_carrito")]
    public long? IdCarrito { get; init; }

    [JsonPropertyName("estado")]
    public EstadoOrdenEnum Estado { get; init; }

    [JsonPropertyName("precio_subtotal")]
    public decimal PrecioSubtotal { get; init; }

    [JsonPropertyName("precio_descuento")]
    public decimal PrecioDescuento { get; init; }

    [JsonPropertyName("precio_envio")]
    public decimal PrecioEnvio { get; init; }

    [JsonPropertyName("precio_total")]
    public decimal PrecioTotal { get; init; }

    [JsonPropertyName("codigo_descuento")]
    public string? CodigoDescuento { get; init; }

    [JsonPropertyName("notas_cliente")]
    public string? NotasCliente { get; init; }

    [JsonPropertyName("fecha_pagada")]
    public DateTimeOffset? FechaPagada { get; init; }

    [JsonPropertyName("fecha_cancelacion")]
    public DateTimeOffset? FechaCancelacion { get; init; }

    [JsonPropertyName("razon_cancelacion")]
    public string? RazonCancelacion { get; init; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; init; }

    [JsonPropertyName("detalles")]
    public List<DetalleOrdenResponseDto>? Detalles { get; init; }

    [JsonPropertyName("pago")]
    public PagoResponseDto? Pago { get; init; }
}
