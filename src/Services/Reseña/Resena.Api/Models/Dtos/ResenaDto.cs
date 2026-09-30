
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Resena.Api.Models.Enums;

namespace Resena.Api.Models.Dtos;

public record CreateResenaDto
{
    [Required]
    [JsonPropertyName("id_calificador")]
    public Guid IdCalificador {get; set;}
 
    [Required]
    [JsonPropertyName("id_calificado")]
    public Guid IdCalificado {get; set;}

    [JsonPropertyName("tipo_actividad")]
    public TipoActividadEnum tipoActividadEnum {get; set;}

    [Required] 
    [JsonPropertyName("id_referencia")]
    public Guid IdReferencia {get; set;} //orden o servicio

    [JsonPropertyName("puntuacion")]
    public int puntuacion {get; set;}


    [MaxLength(300)]
    [JsonPropertyName("comentario")]
    public string comentario {get; set;} = null!;

    [JsonPropertyName("estado")]
    public EstadoEnum estadoEnum {get; set;}
}

public record ResponseResenaDto
{   

    [Required]
    [JsonPropertyName("id_resena")]
    public Guid IdResena {get; set;}

    [Required]
    [JsonPropertyName("id_calificador")]
    public Guid IdCalificador {get; set;}
 
    [Required]
    [JsonPropertyName("id_calificado")]
    public Guid IdCalificado {get; set;}

    [JsonPropertyName("tipo_actividad")]
    public TipoActividadEnum tipoActividadEnum {get; set;}

    [Required] 
    [JsonPropertyName("id_referencia")]
    public Guid IdReferencia {get; set;} //orden o servicio

    [JsonPropertyName("puntuacion")]
    public int puntuacion {get; set;}

    [MaxLength(300)]
    [JsonPropertyName("comentario")]
    public string comentario {get; set;} = null!;

    [JsonPropertyName("estado")]
    public EstadoEnum estadoEnum {get; set;}
}

