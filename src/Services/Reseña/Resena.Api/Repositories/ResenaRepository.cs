
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Resena.Api.Config;
using Resena.Api.Interfaces;
using Resena.Api.Models.Entities;

namespace Resena.Api.Repositories;

public class ResenaRepositories : IResenaRepository
{
    public readonly ResenaContext _context;

    public ResenaRepositories(ResenaContext context)
    {
        _context = context;
    }

    public async Task SaveResenaAsync(ResenaEntity resena)
    {
        await _context.Resena.AddAsync(resena);
        await _context.SaveChangesAsync();

    }
    public async Task DeleteResenaAsync (Guid idResena)
    {
        var resena =  await _context.Resena.FindAsync(idResena);
        
        if (resena != null)
        {
            _context.Resena.Remove(resena);
            await _context.SaveChangesAsync();
        }
    }
    public async Task <ResenaEntity?> GetResenaAsync(Guid idResena)
    {
        return await _context.Resena.FindAsync(idResena);
    }
    public async Task <IEnumerable<ResenaEntity> > GetAllResenaAsync()
    {
        return await _context.Resena.ToListAsync();
    }
}