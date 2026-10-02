using Microsoft.EntityFrameworkCore;
using Servicio.Api.Config;
using Servicio.Api.Interfaces;
using Servicio.Api.Models.Entities;
using Servicio.Api.Models.Enums;

namespace Servicio.Api.Repositories;

public class ServicioRepository : IServicioRepository
{
    private readonly ServicioContext _context;

    public ServicioRepository(ServicioContext context)
    {
        _context = context;
    }

    public async Task<ServicioEntity> CreateAsync(ServicioEntity servicio)
    {
        await _context.Servicios.AddAsync(servicio);
        await _context.SaveChangesAsync();
        return servicio;
    }

    public async Task<ServicioEntity?> GetByIdAsync(Guid idServicio)
    {
        return await _context.Servicios
            .FirstOrDefaultAsync(s => s.IdServicio == idServicio);
    }

    public async Task<IEnumerable<ServicioEntity>> GetAllAsync(
        Guid? idCliente = null,
        Guid? idEmprendedor = null,
        long? idCategoria = null,
        EstadoServicioEnum? estado = null)
    {
        var query = _context.Servicios.AsQueryable();

        if (idCliente.HasValue)
        {
            query = query.Where(s => s.IdCliente == idCliente.Value);
        }

        if (idEmprendedor.HasValue)
        {
            query = query.Where(s => s.IdEmprendedor == idEmprendedor.Value);
        }

        if (idCategoria.HasValue)
        {
            query = query.Where(s => s.IdCategoriaServicio == idCategoria.Value);
        }

        if (estado.HasValue)
        {
            query = query.Where(s => s.Estado == estado.Value);
        }

        return await query.OrderByDescending(s => s.CreatedAt).ToListAsync();
    }

    public async Task<IEnumerable<ServicioEntity>> GetByClienteIdAsync(Guid idCliente)
    {
        return await _context.Servicios
            .Where(s => s.IdCliente == idCliente)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<ServicioEntity>> GetByEmprendedorIdAsync(Guid idEmprendedor)
    {
        return await _context.Servicios
            .Where(s => s.IdEmprendedor == idEmprendedor)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }

    public async Task UpdateAsync(ServicioEntity servicio)
    {
        _context.Servicios.Update(servicio);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid idServicio)
    {
        var entity = await _context.Servicios.FindAsync(idServicio);
        if (entity != null)
        {
            _context.Servicios.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(Guid idServicio)
    {
        return await _context.Servicios
            .AnyAsync(s => s.IdServicio == idServicio);
    }
}
