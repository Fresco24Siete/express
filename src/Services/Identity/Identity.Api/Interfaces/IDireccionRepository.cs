using Identity.Api.Models.Entities;


namespace Identity.Api.Interfaces;


public interface IDireccionRepository
{
    Task AddAsync(DireccionEntity direccion);
    Task<DireccionEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<DireccionEntity>> GetByUsuarioIdAsync(Guid usuarioId);
    Task UpdateAsync(DireccionEntity direccion);
    Task DeleteAsync(Guid id);
}