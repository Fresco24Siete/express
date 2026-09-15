
using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Models.DTOs;

public record LoginDto
{
    [Required]
    [EmailAddress]
    public string Correo { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}