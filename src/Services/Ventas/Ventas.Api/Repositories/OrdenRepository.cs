using Microsoft.EntityFrameworkCore;
using Ventas.Api.Config;
using Ventas.Api.Interfaces;
using Ventas.Api.Models.Entities;
using Ventas.Api.Models.Enums;

namespace Ventas.Api.Repositories;

public class OrdenRepository : IOrdenRepository
{
    private readonly VentasContext _context;

    public OrdenRepository(VentasContext context)
    {
        _context = context;
    }

    public async Task<OrdenEntity> CreateAsync(OrdenEntity orden)
    {
        await _context.Ordenes.AddAsync(orden);
        await _context.SaveChangesAsync();
        return orden;
    }

    public async Task<OrdenEntity?> GetByIdAsync(Guid idOrden, bool includeDetails = true)
    {
        var query = _context.Ordenes.AsQueryable();

        if (includeDetails)
        {
            query = query
                .Include(o => o.Detalles)
                .Include(o => o.Pago)
                .Include(o => o.Devoluciones);
        }

        return await query.FirstOrDefaultAsync(o => o.IdOrden == idOrden);
    }

    public async Task<OrdenEntity?> GetByNumeroOrdenAsync(string numeroOrden)
    {
        return await _context.Ordenes
            .Include(o => o.Detalles)
            .Include(o => o.Pago)
            .Include(o => o.Devoluciones)
            .FirstOrDefaultAsync(o => o.NumeroOrden == numeroOrden);
    }

    public async Task<IEnumerable<OrdenEntity>> GetAllAsync()
    {
        return await _context.Ordenes
            .Include(o => o.Detalles)
            .Include(o => o.Pago)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<OrdenEntity>> GetByUsuarioIdAsync(Guid idUsuario)
    {
        return await _context.Ordenes
            .Include(o => o.Detalles)
            .Include(o => o.Pago)
            .Where(o => o.IdUsuario == idUsuario)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<OrdenEntity>> GetByEstadoAsync(EstadoOrdenEnum estado)
    {
        return await _context.Ordenes
            .Include(o => o.Detalles)
            .Include(o => o.Pago)
            .Where(o => o.Estado == estado)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task UpdateAsync(OrdenEntity orden)
    {
        _context.Ordenes.Update(orden);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid idOrden)
    {
        var orden = await _context.Ordenes.FindAsync(idOrden);
        if (orden != null)
        {
            _context.Ordenes.Remove(orden);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsNumeroOrdenAsync(string numeroOrden)
    {
        return await _context.Ordenes.AnyAsync(o => o.NumeroOrden == numeroOrden);
    }
}
