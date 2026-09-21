using Identity.Api.Models.Entities;



namespace Identity.Api.Interfaces;

public interface IAuthRepository
{
    Task<UsuarioEntity?> GetByIdAsync(Guid id);
    Task<UsuarioEntity?> GetByEmailAsync(string email);
    Task<IEnumerable<UsuarioEntity>> GetAllAsync();
    Task AddAsync(UsuarioEntity usuario);
    Task UpdateAsync(UsuarioEntity usuario);
    Task UpdateRolAsync(UsuarioEntity usuario, string rol);
    Task DeleteAsync(Guid id);
}