using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;

namespace Identity.Api.Interfaces;

public interface IAdministradorTiendaService
{
    Task SaveAdministradorTiendaAsync(AdministradorTiendaDto dto, UsuarioEntity usuario);
    Task<AdministradorTiendaEntity?> GetByIdAsync(Guid idUsuario);
    Task<IEnumerable<AdministradorTiendaEntity>> GetAllAsync();
    Task<AdministradorTiendaEntity> UpdateAdministradorTiendaAsync(Guid idUsuario, AdministradorTiendaDto dto);
    Task DeleteAdministradorTiendaAsync(Guid idUsuario);
}
