using Catalogo.Api.Interface;
using Catalogo.Api.Models.Entities;
using Identity.Api.config;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Repositories;

public class CategoriaProductoRepository : ICategoriaProductoRepository
{
    private readonly CatalogoContext _context;

    public CategoriaProductoRepository(CatalogoContext context)
    {
        _context = context;
    }

    public async Task SaveCategoriaAsync(CategoriaProductoEntity categoria)
    {
        await _context.CategoriaProducto.AddAsync(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoriaAsync(long idCategoria)
    {
        var categoria = await _context.CategoriaProducto.FindAsync(idCategoria);
        if (categoria != null)
        {
            _context.CategoriaProducto.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateCategoriaAsync(CategoriaProductoEntity categoria)
    {
        _context.CategoriaProducto.Update(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task<CategoriaProductoEntity?> GetByIdAsync(long idCategoria)
    {
        return await _context.CategoriaProducto.FindAsync(idCategoria);
    }

    public async Task<CategoriaProductoEntity?> GetCategoriaEntity(string nombre)
    {
        return await _context.CategoriaProducto
            .FirstOrDefaultAsync(c => c.Nombre == nombre);
    }

    public async Task<IEnumerable<CategoriaProductoEntity>> GetAllCategoriasAsync()
    {
        return await _context.CategoriaProducto.ToListAsync();
    }
}
