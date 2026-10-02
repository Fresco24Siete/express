using Ventas.Api.Models.Entities;

namespace Ventas.Api.Interfaces;

public interface IDevolucionRepository
{
    Task<DevolucionEntity> CreateAsync(DevolucionEntity devolucion);
    Task<DevolucionEntity?> GetByIdAsync(Guid idDevolucion);
    Task<IEnumerable<DevolucionEntity>> GetAllAsync();
    Task<IEnumerable<DevolucionEntity>> GetByOrdenIdAsync(Guid idOrden);
    Task<IEnumerable<DevolucionEntity>> GetByDetalleOrdenIdAsync(Guid idDetalleOrden);
    Task UpdateAsync(DevolucionEntity devolucion);
    Task DeleteAsync(Guid idDevolucion);
}
