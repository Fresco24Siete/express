using Ventas.Api.Models.Dtos;

namespace Ventas.Api.Interfaces;

public interface IDevolucionService
{
    Task<DevolucionResponseDto> CreateAsync(DevolucionCreateDto dto);
    Task<DevolucionResponseDto?> GetByIdAsync(Guid idDevolucion);
    Task<IEnumerable<DevolucionResponseDto>> GetAllAsync();
    Task<IEnumerable<DevolucionResponseDto>> GetByOrdenIdAsync(Guid idOrden);
    Task<IEnumerable<DevolucionResponseDto>> GetByDetalleOrdenIdAsync(Guid idDetalleOrden);
    Task<DevolucionResponseDto> UpdateAsync(Guid idDevolucion, DevolucionUpdateDto dto);
    Task DeleteAsync(Guid idDevolucion);
}
