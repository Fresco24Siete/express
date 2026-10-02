using Ventas.Api.Models.Dtos;

namespace Ventas.Api.Interfaces;

public interface IItemCarritoService
{
    Task<ItemCarritoResponseDto> CreateAsync(ItemCarritoCreateDto dto);
    Task<ItemCarritoResponseDto?> GetByIdAsync(Guid idItemCarrito);
    Task<IEnumerable<ItemCarritoResponseDto>> GetByCarritoIdAsync(long idCarrito);
    Task<ItemCarritoResponseDto> UpdateAsync(Guid idItemCarrito, ItemCarritoUpdateDto dto);
    Task DeleteAsync(Guid idItemCarrito);
    Task ClearCarritoAsync(long idCarrito);
}
