using Identity.Api.Models.Entities;

namespace Identity.Api.Interfaces;

public interface IAdministradorTiendaRepository
{
    Task SaveAdministradorTiendaAsync(AdministradorTiendaEntity admin);
    Task<AdministradorTiendaEntity?> GetByIdAsync(Guid idUsuario);
    Task<IEnumerable<AdministradorTiendaEntity>> GetAllAsync();
    Task UpdateAsync(AdministradorTiendaEntity admin);
    Task DeleteAsync(Guid idUsuario);
}
