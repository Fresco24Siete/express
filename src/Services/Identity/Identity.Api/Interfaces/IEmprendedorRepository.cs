using Identity.Api.Models.Entities;

namespace Identity.Api.Interfaces;

public interface IEmprendedorRepository
{
    Task SaveEmprendedorAsync(EmprendedorEntity emprendedor);
    Task<EmprendedorEntity?> GetByIdAsync(Guid idUsuario);
    Task<IEnumerable<EmprendedorEntity>> GetAllAsync();
    Task UpdateAsync(EmprendedorEntity emprendedor);
    Task DeleteAsync(Guid idUsuario);
}
