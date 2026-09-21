using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;

namespace Identity.Api.Interfaces;

public interface IClienteService
{
    Task SaveClienteAsync(ClienteDto dto, UsuarioEntity usuario);
    Task<ClienteEntity?> GetByIdAsync(Guid idUsuario);
    Task<IEnumerable<ClienteEntity>> GetAllAsync();
    Task<ClienteEntity> UpdateClienteAsync(Guid idUsuario, ClienteDto dto);
    Task DeleteClienteAsync(Guid idUsuario);
}
