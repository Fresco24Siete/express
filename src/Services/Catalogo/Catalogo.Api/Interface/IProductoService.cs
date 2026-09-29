using Catalogo.Api.Models.Dtos;
using Catalogo.Api.Models.Entities;

namespace Catalogo.Api.Interface;

public interface IProductoService
{
    Task SaveProductoAsync(ProductoCreateDto producto);
    Task DeleteProductoAsync(Guid idProducto);
    Task UpdateProductoAsync(Guid idProducto, ProductoUpdateDto producto);
    Task UpdateProductoAsync(ProductoUpdateDto producto);
    Task<ProductoEntity?> GetByIdAsync(Guid idProducto);
    Task<ProductoEntity?> GetProductoEntity(string nombre);
    Task<IEnumerable<ProductoEntity>> GetAllProductoAsync();
    Task<IEnumerable<ProductoEntity>> GetByTiendaIdAsync(Guid idTienda);
    Task<IEnumerable<ProductoEntity>> GetByCategoriaIdAsync(long idCategoria);
    Task<ProductoEntity?> GetBySlugAsync(string slug);
    Task<ProductoEntity?> GetBySkuAsync(string sku);
}
