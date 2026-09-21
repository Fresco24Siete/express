using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;

namespace Identity.Api.Interfaces;

public interface IAuthService
{
    Task<UsuarioEntity> RegistrarUsuarioAsync(UsuarioRequestDto dto);
    Task<string> LoginUsuarioAsync(LoginDto dto);
    Task UpdateRolAsync(UsuarioEntity usuario, CambioRolDto rol_nuevo);
    Task<UsuarioEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<UsuarioEntity>> GetAllAsync();
    Task<UsuarioEntity> UpdateUsuarioAsync(Guid id, UsuarioUpdateDto dto);
    Task DeleteUsuarioAsync(Guid id);
}