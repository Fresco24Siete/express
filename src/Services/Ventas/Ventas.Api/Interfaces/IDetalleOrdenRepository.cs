using Ventas.Api.Models.Entities;

namespace Ventas.Api.Interfaces;

public interface IDetalleOrdenRepository
{
    Task<DetalleOrdenEntity> CreateAsync(DetalleOrdenEntity detalle);
    Task CreateRangeAsync(IEnumerable<DetalleOrdenEntity> detalles);
    Task<DetalleOrdenEntity?> GetByIdAsync(Guid idDetalleOrden);
    Task<IEnumerable<DetalleOrdenEntity>> GetAllAsync();
    Task<IEnumerable<DetalleOrdenEntity>> GetByOrdenIdAsync(Guid idOrden);
    Task UpdateAsync(DetalleOrdenEntity detalle);
    Task DeleteAsync(Guid idDetalleOrden);
}
