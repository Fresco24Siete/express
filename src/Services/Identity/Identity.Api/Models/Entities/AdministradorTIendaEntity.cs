using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Api.Models.Entities;

[Table("administrador_tienda")]
public class AdministradorTiendaEntity
{
    [Key]
    [Column("id_usuario")]
    public Guid IdUsuario { get; set; }

    [Column("estado_activo")]
    public bool EstadoActivo { get; set; }
}