using Microsoft.EntityFrameworkCore;
using Ventas.Api.Config;
using Ventas.Api.Interfaces;
using Ventas.Api.Models.Entities;

namespace Ventas.Api.Repositories;

public class DevolucionRepository : IDevolucionRepository
{
    private readonly VentasContext _context;

    public DevolucionRepository(VentasContext context)
    {
        _context = context;
    }

    public async Task<DevolucionEntity> CreateAsync(DevolucionEntity devolucion)
    {
        await _context.Devoluciones.AddAsync(devolucion);
        await _context.SaveChangesAsync();
        return devolucion;
    }

    public async Task<DevolucionEntity?> GetByIdAsync(Guid idDevolucion)
    {
        return await _context.Devoluciones
            .Include(d => d.Orden)
            .Include(d => d.DetalleOrden)
            .FirstOrDefaultAsync(d => d.IdDevolucion == idDevolucion);
    }

    public async Task<IEnumerable<DevolucionEntity>> GetAllAsync()
    {
        return await _context.Devoluciones
            .Include(d => d.Orden)
            .Include(d => d.DetalleOrden)
            .OrderByDescending(d => d.FechaSolicitud)
            .ToListAsync();
    }

    public async Task<IEnumerable<DevolucionEntity>> GetByOrdenIdAsync(Guid idOrden)
    {
        return await _context.Devoluciones
            .Include(d => d.DetalleOrden)
            .Where(d => d.IdOrden == idOrden)
            .OrderByDescending(d => d.FechaSolicitud)
            .ToListAsync();
    }

    public async Task<IEnumerable<DevolucionEntity>> GetByDetalleOrdenIdAsync(Guid idDetalleOrden)
    {
        return await _context.Devoluciones
            .Where(d => d.IdDetalleOrden == idDetalleOrden)
            .OrderByDescending(d => d.FechaSolicitud)
            .ToListAsync();
    }

    public async Task UpdateAsync(DevolucionEntity devolucion)
    {
        _context.Devoluciones.Update(devolucion);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid idDevolucion)
    {
        var devolucion = await _context.Devoluciones.FindAsync(idDevolucion);
        if (devolucion != null)
        {
            _context.Devoluciones.Remove(devolucion);
            await _context.SaveChangesAsync();
        }
    }
}
