using Ventas.Api.Models.Dtos;

namespace Ventas.Api.Interfaces;

public interface IPagoService
{
    Task<PagoResponseDto> CreateAsync(PagoCreateDto dto);
    Task<PagoResponseDto?> GetByIdAsync(Guid idPago);
    Task<PagoResponseDto?> GetByOrdenIdAsync(Guid idOrden);
    Task<IEnumerable<PagoResponseDto>> GetAllAsync();
    Task<PagoResponseDto> UpdateAsync(Guid idPago, PagoUpdateDto dto);
    Task DeleteAsync(Guid idPago);
}
