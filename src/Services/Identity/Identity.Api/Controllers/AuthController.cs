using Microsoft.AspNetCore.Mvc;
using Identity.Api.Models.DTOs;
using Identity.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Identity.Api.Models.Entities;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/users/auth")]
public class AuthController : ControllerBase
    {
    private readonly IAuthService _iAuthService;
    private readonly ILogger<AuthController> _logger;
    public AuthController(IAuthService iAuthService, ILogger<AuthController> logger)
    {
        _iAuthService = iAuthService;
        _logger = logger;
    }

    [HttpPost("register")] //post api/users/auth/register
    public async Task<IActionResult> Registrar([FromBody] UsuarioRequestDto dto)
    {
        try
        {
            var nuevoUsuario = await _iAuthService.RegistrarUsuarioAsync(dto);

            return Created("", new 
            { 
                id = nuevoUsuario.IdUsuario, 
                correo = nuevoUsuario.Correo,
                mensaje = "Usuario registrado exitosamente." 
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor." });
        }
    }

    [HttpPost("login")] // POST api/users/auth/login
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var token = await _iAuthService.LoginUsuarioAsync(dto);

            return Ok(new
            {
               token = token
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor." });
        }
    }
 

    [Authorize]
    [HttpPut("nuevo-rol")]
    public async Task<IActionResult> UpdateRolAsync([FromBody] CambioRolDto rolNuevo)
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub); // busca "sub"
        var correo = User.FindFirstValue(JwtRegisteredClaimNames.Email); // busca "email"
        var rol = User.FindFirstValue("role"); // busca "role"

        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        var usuarioActual = new UsuarioEntity
        {
            IdUsuario = idUsuario,
            Correo = correo ?? string.Empty,
            RolActual = rol ?? string.Empty
        };

        await _iAuthService.UpdateRolAsync(usuarioActual, rolNuevo);

        return Ok(new { mensaje = "Rol actualizado con éxito." });
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetPerfil()
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        var usuario = await _iAuthService.GetByIdAsync(idUsuario);
        if (usuario == null) return NotFound(new { error = "Usuario no encontrado." });

        return Ok(usuario);
    }

    [Authorize]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _iAuthService.GetAllAsync();
        return Ok(usuarios);
    }

    [Authorize]
    public async Task<IActionResult> GetById()
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        var usuario = await _iAuthService.GetByIdAsync(idUsuario);
        if (usuario == null) return NotFound(new { error = "Usuario no encontrado." });

        return Ok(usuario);
    }

    [Authorize]
    [HttpPut("me")]
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UsuarioUpdateDto dto)
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        try
        {
            var usuarioActualizado = await _iAuthService.UpdateUsuarioAsync(idUsuario, dto);
            return Ok(new { mensaje = "Usuario actualizado con éxito.", usuario = usuarioActualizado });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor." });
        }
    }

    [Authorize]
    [HttpDelete("me")]
    [HttpDelete("delete")]
   
    public async Task<IActionResult> Delete()
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        try
        {
            await _iAuthService.DeleteUsuarioAsync(idUsuario);
            return Ok(new { mensaje = "Usuario eliminado con éxito." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor." });
        }
    }
}