using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Api.Models.Entities;

[Table("intermediario")]
public class IntermediarioEntity 
{   

    [Key]
    [Column("id_usuario")]
    public Guid IdUsuario { get; set; } 
    [Column("casos_resueltos")]
    public int CasosResueltos {get;set;}

    [Column("nivel_autorizacion")]
    [MaxLength(50)]
    public string NivelAutorizacion {get; set;} = null!;

    [Column("estado_activo")]
    public bool EstadoActivo {get; set;}
}