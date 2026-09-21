
using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;

namespace Identity.Api.Interfaces;

public interface IIntermediarioService
{
    Task SaveIntermediarioAsync(IntermediarioDto dto, UsuarioEntity usuario);
    Task<IntermediarioEntity?> GetByIdAsync(Guid idUsuario);
    Task<IEnumerable<IntermediarioEntity>> GetAllAsync();
    Task<IntermediarioEntity> UpdateIntermediarioAsync(Guid idUsuario, IntermediarioDto dto);
    Task DeleteIntermediarioAsync(Guid idUsuario);
}