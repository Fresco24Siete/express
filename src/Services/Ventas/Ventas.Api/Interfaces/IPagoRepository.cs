using Ventas.Api.Models.Entities;

namespace Ventas.Api.Interfaces;

public interface IPagoRepository
{
    Task<PagoEntity> CreateAsync(PagoEntity pago);
    Task<PagoEntity?> GetByIdAsync(Guid idPago);
    Task<PagoEntity?> GetByOrdenIdAsync(Guid idOrden);
    Task<IEnumerable<PagoEntity>> GetAllAsync();
    Task UpdateAsync(PagoEntity pago);
    Task DeleteAsync(Guid idPago);
}
