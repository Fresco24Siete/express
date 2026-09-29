using Catalogo.Api.Interface;
using Catalogo.Api.Models.Entities;
using Identity.Api.config;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Repositories;

public class TiendaRepository : ITiendaRepository
{
    private readonly CatalogoContext _context;

    public TiendaRepository(CatalogoContext context)
    {
        _context = context;
    }

    public async Task SaveTiendaAsync(TiendaEntity tienda)
    {
        await _context.Tienda.AddAsync(tienda);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTiendaAsync(Guid idTienda)
    {
        var tienda = await _context.Tienda.FindAsync(idTienda);
        if (tienda != null)
        {
            _context.Tienda.Remove(tienda);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateTiendaAsync(TiendaEntity tienda)
    {
        _context.Tienda.Update(tienda);
        await _context.SaveChangesAsync();
    }

    public async Task<TiendaEntity?> GetByIdAsync(Guid idTienda)
    {
        return await _context.Tienda
            .FirstOrDefaultAsync(t => t.IdTienda == idTienda);
    }

    public async Task<TiendaEntity?> GetTiendaEntity(string nombre)
    {
        return await _context.Tienda
            .FirstOrDefaultAsync(t => t.Nombre == nombre);
    }

    public async Task<IEnumerable<TiendaEntity>> GetAllTiendasAsync()
    {
        return await _context.Tienda
            .ToListAsync();
    }
}