

using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using Identity.Api.Interfaces;
using Identity.Api.Models.Entities;

namespace Identity.Api.Repositories;

public class DomiciliarioRepository : IDomiciliarioRepository
{
    private readonly IdentidadContext _context;

    public DomiciliarioRepository(IdentidadContext context)
    {
        _context = context;
    }

    public async Task SaveDomiciliarioAsync(DomiciliarioEntity domiciliario)
    {
        await _context.Domiciliario.AddAsync(domiciliario);
        await _context.SaveChangesAsync();
    }

    public async Task<DomiciliarioEntity?> GetByIdAsync(Guid idUsuario)
    {
        return await _context.Domiciliario.FindAsync(idUsuario);
    }

    public async Task<IEnumerable<DomiciliarioEntity>> GetAllAsync()
    {
        return await _context.Domiciliario.ToListAsync();
    }

    public async Task UpdateAsync(DomiciliarioEntity domiciliario)
    {
        _context.Domiciliario.Update(domiciliario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid idUsuario)
    {
        var domiciliario = await _context.Domiciliario.FindAsync(idUsuario);
        if (domiciliario != null)
        {
            _context.Domiciliario.Remove(domiciliario);
            await _context.SaveChangesAsync();
        }
    }
}