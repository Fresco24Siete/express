using Microsoft.EntityFrameworkCore;
using Servicio.Api.Config;
using Servicio.Api.Interfaces;
using Servicio.Api.Models.Entities;

namespace Servicio.Api.Repositories;

public class CategoriaServicioRepository : ICategoriaServicioRepository
{
    private readonly ServicioContext _context;

    public CategoriaServicioRepository(ServicioContext context)
    {
        _context = context;
    }

    public async Task<CategoriaServicioEntity> CreateAsync(CategoriaServicioEntity categoria)
    {
        await _context.CategoriasServicio.AddAsync(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<CategoriaServicioEntity?> GetByIdAsync(long idCategoriaServicio)
    {
        return await _context.CategoriasServicio
            .FirstOrDefaultAsync(c => c.IdCategoriaServicio == idCategoriaServicio);
    }

    public async Task<IEnumerable<CategoriaServicioEntity>> GetAllAsync(bool? soloActivas = null)
    {
        var query = _context.CategoriasServicio.AsQueryable();

        if (soloActivas.HasValue)
        {
            query = query.Where(c => c.Activa == soloActivas.Value);
        }

        return await query.OrderBy(c => c.Nombre).ToListAsync();
    }

    public async Task UpdateAsync(CategoriaServicioEntity categoria)
    {
        _context.CategoriasServicio.Update(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long idCategoriaServicio)
    {
        var entity = await _context.CategoriasServicio.FindAsync(idCategoriaServicio);
        if (entity != null)
        {
            _context.CategoriasServicio.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(long idCategoriaServicio)
    {
        return await _context.CategoriasServicio
            .AnyAsync(c => c.IdCategoriaServicio == idCategoriaServicio);
    }
}
