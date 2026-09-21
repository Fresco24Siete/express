using Identity.Api.Models.Entities;

namespace Identity.Api.Interfaces;

public interface IIntermediarioRepository
{
    Task SaveIntermediarioAsync(IntermediarioEntity intermediario);
    Task<IntermediarioEntity?> GetByIdAsync(Guid idUsuario);
    Task<IEnumerable<IntermediarioEntity>> GetAllAsync();
    Task UpdateAsync(IntermediarioEntity intermediario);
    Task DeleteAsync(Guid idUsuario);
}