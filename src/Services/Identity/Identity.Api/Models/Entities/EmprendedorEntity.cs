using System.ComponentModel.DataAnnotations.Schema;
using Identity.Api.Models.Enums;

namespace Identity.Api.Models.Entities;

[Table("emprendedor")]
public class EmprendedorEntity : UsuarioEntity
{   
    [Column("id_categoria_servicio")]
    public long IdCategoriaServicio {get; set;}

    [Column("numero_servicios")]
    public int NumeroServicios {get; set;}

    [Column("estado_certificacion")]
    public EStadoCertificadoEnum EstadoCertidicado {get; set;} = EStadoCertificadoEnum.NoVerificado;

    [Column("disponibilidad_activa")]
    public bool DisponibilidadActiva {get; set;}

    [Column("precio_base_hora", TypeName = "decimal(12,2)")]
    public decimal? PrecioBaseHora {get; set;}

    [Column("descripcion_servicio")]
    public string DescripcionServicio {get; set;} = null!;
}