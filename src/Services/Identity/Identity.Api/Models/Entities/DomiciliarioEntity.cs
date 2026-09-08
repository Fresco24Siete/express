using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Identity.Api.Models.Enums;

namespace Identity.Api.Models.Entities;

[Table("domiciliario")]
public class DomiciliarioEntity : UsuarioEntity
{   
    [Column("placa_vehiculo")]
    public string PlacaVehiculo {get; set;} = null!;

    [Column("tipo_vehiculo")]
    public TipoVehiculoEnum TipoVehiculo {get; set;}

    [Column("capacidad_carga", TypeName = "decimal(8,2)")]
    public decimal CapacidadCarga {get; set;}

    [Column("estado_panel_tareas")]
    [MaxLength(50)]
    public string EstadoPanelTareas {get; set;} = null!;

    [Column("numero_entregas")]
    public int NumeroEntregas {get; set;}

    [Column("numero_entregas_exitosas")]
    public int NumeroEntregasExitosas {get;set;}

    [Column("estado_activo")]
    public bool EstadoActivo {get; set;}

    [Column("valoracion", TypeName = "decimal(3,2)")]
    public decimal Valoracion {get; set;}


}