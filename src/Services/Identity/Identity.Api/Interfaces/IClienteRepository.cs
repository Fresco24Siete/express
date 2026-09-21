using Identity.Api.Models.Entities;

namespace Identity.Api.Interfaces;

public interface IClienteRepository
{
    Task SaveClienteAsync(ClienteEntity cliente);
    Task<ClienteEntity?> GetByIdAsync(Guid idUsuario);
    Task<IEnumerable<ClienteEntity>> GetAllAsync();
    Task UpdateAsync(ClienteEntity cliente);
    Task DeleteAsync(Guid idUsuario);
}
