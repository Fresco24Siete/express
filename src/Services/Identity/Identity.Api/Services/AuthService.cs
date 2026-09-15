using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;
using Identity.Api.Models.Enums;
using Identity.Api.Interfaces;
using System.Security.Authentication;


namespace Identity.Api.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repository;

    public AuthService(IAuthRepository repository)
    {
        _repository = repository;
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
            RolActual = "Cliente", 
            FechaRegistro = DateTimeOffset.UtcNow,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        await _repository.AddAsync(nuevoUsuario);

        return nuevoUsuario;
    }

    public async Task<UsuarioEntity> LoginUsuarioAsync(LoginDto dto)
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

        return usuarioExistente;
    }

}