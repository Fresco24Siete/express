using Catalogo.Api.Models.Entities;

namespace Catalogo.Api.Interface;

public interface ICategoriaProductoRepository
{
    Task SaveCategoriaAsync(CategoriaProductoEntity categoria);
    Task DeleteCategoriaAsync(long idCategoria);
    Task UpdateCategoriaAsync(CategoriaProductoEntity categoria);
    Task<CategoriaProductoEntity?> GetByIdAsync(long idCategoria);
    Task<CategoriaProductoEntity?> GetCategoriaEntity(string nombre);
    Task<IEnumerable<CategoriaProductoEntity>> GetAllCategoriasAsync();
}
