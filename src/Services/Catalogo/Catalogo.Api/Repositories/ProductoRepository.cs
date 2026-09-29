using Catalogo.Api.Interface;
using Catalogo.Api.Models.Entities;
using Identity.Api.config;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly CatalogoContext _context;

    public ProductoRepository(CatalogoContext context)
    {
        _context = context;
    }

    public async Task SaveProductoAsync(ProductoEntity producto)
    {
        await _context.Producto.AddAsync(producto);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductoAsync(Guid idProducto)
    {
        var producto = await _context.Producto.FindAsync(idProducto);
        if (producto != null)
        {
            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateProductoAsync(ProductoEntity producto)
    {
        _context.Producto.Update(producto);
        await _context.SaveChangesAsync();
    }

    public async Task<ProductoEntity?> GetByIdAsync(Guid idProducto)
    {
        return await _context.Producto
            .FirstOrDefaultAsync(p => p.IdProducto == idProducto);
    }

    public async Task<ProductoEntity?> GetProductoEntity(string nombre)
    {
        return await _context.Producto
            .FirstOrDefaultAsync(p => p.Nombre == nombre);
    }

    public async Task<IEnumerable<ProductoEntity>> GetAllProductoAsync()
    {
        return await _context.Producto
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductoEntity>> GetByTiendaIdAsync(Guid idTienda)
    {
        return await _context.Producto
            .Where(p => p.IdTienda == idTienda)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductoEntity>> GetByCategoriaIdAsync(long idCategoria)
    {
        return await _context.Producto
            .Where(p => p.IdCategoriaProducto == idCategoria)
            .ToListAsync();
    }

    public async Task<ProductoEntity?> GetBySlugAsync(string slug)
    {
        return await _context.Producto
            .FirstOrDefaultAsync(p => p.Slug == slug);
    }

    public async Task<ProductoEntity?> GetBySkuAsync(string sku)
    {
        return await _context.Producto
            .FirstOrDefaultAsync(p => p.Sku == sku);
    }
}
