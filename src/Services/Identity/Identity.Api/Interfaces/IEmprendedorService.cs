using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;

namespace Identity.Api.Interfaces;

public interface IEmprendedorService
{
    Task SaveEmprendedorAsync(EmprendedorDto dto, UsuarioEntity usuario);
    Task<EmprendedorEntity?> GetByIdAsync(Guid idUsuario);
    Task<IEnumerable<EmprendedorEntity>> GetAllAsync();
    Task<EmprendedorEntity> UpdateEmprendedorAsync(Guid idUsuario, EmprendedorDto dto);
    Task DeleteEmprendedorAsync(Guid idUsuario);
}
