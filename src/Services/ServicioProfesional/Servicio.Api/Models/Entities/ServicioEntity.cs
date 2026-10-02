using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Servicio.Api.Models.Enums;

namespace Servicio.Api.Models.Entities;

[Table("servicio")]
public class ServicioEntity
{
    [Key]
    [Column("id_servicio")]
    public Guid IdServicio { get; set; } = Guid.NewGuid();

    [Column("id_cliente")]
    public Guid IdCliente { get; set; }

    [Column("id_emprendedor")]
    public Guid IdEmprendedor { get; set; }

    [Column("id_categoria_servicio")]
    public long IdCategoriaServicio { get; set; }

    [Column("descripcion_solicitud", TypeName = "text")]
    public string DescripcionSolicitud { get; set; } = null!;

    [Column("ubicacion_lat", TypeName = "decimal(9,6)")]
    public decimal? UbicacionLat { get; set; }

    [Column("ubicacion_lon", TypeName = "decimal(9,6)")]
    public decimal? UbicacionLon { get; set; }

    [Column("fecha_estimada_inicio")]
    public DateTimeOffset? FechaEstimadaInicio { get; set; }

    [Column("fecha_real_inicio")]
    public DateTimeOffset? FechaRealInicio { get; set; }

    [Column("duracion_estimada_horas")]
    public int? DuracionEstimadaHoras { get; set; }

    [Column("duracion_real_horas", TypeName = "decimal(4,2)")]
    public decimal? DuracionRealHoras { get; set; }

    [Column("costo_base", TypeName = "decimal(12,2)")]
    public decimal CostoBase { get; set; } = 0.00m;

    [Column("costo_adicional", TypeName = "decimal(12,2)")]
    public decimal CostoAdicional { get; set; } = 0.00m;

    [Column("costo_total", TypeName = "decimal(12,2)")]
    public decimal CostoTotal { get; set; } = 0.00m;

    [Column("estado")]
    public EstadoServicioEnum Estado { get; set; } = EstadoServicioEnum.solicitado;

    [Column("razon_cancelacion", TypeName = "text")]
    public string? RazonCancelacion { get; set; }

    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [Column("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
