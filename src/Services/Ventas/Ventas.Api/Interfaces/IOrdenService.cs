using Ventas.Api.Models.Dtos;
using Ventas.Api.Models.Enums;

namespace Ventas.Api.Interfaces;

public interface IOrdenService
{
    Task<OrdenResponseDto> CreateAsync(OrdenCreateDto dto);
    Task<OrdenResponseDto?> GetByIdAsync(Guid idOrden);
    Task<OrdenResponseDto?> GetByNumeroOrdenAsync(string numeroOrden);
    Task<IEnumerable<OrdenResponseDto>> GetAllAsync();
    Task<IEnumerable<OrdenResponseDto>> GetByUsuarioIdAsync(Guid idUsuario);
    Task<IEnumerable<OrdenResponseDto>> GetByEstadoAsync(EstadoOrdenEnum estado);
    Task<OrdenResponseDto> UpdateAsync(Guid idOrden, OrdenUpdateDto dto);
    Task<OrdenResponseDto> CancelarOrdenAsync(Guid idOrden, OrdenCancelarDto dto);
    Task DeleteAsync(Guid idOrden);
}
