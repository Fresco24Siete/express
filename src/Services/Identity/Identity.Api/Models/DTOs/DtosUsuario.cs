using Identity.Api.Models.Enums;

namespace Identity.Api.Models.DTOs;

public record ClienteDto : UsuarioDto
{   
    public long? IdCarrito { get; init; }
    public int CantidadResenas { get; init; }
    public string NivelConfianza { get; init; } = null!;
    public MetodoPagoEnum MetodoPago { get; init; }
    public int NumeroPedidos { get; init; }
    public bool EstadoActivo { get; init; }
}

public record AdministradorTiendaDto : UsuarioDto
{
    public bool EstadoActivo { get; init; }
}

public record IntermediarioDto : UsuarioDto
{
    public int CasosResueltos { get; init; }
    public string NivelAutorizacion { get; init; } = null!;
    public bool EstadoActivo { get; init; }
}

public record EmprendedorDto : UsuarioDto
{   
    public long IdCategoriaServicio { get; init; }
    public int NumeroServicios { get; init; }
    public EStadoCertificadoEnum EstadoCertidicado { get; init; } = EStadoCertificadoEnum.NoVerificado;
    public bool DisponibilidadActiva { get; init; }
    public decimal? PrecioBaseHora { get; init; }
    public string DescripcionServicio { get; init; } = null!;
}

public record DomiciliarioDto : UsuarioDto
{   
    public string PlacaVehiculo { get; init; } = null!;
    public TipoVehiculoEnum TipoVehiculo { get; init; }
    public decimal CapacidadCarga { get; init; }
    public string EstadoPanelTareas { get; init; } = null!;
    public int NumeroEntregas { get; init; }
    public int NumeroEntregasExitosas { get; init; }
    public bool EstadoActivo { get; init; }
    public decimal Valoracion { get; init; }
}