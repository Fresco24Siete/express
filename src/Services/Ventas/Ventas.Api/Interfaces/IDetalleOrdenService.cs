using Ventas.Api.Models.Dtos;

namespace Ventas.Api.Interfaces;

public interface IDetalleOrdenService
{
    Task<DetalleOrdenResponseDto> CreateAsync(DetalleOrdenCreateDto dto);
    Task<DetalleOrdenResponseDto?> GetByIdAsync(Guid idDetalleOrden);
    Task<IEnumerable<DetalleOrdenResponseDto>> GetAllAsync();
    Task<IEnumerable<DetalleOrdenResponseDto>> GetByOrdenIdAsync(Guid idOrden);
    Task<DetalleOrdenResponseDto> UpdateAsync(Guid idDetalleOrden, DetalleOrdenUpdateDto dto);
    Task DeleteAsync(Guid idDetalleOrden);
}
