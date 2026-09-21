using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using Identity.Api.Interfaces;
using Identity.Api.Models.Entities;

namespace Identity.Api.Repositories;


public class DireccionRepository : IDireccionRepository
{
    private readonly IdentidadContext _context;

    public DireccionRepository(IdentidadContext context)
    {
        _context = context;
    }

    public async Task AddAsync(DireccionEntity direccion)
    {
        await _context.Direccion.AddAsync(direccion);
        await _context.SaveChangesAsync();
    }

    public async Task<DireccionEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Direccion.FindAsync(id);
    }

    public async Task<IEnumerable<DireccionEntity>> GetByUsuarioIdAsync(Guid usuarioId)
    {
        return await _context.Direccion
            .Where(d => d.IdUsuario == usuarioId)
            .ToListAsync();
    }

    public async Task UpdateAsync(DireccionEntity direccion)
    {
        _context.Direccion.Update(direccion);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var direccion = await _context.Direccion.FindAsync(id);
        if (direccion != null)
        {
            _context.Direccion.Remove(direccion);
            await _context.SaveChangesAsync();
        }
    }
}