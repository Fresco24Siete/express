using System.ComponentModel.DataAnnotations;
using Identity.Api.Models.Enums;

namespace Identity.Api.Models.DTOs;

public record DireccionRequestDto
{
    [Required]
    [MaxLength(500)]
    public string NumeroCalle { get; set; } = null!;

    [MaxLength(50)]
    public string? AptoSuiteNumero { get; set; }

    [Required]
    public TipoDomicilioEnum TipoDomicilio { get; set; }

    [Required]
    [MaxLength(100)]
    public string Ciudad { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Departamento { get; set; } = null!;

    [MaxLength(2)]
    public string Pais { get; set; } = "CO"; // Valor por defecto según tu diseño

    [MaxLength(20)]
    public string? CodigoPostal { get; set; }

    public decimal? Latitud { get; set; }
    
    public decimal? Longitud { get; set; }

    public string? ReferenciaAdicional { get; set; }

    public bool IsPredeterminada { get; set; }
}