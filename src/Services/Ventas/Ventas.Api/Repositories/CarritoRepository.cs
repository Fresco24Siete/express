using Microsoft.EntityFrameworkCore;
using Ventas.Api.Config;
using Ventas.Api.Interfaces;
using Ventas.Api.Models.Entities;

namespace Ventas.Api.Repositories;

public class CarritoRepository : ICarritoRepository
{
    private readonly VentasContext _context;

    public CarritoRepository(VentasContext context)
    {
        _context = context;
    }

    public async Task<CarritoEntity> CreateAsync(CarritoEntity carrito)
    {
        await _context.Carritos.AddAsync(carrito);
        await _context.SaveChangesAsync();
        return carrito;
    }

    public async Task<CarritoEntity?> GetByIdAsync(long idCarrito, bool includeItems = true)
    {
        var query = _context.Carritos.AsQueryable();

        if (includeItems)
        {
            query = query.Include(c => c.Items);
        }

        return await query.FirstOrDefaultAsync(c => c.IdCarrito == idCarrito);
    }

    public async Task<IEnumerable<CarritoEntity>> GetAllAsync()
    {
        return await _context.Carritos
            .Include(c => c.Items)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task UpdateAsync(CarritoEntity carrito)
    {
        _context.Carritos.Update(carrito);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long idCarrito)
    {
        var carrito = await _context.Carritos.FindAsync(idCarrito);
        if (carrito != null)
        {
            _context.Carritos.Remove(carrito);
            await _context.SaveChangesAsync();
        }
    }
}
