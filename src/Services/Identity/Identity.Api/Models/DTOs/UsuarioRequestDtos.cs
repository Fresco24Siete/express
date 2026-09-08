using System.ComponentModel.DataAnnotations;
using Identity.Api.Models.Enums;

namespace Identity.Api.Models.DTOs;

public record UsuarioDto
{
    [Required]
    [MaxLength(100)]
    public string Nombres { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Apellidos { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Correo { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string NumeroIdentificacion { get; set; } = null!;

    [Required]
    public TipoIdentificacionEnum TipoIdentificacion { get; set; }

    [MaxLength(20)]
    public string? Telefono { get; set; }

  
    [Required]
    [MinLength(6)] 
    public string Password { get; set; } = null!;

    [MaxLength(500)]
    public string? FotoPerfil { get; set; }

}