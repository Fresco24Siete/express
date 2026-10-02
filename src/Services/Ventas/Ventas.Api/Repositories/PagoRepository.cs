using Microsoft.EntityFrameworkCore;
using Ventas.Api.Config;
using Ventas.Api.Interfaces;
using Ventas.Api.Models.Entities;

namespace Ventas.Api.Repositories;

public class PagoRepository : IPagoRepository
{
    private readonly VentasContext _context;

    public PagoRepository(VentasContext context)
    {
        _context = context;
    }

    public async Task<PagoEntity> CreateAsync(PagoEntity pago)
    {
        await _context.Pagos.AddAsync(pago);
        await _context.SaveChangesAsync();
        return pago;
    }

    public async Task<PagoEntity?> GetByIdAsync(Guid idPago)
    {
        return await _context.Pagos
            .Include(p => p.Orden)
            .FirstOrDefaultAsync(p => p.IdPago == idPago);
    }

    public async Task<PagoEntity?> GetByOrdenIdAsync(Guid idOrden)
    {
        return await _context.Pagos
            .Include(p => p.Orden)
            .FirstOrDefaultAsync(p => p.IdOrden == idOrden);
    }

    public async Task<IEnumerable<PagoEntity>> GetAllAsync()
    {
        return await _context.Pagos
            .OrderByDescending(p => p.FechaIntento)
            .ToListAsync();
    }

    public async Task UpdateAsync(PagoEntity pago)
    {
        _context.Pagos.Update(pago);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid idPago)
    {
        var pago = await _context.Pagos.FindAsync(idPago);
        if (pago != null)
        {
            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();
        }
    }
}
