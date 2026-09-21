using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using Identity.Api.Interfaces;
using Identity.Api.Models.Entities;

namespace Identity.Api.Repositories;

public class AdministradorTiendaRepository : IAdministradorTiendaRepository
{
    private readonly IdentidadContext _context;

    public AdministradorTiendaRepository(IdentidadContext context)
    {
        _context = context;
    }

    public async Task SaveAdministradorTiendaAsync(AdministradorTiendaEntity admin)
    {
        await _context.AdministradorTienda.AddAsync(admin);
        await _context.SaveChangesAsync();
    }

    public async Task<AdministradorTiendaEntity?> GetByIdAsync(Guid idUsuario)
    {
        return await _context.AdministradorTienda.FindAsync(idUsuario);
    }

    public async Task<IEnumerable<AdministradorTiendaEntity>> GetAllAsync()
    {
        return await _context.AdministradorTienda.ToListAsync();
    }

    public async Task UpdateAsync(AdministradorTiendaEntity admin)
    {
        _context.AdministradorTienda.Update(admin);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid idUsuario)
    {
        var admin = await _context.AdministradorTienda.FindAsync(idUsuario);
        if (admin != null)
        {
            _context.AdministradorTienda.Remove(admin);
            await _context.SaveChangesAsync();
        }
    }
}
