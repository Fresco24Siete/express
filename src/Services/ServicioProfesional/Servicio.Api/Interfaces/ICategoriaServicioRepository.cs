using Servicio.Api.Models.Entities;

namespace Servicio.Api.Interfaces;

public interface ICategoriaServicioRepository
{
    Task<CategoriaServicioEntity> CreateAsync(CategoriaServicioEntity categoria);
    Task<CategoriaServicioEntity?> GetByIdAsync(long idCategoriaServicio);
    Task<IEnumerable<CategoriaServicioEntity>> GetAllAsync(bool? soloActivas = null);
    Task UpdateAsync(CategoriaServicioEntity categoria);
    Task DeleteAsync(long idCategoriaServicio);
    Task<bool> ExistsAsync(long idCategoriaServicio);
}
