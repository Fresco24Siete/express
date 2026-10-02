using Microsoft.EntityFrameworkCore;
using Ventas.Api.Config;
using Ventas.Api.Interfaces;
using Ventas.Api.Models.Entities;

namespace Ventas.Api.Repositories;

public class DetalleOrdenRepository : IDetalleOrdenRepository
{
    private readonly VentasContext _context;

    public DetalleOrdenRepository(VentasContext context)
    {
        _context = context;
    }

    public async Task<DetalleOrdenEntity> CreateAsync(DetalleOrdenEntity detalle)
    {
        await _context.DetallesOrden.AddAsync(detalle);
        await _context.SaveChangesAsync();
        return detalle;
    }

    public async Task CreateRangeAsync(IEnumerable<DetalleOrdenEntity> detalles)
    {
        await _context.DetallesOrden.AddRangeAsync(detalles);
        await _context.SaveChangesAsync();
    }

    public async Task<DetalleOrdenEntity?> GetByIdAsync(Guid idDetalleOrden)
    {
        return await _context.DetallesOrden
            .FirstOrDefaultAsync(d => d.IdDetalleOrden == idDetalleOrden);
    }

    public async Task<IEnumerable<DetalleOrdenEntity>> GetAllAsync()
    {
        return await _context.DetallesOrden
            .ToListAsync();
    }

    public async Task<IEnumerable<DetalleOrdenEntity>> GetByOrdenIdAsync(Guid idOrden)
    {
        return await _context.DetallesOrden
            .Where(d => d.IdOrden == idOrden)
            .ToListAsync();
    }

    public async Task UpdateAsync(DetalleOrdenEntity detalle)
    {
        _context.DetallesOrden.Update(detalle);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid idDetalleOrden)
    {
        var detalle = await _context.DetallesOrden.FindAsync(idDetalleOrden);
        if (detalle != null)
        {
            _context.DetallesOrden.Remove(detalle);
            await _context.SaveChangesAsync();
        }
    }
}
