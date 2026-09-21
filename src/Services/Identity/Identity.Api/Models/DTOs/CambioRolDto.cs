using System.Text.Json.Serialization;

namespace Identity.Api.Models.DTOs;
public record CambioRolDto
{
    [JsonPropertyName("nuevo_rol")]
    public string NuevoRol { get; set; } = string.Empty; 
}