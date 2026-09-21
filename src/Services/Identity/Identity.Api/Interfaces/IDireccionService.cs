using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;

namespace Identity.Api.Interfaces;


public interface IDireccionService
{
    Task GuardarDireccion(DireccionRequestDto dto, UsuarioEntity usuario);
    Task<DireccionEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<DireccionEntity>> GetByUsuarioIdAsync(Guid usuarioId);
    Task<DireccionEntity> UpdateDireccionAsync(Guid id, DireccionRequestDto dto, Guid usuarioId);
    Task DeleteDireccionAsync(Guid id, Guid usuarioId);
}