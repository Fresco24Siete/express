using Ventas.Api.Models.Dtos;

namespace Ventas.Api.Interfaces;

public interface ICarritoService
{
    Task<CarritoResponseDto> CreateAsync(CarritoCreateDto dto);
    Task<CarritoResponseDto?> GetByIdAsync(long idCarrito);
    Task<IEnumerable<CarritoResponseDto>> GetAllAsync();
    Task<CarritoResponseDto> UpdateAsync(long idCarrito, CarritoUpdateDto dto);
    Task DeleteAsync(long idCarrito);
    Task RecalculateTotalsAsync(long idCarrito);
}
