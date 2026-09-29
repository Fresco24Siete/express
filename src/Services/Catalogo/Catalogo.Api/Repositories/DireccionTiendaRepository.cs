using Catalogo.Api.Interface;
using Catalogo.Api.Models.Entities;
using Identity.Api.config;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Repositories;

public class DireccionTiendaRepository : IDireccionTiendaRepository
{
    private readonly CatalogoContext _context;

    public DireccionTiendaRepository(CatalogoContext context)
    {
        _context = context;
    }

    public async Task SaveDireccionAsync(DireccionTiendaEntity direccionTienda)
    {
        await _context.DireccionTienda.AddAsync(direccionTienda);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDireccionAsync(long idDireccion)
    {
        var direccion = await _context.DireccionTienda.FindAsync(idDireccion);
        if (direccion != null)
        {
            _context.DireccionTienda.Remove(direccion);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateDireccionAsync(DireccionTiendaEntity direccionTienda)
    {
        _context.DireccionTienda.Update(direccionTienda);
        await _context.SaveChangesAsync();
    }

    public async Task<DireccionTiendaEntity?> GetByIdAsync(long idDireccion)
    {
        return await _context.DireccionTienda.FindAsync(idDireccion);
    }

    public async Task<DireccionTiendaEntity?> GetDireccionEntity(long idDireccion)
    {
        return await GetByIdAsync(idDireccion);
    }

    public async Task<IEnumerable<DireccionTiendaEntity>> GetAllDireccionAsync()
    {
        return await _context.DireccionTienda.ToListAsync();
    }
}
