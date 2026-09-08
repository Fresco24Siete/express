using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Identity.Api.Models.Enums;

namespace Identity.Api.Models.Entities;

[Table("usuario")]
public class UsuarioEntity
{
    [Key]
    [Column("id_usuario")]
    public Guid IdUsuario { get; set; }

    [Column("nombres")]
    [MaxLength(100)]
    public string Nombres { get; set; } = null!;

    [Column("apellidos")]
    [MaxLength(100)]
    public string Apellidos { get; set; } = null!;

    [Column("correo")]
    [MaxLength(255)]
    public string Correo { get; set; } = null!;

    [Column("numero_identificacion")]
    [MaxLength(20)]
    public string NumeroIdentificacion { get; set; } = null!;

    [Column("tipo_identificacion")]
    public TipoIdentificacionEnum TipoIdentificacion { get; set; }

    [Column("telefono")]
    [MaxLength(20)]
    public string? Telefono { get; set; }

    [Column("password_hash")]
    [MaxLength(255)]
    public string PasswordHash { get; set; } = null!;

    [Column("foto_perfil")]
    [MaxLength(500)]
    public string? FotoPerfil { get; set; }

    [Column("estado")]
    public EstadoUsuarioEnum Estado { get; set; }

    [Column("rol_actual")]
    [MaxLength(50)]
    public string RolActual { get; set; } = null!;

    [Column("fecha_registro")]
    public DateTimeOffset FechaRegistro { get; set; }

    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }
}