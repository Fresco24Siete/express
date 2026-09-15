using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using Identity.Api.Models.Entities;
using Identity.Api.Interfaces;

namespace Identity.Api.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly IdentidadContext _context;

    public AuthRepository(IdentidadContext context)
    {
        _context = context;
    }

    public async Task<UsuarioEntity?> GetByIdAsync(Guid id)
        => await _context.Usuarios.FindAsync(id);

    public async Task<UsuarioEntity?> GetByEmailAsync(string email)
        => await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == email);

    public async Task AddAsync(UsuarioEntity usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }
}