using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;

namespace Identity.Api.Interfaces;

public interface IAuthService
{
    // Recibe el DTO (lo que llega en el JSON) y orquesta el registro
    Task<UsuarioEntity> RegistrarUsuarioAsync(UsuarioRequestDto dto);
    Task<UsuarioEntity> LoginUsuarioAsync(LoginDto dto);
}