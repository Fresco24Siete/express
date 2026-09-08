using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Api.Models.Entities;

[Table("administrador_tienda")]
public class AdministradorTiendaEntity : UsuarioEntity
{
    [Column("estado_activo")]
    public bool EstadoActivo {get; set;}
}