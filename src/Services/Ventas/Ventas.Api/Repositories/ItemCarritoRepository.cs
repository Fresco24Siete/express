using Microsoft.EntityFrameworkCore;
using Ventas.Api.Config;
using Ventas.Api.Interfaces;
using Ventas.Api.Models.Entities;

namespace Ventas.Api.Repositories;

public class ItemCarritoRepository : IItemCarritoRepository
{
    private readonly VentasContext _context;

    public ItemCarritoRepository(VentasContext context)
    {
        _context = context;
    }

    public async Task<ItemCarritoEntity> CreateAsync(ItemCarritoEntity item)
    {
        await _context.ItemsCarrito.AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<ItemCarritoEntity?> GetByIdAsync(Guid idItemCarrito)
    {
        return await _context.ItemsCarrito
            .FirstOrDefaultAsync(i => i.IdItemCarrito == idItemCarrito);
    }

    public async Task<IEnumerable<ItemCarritoEntity>> GetByCarritoIdAsync(long idCarrito)
    {
        return await _context.ItemsCarrito
            .Where(i => i.IdCarrito == idCarrito)
            .OrderBy(i => i.CreatedAt)
            .ToListAsync();
    }

    public async Task<ItemCarritoEntity?> GetByCarritoAndProductoAsync(long idCarrito, Guid idProducto)
    {
        return await _context.ItemsCarrito
            .FirstOrDefaultAsync(i => i.IdCarrito == idCarrito && i.IdProducto == idProducto);
    }

    public async Task UpdateAsync(ItemCarritoEntity item)
    {
        _context.ItemsCarrito.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid idItemCarrito)
    {
        var item = await _context.ItemsCarrito.FindAsync(idItemCarrito);
        if (item != null)
        {
            _context.ItemsCarrito.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteByCarritoIdAsync(long idCarrito)
    {
        var items = await _context.ItemsCarrito
            .Where(i => i.IdCarrito == idCarrito)
            .ToListAsync();

        if (items.Any())
        {
            _context.ItemsCarrito.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
