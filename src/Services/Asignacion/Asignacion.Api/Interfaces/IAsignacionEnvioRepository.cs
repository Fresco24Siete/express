using Asignacion.Api.Models.Entities;
using Asignacion.Api.Models.Enums;

namespace Asignacion.Api.Interfaces;

public interface IAsignacionEnvioRepository
{
    Task SaveAsignacionEnvioAsync(AsignacionEnvioEntity asignacionEnvio);
    Task DeleteAsignacionEnvioAsync(Guid idAsignacionEnvio);
    Task UpdateAsignacionEnvioAsync(AsignacionEnvioEntity asignacionEnvio);
    Task<AsignacionEnvioEntity?> GetByIdAsync(Guid idAsignacionEnvio);
    Task<IEnumerable<AsignacionEnvioEntity>> GetAllAsync();
    Task<IEnumerable<AsignacionEnvioEntity>> GetByOrdenIdAsync(Guid idOrden);
    Task<IEnumerable<AsignacionEnvioEntity>> GetByDomiciliarioIdAsync(Guid idDomiciliario);
    Task<IEnumerable<AsignacionEnvioEntity>> GetByEstadoAsync(EstadoAsignacionEnvioEnum estado);
}
