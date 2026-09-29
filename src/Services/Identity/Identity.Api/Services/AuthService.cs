using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;
using Identity.Api.Models.Enums;
using Identity.Api.Interfaces;
using System.Security.Authentication;



namespace Identity.Api.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repository;
    private readonly TokenService _tokenService;


    public AuthService(IAuthRepository repository, TokenService tokenService)
    {
        _repository = repository;
        _tokenService = tokenService;
    }

    public async Task<UsuarioEntity> RegistrarUsuarioAsync(UsuarioRequestDto dto)
    {
    
        var usuarioExistente = await _repository.GetByEmailAsync(dto.Correo);
        if (usuarioExistente != null)
        {
            throw new InvalidOperationException("El correo ya está registrado en el sistema.");
        }

        var passwordHashSalt = BCrypt.Net.BCrypt.HashPassword(dto.Password);


        // Convertir el DTO (Data Transfer Object) a una Entidad de Base de Datos
        var nuevoUsuario = new UsuarioEntity
        {
            Nombres = dto.Nombres,
            Apellidos = dto.Apellidos,
            Correo = dto.Correo,
            NumeroIdentificacion = dto.NumeroIdentificacion,
            TipoIdentificacion = dto.TipoIdentificacion,
            Telefono = dto.Telefono,
            FotoPerfil = dto.FotoPerfil,
            PasswordHash = passwordHashSalt,
            IdUsuario = Guid.NewGuid(),
            Estado = EstadoUsuarioEnum.Activo,
            RolActual = "cliente", 
            FechaRegistro = DateTimeOffset.UtcNow,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        await _repository.AddAsync(nuevoUsuario);

        return nuevoUsuario;
    }

    public async Task<string> LoginUsuarioAsync(LoginDto dto)
    {
        var usuarioExistente = await _repository.GetByEmailAsync(dto.Correo);
    
        if (usuarioExistente == null)
        {
            throw new UnauthorizedAccessException("Credenciales inválidas.");
        }

        bool passwordCorrecta = BCrypt.Net.BCrypt.Verify(dto.Password, usuarioExistente.PasswordHash);

        if (!passwordCorrecta)
        {
            throw new UnauthorizedAccessException("Credenciales inválidas.");
        }


        return _tokenService.GenerarToken(usuarioExistente);
    }

    public async Task UpdateRolAsync(UsuarioEntity usuario, CambioRolDto dto)
    {
        await _repository.UpdateRolAsync(usuario, dto.NuevoRol);
    }

    public async Task<UsuarioEntity?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<UsuarioEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<UsuarioEntity> UpdateUsuarioAsync(Guid id, UsuarioUpdateDto dto)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null)
        {
            throw new KeyNotFoundException("Usuario no encontrado.");
        }

        if (dto.Nombres != null) usuario.Nombres = dto.Nombres;
        if (dto.Apellidos != null) usuario.Apellidos = dto.Apellidos;
        if (dto.Telefono != null) usuario.Telefono = dto.Telefono;
        if (dto.FotoPerfil != null) usuario.FotoPerfil = dto.FotoPerfil;
        usuario.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(usuario);
        return usuario;
    }

    public async Task DeleteUsuarioAsync(Guid id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null)
        {
            throw new KeyNotFoundException("Usuario no encontrado.");
        }

        await _repository.DeleteAsync(id);
    }
}