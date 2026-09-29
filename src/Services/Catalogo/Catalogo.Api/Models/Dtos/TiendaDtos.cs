using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Catalogo.Api.Models.Enums;

namespace Catalogo.Api.Models.Dtos;

public record TiendaCreateDto
{
    [Required]
    [JsonPropertyName("id_administrador")]
    public Guid IdAdministrador { get; init; }

    [JsonPropertyName("id_direccion_tienda")]
    public long? IdDireccionTienda { get; init; }

    [Required]
    [MaxLength(255)]
    [JsonPropertyName("nombre")]
    public string Nombre { get; init; } = null!;

    [Required]
    [MaxLength(50)]
    [JsonPropertyName("nit")]
    public string Nit { get; init; } = null!;

    [Required]
    [MaxLength(100)]
    [JsonPropertyName("categoria_principal")]
    public string CategoriaPrincipal { get; init; } = null!;

    [JsonPropertyName("terminos_aceptados")]
    public bool TerminosAceptados { get; init; }

    [JsonPropertyName("direccion")]
    public DireccionTiendaCreateDto? Direccion { get; init; }
}

public record TiendaUpdateDto
{
    [JsonPropertyName("id_tienda")]
    public Guid? IdTienda { get; init; }

    [MaxLength(255)]
    [JsonPropertyName("nombre")]
    public string? Nombre { get; init; }

    [MaxLength(100)]
    [JsonPropertyName("categoria_principal")]
    public string? CategoriaPrincipal { get; init; }

    [JsonPropertyName("estado_certificacion")]
    public EstadoCertificacionEnum? EstadoCertificacion { get; init; }

    [JsonPropertyName("terminos_aceptados")]
    public bool? TerminosAceptados { get; init; }
}

public record TiendaResponseDto
{
    [JsonPropertyName("id_tienda")]
    public Guid IdTienda { get; init; }

    [JsonPropertyName("id_administrador")]
    public Guid IdAdministrador { get; init; }

    [JsonPropertyName("id_direccion_tienda")]
    public long? IdDireccionTienda { get; init; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; init; } = null!;

    [JsonPropertyName("nit")]
    public string Nit { get; init; } = null!;

    [JsonPropertyName("seguidores")]
    public int Seguidores { get; init; }

    [JsonPropertyName("categoria_principal")]
    public string CategoriaPrincipal { get; init; } = null!;

    [JsonPropertyName("estado_certificacion")]
    public EstadoCertificacionEnum EstadoCertificacion { get; init; }

    [JsonPropertyName("cantidad_productos")]
    public int CantidadProductos { get; init; }

    [JsonPropertyName("terminos_aceptados")]
    public bool TerminosAceptados { get; init; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; init; }

    [JsonPropertyName("direccion")]
    public DireccionTiendaResponseDto? Direccion { get; init; }
}
