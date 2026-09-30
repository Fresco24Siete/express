using Asignacion.Api.Config;
using Asignacion.Api.Interfaces;
using Asignacion.Api.Models.Entities;
using Asignacion.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Asignacion.Api.Repositories;

public class AsignacionEnvioRepository : IAsignacionEnvioRepository
{
    private readonly AsignacionContext _context;

    public AsignacionEnvioRepository(AsignacionContext context)
    {
        _context = context;
    }

    public async Task SaveAsignacionEnvioAsync(AsignacionEnvioEntity asignacionEnvio)
    {
        await _context.AsignacionesEnvio.AddAsync(asignacionEnvio);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsignacionEnvioAsync(Guid idAsignacionEnvio)
    {
        var entity = await _context.AsignacionesEnvio.FindAsync(idAsignacionEnvio);
        if (entity != null)
        {
            _context.AsignacionesEnvio.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateAsignacionEnvioAsync(AsignacionEnvioEntity asignacionEnvio)
    {
        _context.AsignacionesEnvio.Update(asignacionEnvio);
        await _context.SaveChangesAsync();
    }

    public async Task<AsignacionEnvioEntity?> GetByIdAsync(Guid idAsignacionEnvio)
    {
        return await _context.AsignacionesEnvio
            .FirstOrDefaultAsync(a => a.IdAsignacionEnvio == idAsignacionEnvio);
    }

    public async Task<IEnumerable<AsignacionEnvioEntity>> GetAllAsync()
    {
        return await _context.AsignacionesEnvio
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<AsignacionEnvioEntity>> GetByOrdenIdAsync(Guid idOrden)
    {
        return await _context.AsignacionesEnvio
            .AsNoTracking()
            .Where(a => a.IdOrden == idOrden)
            .ToListAsync();
    }

    public async Task<IEnumerable<AsignacionEnvioEntity>> GetByDomiciliarioIdAsync(Guid idDomiciliario)
    {
        return await _context.AsignacionesEnvio
            .AsNoTracking()
            .Where(a => a.IdDomiciliario == idDomiciliario)
            .ToListAsync();
    }

    public async Task<IEnumerable<AsignacionEnvioEntity>> GetByEstadoAsync(EstadoAsignacionEnvioEnum estado)
    {
        return await _context.AsignacionesEnvio
            .AsNoTracking()
            .Where(a => a.Estado == estado)
            .ToListAsync();
    }
}
