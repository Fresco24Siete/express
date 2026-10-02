using Ventas.Api.Models.Entities;

namespace Ventas.Api.Interfaces;

public interface IItemCarritoRepository
{
    Task<ItemCarritoEntity> CreateAsync(ItemCarritoEntity item);
    Task<ItemCarritoEntity?> GetByIdAsync(Guid idItemCarrito);
    Task<IEnumerable<ItemCarritoEntity>> GetByCarritoIdAsync(long idCarrito);
    Task<ItemCarritoEntity?> GetByCarritoAndProductoAsync(long idCarrito, Guid idProducto);
    Task UpdateAsync(ItemCarritoEntity item);
    Task DeleteAsync(Guid idItemCarrito);
    Task DeleteByCarritoIdAsync(long idCarrito);
}
