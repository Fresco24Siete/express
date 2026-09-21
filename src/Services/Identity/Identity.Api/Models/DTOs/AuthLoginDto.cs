using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Models.DTOs;

public record LoginDto
{
    [Required]
    [EmailAddress]
    [JsonPropertyName("correo")]
    public string Correo { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}