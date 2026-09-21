

using Identity.Api.Models.Dtos;
using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;

namespace Identity.Api.Interfaces;

public interface IDomiciliarioService
{
    Task SaveDomiciliarioAsync(DomiciliarioDto dto, UsuarioEntity usuario);
    Task<DomiciliarioEntity?> GetByIdAsync(Guid idUsuario);
    Task<IEnumerable<DomiciliarioEntity>> GetAllAsync();
    Task<DomiciliarioEntity> UpdateDomiciliarioAsync(Guid idUsuario, DomiciliarioDto dto);
    Task DeleteDomiciliarioAsync(Guid idUsuario);
}