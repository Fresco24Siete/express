using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using Identity.Api.Interfaces;
using Identity.Api.Models.Entities;

namespace Identity.Api.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly IdentidadContext _context;

    public ClienteRepository(IdentidadContext context)
    {
        _context = context;
    }

    public async Task SaveClienteAsync(ClienteEntity cliente)
    {
        await _context.Cliente.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task<ClienteEntity?> GetByIdAsync(Guid idUsuario)
    {
        return await _context.Cliente.FindAsync(idUsuario);
    }

    public async Task<IEnumerable<ClienteEntity>> GetAllAsync()
    {
        return await _context.Cliente.ToListAsync();
    }

    public async Task UpdateAsync(ClienteEntity cliente)
    {
        _context.Cliente.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid idUsuario)
    {
        var cliente = await _context.Cliente.FindAsync(idUsuario);
        if (cliente != null)
        {
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
