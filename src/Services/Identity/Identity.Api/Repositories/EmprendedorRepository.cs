using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using Identity.Api.Interfaces;
using Identity.Api.Models.Entities;

namespace Identity.Api.Repositories;

public class EmprendedorRepository : IEmprendedorRepository
{
    private readonly IdentidadContext _context;

    public EmprendedorRepository(IdentidadContext context)
    {
        _context = context;
    }

    public async Task SaveEmprendedorAsync(EmprendedorEntity emprendedor)
    {
        await _context.Emprendedor.AddAsync(emprendedor);
        await _context.SaveChangesAsync();
    }

    public async Task<EmprendedorEntity?> GetByIdAsync(Guid idUsuario)
    {
        return await _context.Emprendedor.FindAsync(idUsuario);
    }

    public async Task<IEnumerable<EmprendedorEntity>> GetAllAsync()
    {
        return await _context.Emprendedor.ToListAsync();
    }

    public async Task UpdateAsync(EmprendedorEntity emprendedor)
    {
        _context.Emprendedor.Update(emprendedor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid idUsuario)
    {
        var emprendedor = await _context.Emprendedor.FindAsync(idUsuario);
        if (emprendedor != null)
        {
            _context.Emprendedor.Remove(emprendedor);
            await _context.SaveChangesAsync();
        }
    }
}
