using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ventas.Api.Models.Enums;

namespace Ventas.Api.Models.Entities;

[Table("orden")]
public class OrdenEntity
{
    [Key]
    [Column("id_orden")]
    public Guid IdOrden { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("numero_orden")]
    public string NumeroOrden { get; set; } = null!;

    [Column("id_usuario")]
    public Guid IdUsuario { get; set; }

    [Column("id_direccion_envio")]
    public Guid IdDireccionEnvio { get; set; }

    [Column("id_carrito")]
    public long? IdCarrito { get; set; }

    [Column("estado")]
    public EstadoOrdenEnum Estado { get; set; } = EstadoOrdenEnum.pendiente_pago;

    [Column("precio_subtotal", TypeName = "decimal(12,2)")]
    public decimal PrecioSubtotal { get; set; } = 0.00m;

    [Column("precio_descuento", TypeName = "decimal(12,2)")]
    public decimal PrecioDescuento { get; set; } = 0.00m;

    [Column("precio_envio", TypeName = "decimal(12,2)")]
    public decimal PrecioEnvio { get; set; } = 0.00m;

    [Column("precio_total", TypeName = "decimal(12,2)")]
    public decimal PrecioTotal { get; set; } = 0.00m;

    [MaxLength(50)]
    [Column("codigo_descuento")]
    public string? CodigoDescuento { get; set; }

    [Column("notas_cliente", TypeName = "text")]
    public string? NotasCliente { get; set; }

    [Column("fecha_pagada")]
    public DateTimeOffset? FechaPagada { get; set; }

    [Column("fecha_cancelacion")]
    public DateTimeOffset? FechaCancelacion { get; set; }

    [Column("razon_cancelacion", TypeName = "text")]
    public string? RazonCancelacion { get; set; }

    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [Column("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Relaciones
    [ForeignKey(nameof(IdCarrito))]
    public CarritoEntity? Carrito { get; set; }

    public ICollection<DetalleOrdenEntity> Detalles { get; set; } = new List<DetalleOrdenEntity>();

    public PagoEntity? Pago { get; set; }

    public ICollection<DevolucionEntity> Devoluciones { get; set; } = new List<DevolucionEntity>();
}
