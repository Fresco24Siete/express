using Ventas.Api.Models.Entities;
using Ventas.Api.Models.Enums;

namespace Ventas.Api.Interfaces;

public interface IOrdenRepository
{
    Task<OrdenEntity> CreateAsync(OrdenEntity orden);
    Task<OrdenEntity?> GetByIdAsync(Guid idOrden, bool includeDetails = true);
    Task<OrdenEntity?> GetByNumeroOrdenAsync(string numeroOrden);
    Task<IEnumerable<OrdenEntity>> GetAllAsync();
    Task<IEnumerable<OrdenEntity>> GetByUsuarioIdAsync(Guid idUsuario);
    Task<IEnumerable<OrdenEntity>> GetByEstadoAsync(EstadoOrdenEnum estado);
    Task UpdateAsync(OrdenEntity orden);
    Task DeleteAsync(Guid idOrden);
    Task<bool> ExistsNumeroOrdenAsync(string numeroOrden);
}
