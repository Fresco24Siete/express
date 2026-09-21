

using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;

namespace Identity.Api.Interfaces;

public interface IDomiciliarioRepository
{
    Task SaveDomiciliarioAsync(DomiciliarioEntity domiciliario);
    Task<DomiciliarioEntity?> GetByIdAsync(Guid idUsuario);
    Task<IEnumerable<DomiciliarioEntity>> GetAllAsync();
    Task UpdateAsync(DomiciliarioEntity domiciliario);
    Task DeleteAsync(Guid idUsuario);
}