using System.Text.Json.Serialization;
using Identity.Api.Models.Enums;

namespace Identity.Api.Models.DTOs;

public record ClienteDto
{   
    public long? IdCarrito { get; init; }
    public int CantidadResenas { get; init; }
    public string NivelConfianza { get; init; } = null!;
    public MetodoPagoEnum MetodoPago { get; init; }
    public int NumeroPedidos { get; init; }
    public bool EstadoActivo { get; init; }
}

public record AdministradorTiendaDto
{
    public bool EstadoActivo { get; init; }
}

public record IntermediarioDto
{   
    [JsonPropertyName("casos_resueltos")]
    public int CasosResueltos { get; init; }

}

public record EmprendedorDto
{   
    public long IdCategoriaServicio { get; init; }
    public int NumeroServicios { get; init; }
    public EStadoCertificadoEnum EstadoCertidicado { get; init; } = EStadoCertificadoEnum.NoVerificado;
    public bool DisponibilidadActiva { get; init; }
    public decimal? PrecioBaseHora { get; init; }
    public string DescripcionServicio { get; init; } = null!;
}
