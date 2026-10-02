using Servicio.Api.Models.Entities;
using Servicio.Api.Models.Enums;

namespace Servicio.Api.Interfaces;

public interface IServicioRepository
{
    Task<ServicioEntity> CreateAsync(ServicioEntity servicio);
    Task<ServicioEntity?> GetByIdAsync(Guid idServicio);
    Task<IEnumerable<ServicioEntity>> GetAllAsync(Guid? idCliente = null, Guid? idEmprendedor = null, long? idCategoria = null, EstadoServicioEnum? estado = null);
    Task<IEnumerable<ServicioEntity>> GetByClienteIdAsync(Guid idCliente);
    Task<IEnumerable<ServicioEntity>> GetByEmprendedorIdAsync(Guid idEmprendedor);
    Task UpdateAsync(ServicioEntity servicio);
    Task DeleteAsync(Guid idServicio);
    Task<bool> ExistsAsync(Guid idServicio);
}
