using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Asignacion.Api.Models.Enums;

namespace Asignacion.Api.Models.Entities;

[Table("asignacion_envio")]
public class AsignacionEnvioEntity
{
    [Key]
    [Column("id_asignacion_envio")]
    public Guid IdAsignacionEnvio { get; set; }

    [Column("id_orden")]
    public Guid IdOrden { get; set; }

    [Column("id_domiciliario")]
    public Guid IdDomiciliario { get; set; }

    [Column("estado")]
    public EstadoAsignacionEnvioEnum Estado { get; set; } = EstadoAsignacionEnvioEnum.Asignado;

    [Column("fecha_asignacion")]
    public DateTimeOffset FechaAsignacion { get; set; } = DateTimeOffset.UtcNow;

    [Column("fecha_estimada_entrega")]
    public DateTimeOffset? FechaEstimadaEntrega { get; set; }

    [Column("fecha_real_entrega")]
    public DateTimeOffset? FechaRealEntrega { get; set; }

    [Column("latitud_actual", TypeName = "decimal(9,6)")]
    public decimal? LatitudActual { get; set; }

    [Column("longitud_actual", TypeName = "decimal(9,6)")]
    public decimal? LongitudActual { get; set; }

    [Column("distancia_km", TypeName = "decimal(10,2)")]
    public decimal? DistanciaKm { get; set; }

    [Column("tiempo_estimado_min")]
    public int? TiempoEstimadoMin { get; set; }

    [Column("numero_intento")]
    public int NumeroIntento { get; set; } = 1;

    [Column("razon_fallo", TypeName = "text")]
    public string? RazonFallo { get; set; }

    [Column("firma_cliente")]
    [MaxLength(1000)]
    public string? FirmaCliente { get; set; }

    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [Column("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
