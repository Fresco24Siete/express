using Identity.Api.Models.Entities;



namespace Identity.Api.Interfaces;

public interface IAuthRepository
{
    Task<UsuarioEntity?> GetByIdAsync(Guid id);
    Task<UsuarioEntity?> GetByEmailAsync(string email);
    Task AddAsync(UsuarioEntity usuario);

}