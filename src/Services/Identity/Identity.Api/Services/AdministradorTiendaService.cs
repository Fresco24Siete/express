using Identity.Api.Interfaces;
using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;

namespace Identity.Api.Services;

public class AdministradorTiendaService : IAdministradorTiendaService
{
    private readonly IAdministradorTiendaRepository _repository;

    public AdministradorTiendaService(IAdministradorTiendaRepository repository)
    {
        _repository = repository;
    }

    public async Task SaveAdministradorTiendaAsync(AdministradorTiendaDto dto, UsuarioEntity usuario)
    {
        var admin = new AdministradorTiendaEntity
        {
            IdUsuario = usuario.IdUsuario,
            EstadoActivo = dto.EstadoActivo
        };

        await _repository.SaveAdministradorTiendaAsync(admin);
    }

    public async Task<AdministradorTiendaEntity?> GetByIdAsync(Guid idUsuario)
    {
        return await _repository.GetByIdAsync(idUsuario);
    }

    public async Task<IEnumerable<AdministradorTiendaEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<AdministradorTiendaEntity> UpdateAdministradorTiendaAsync(Guid idUsuario, AdministradorTiendaDto dto)
    {
        var admin = await _repository.GetByIdAsync(idUsuario);
        if (admin == null)
        {
            throw new KeyNotFoundException("Datos de administrador de tienda no encontrados.");
        }

        admin.EstadoActivo = dto.EstadoActivo;

        await _repository.UpdateAsync(admin);
        return admin;
    }

    public async Task DeleteAdministradorTiendaAsync(Guid idUsuario)
    {
        var admin = await _repository.GetByIdAsync(idUsuario);
        if (admin == null)
        {
            throw new KeyNotFoundException("Datos de administrador de tienda no encontrados.");
        }

        await _repository.DeleteAsync(idUsuario);
    }
}
