

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resena.Api.Models.Enums;

namespace Resena.Api.Models.Entities;


[Table("reseña")]
public class ResenaEntity
{
    [Key]
    [Column("id_resena")]
    public Guid IdResena {get; set;}

    [Column("id_calificador")]
    public Guid IdCalificador {get; set;}

    [Column("id_calificado")]
    public Guid IdCalificado {get; set;}

    [Column("tipo_actividad")]
    public TipoActividadEnum tipoActividadEnum {get; set;}

    [Column("id_referencia")]
    public Guid IdReferencia {get; set;} //orden o servicio

    [Column("puntuacion")]
    public int puntuacion {get; set;}

    [Column("comentario")]
    public string comentario {get; set;} = null!;

    [Column("estado")]
    public EstadoEnum estadoEnum {get; set;}
    
    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [Column("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

}