using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using Identity.Api.Interfaces;
using Identity.Api.Models.Entities;

namespace Identity.Api.Repositories;

public class IntermediarioRepository : IIntermediarioRepository
{
    private readonly IdentidadContext _context;
    public IntermediarioRepository(IdentidadContext context)
    {
        _context = context;
    }

    public async Task SaveIntermediarioAsync(IntermediarioEntity intermediario)
    {
        await _context.Intermediario.AddAsync(intermediario);
        await _context.SaveChangesAsync();
    }

    public async Task<IntermediarioEntity?> GetByIdAsync(Guid idUsuario)
    {
        return await _context.Intermediario.FindAsync(idUsuario);
    }

    public async Task<IEnumerable<IntermediarioEntity>> GetAllAsync()
    {
        return await _context.Intermediario.ToListAsync();
    }

    public async Task UpdateAsync(IntermediarioEntity intermediario)
    {
        _context.Intermediario.Update(intermediario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid idUsuario)
    {
        var intermediario = await _context.Intermediario.FindAsync(idUsuario);
        if (intermediario != null)
        {
            _context.Intermediario.Remove(intermediario);
            await _context.SaveChangesAsync();
        }
    }
}