using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Identity.Api.Models.Enums;

namespace Identity.Api.Models.DTOs;

public record UsuarioRequestDto
{
    [Required]
    [MaxLength(100)]
    [JsonPropertyName("nombres")]
    public string Nombres { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    [JsonPropertyName("apellidos")]
    public string Apellidos { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    [JsonPropertyName("correo")]
    public string Correo { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    [JsonPropertyName("numero_identificacion")]
    public string NumeroIdentificacion { get; set; } = null!;

    [Required]
    [JsonPropertyName("tipo_identificacion")]
    public TipoIdentificacionEnum TipoIdentificacion { get; set; }

    [MaxLength(20)]
    [JsonPropertyName("telefono")]
    public string? Telefono { get; set; }

  
    [Required]
    [MinLength(6)]
    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;

    [MaxLength(500)]
    [JsonPropertyName("foto_perfil")]
    public string? FotoPerfil { get; set; }

}

public record UsuarioUpdateDto
{
    [MaxLength(100)]
    [JsonPropertyName("nombres")]
    public string? Nombres { get; init; }

    [MaxLength(100)]
    [JsonPropertyName("apellidos")]
    public string? Apellidos { get; init; }

    [MaxLength(20)]
    [JsonPropertyName("telefono")]
    public string? Telefono { get; init; }

    [MaxLength(500)]
    [JsonPropertyName("foto_perfil")]
    public string? FotoPerfil { get; init; }
}