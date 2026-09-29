using Catalogo.Api.Models.Dtos;
using Catalogo.Api.Models.Entities;

namespace Catalogo.Api.Interface;

public interface ICategoriaProductoService
{
    Task SaveCategoriaAsync(CategoriaProductoCreateDto categoria);
    Task DeleteCategoriaAsync(long idCategoria);
    Task UpdateCategoriaAsync(long idCategoria, CategoriaProductoUpdateDto categoria);
    Task UpdateCategoriaAsync(CategoriaProductoUpdateDto categoria);
    Task<CategoriaProductoEntity?> GetByIdAsync(long idCategoria);
    Task<CategoriaProductoEntity?> GetCategoriaEntity(string nombre);
    Task<IEnumerable<CategoriaProductoEntity>> GetAllCategoriasAsync();
}
