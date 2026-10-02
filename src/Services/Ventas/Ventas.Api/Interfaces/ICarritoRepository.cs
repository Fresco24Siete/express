using Ventas.Api.Models.Entities;

namespace Ventas.Api.Interfaces;

public interface ICarritoRepository
{
    Task<CarritoEntity> CreateAsync(CarritoEntity carrito);
    Task<CarritoEntity?> GetByIdAsync(long idCarrito, bool includeItems = true);
    Task<IEnumerable<CarritoEntity>> GetAllAsync();
    Task UpdateAsync(CarritoEntity carrito);
    Task DeleteAsync(long idCarrito);
}
