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

    public async Task<IEnumerable<UsuarioEntity>> GetAllAsync()
        => await _context.Usuarios.ToListAsync();

    public async Task AddAsync(UsuarioEntity usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UsuarioEntity usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRolAsync(UsuarioEntity usuarioData, string rol)
    {
        var usuario = await _context.Usuarios
            .SingleAsync(u => u.IdUsuario == usuarioData.IdUsuario);

        usuario.RolActual = rol;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}