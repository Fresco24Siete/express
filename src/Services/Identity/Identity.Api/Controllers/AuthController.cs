using Microsoft.AspNetCore.Mvc;
using Identity.Api.Models.DTOs;
using Identity.Api.Interfaces;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/users/auth")]
public class AuthController : ControllerBase
    {
    private readonly IAuthService _iAuthService;
    public AuthController(IAuthService iAuthService)
    {
        _iAuthService = iAuthService;
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
            var usuario = await _iAuthService.LoginUsuarioAsync(dto);

            return Ok(new
            {
                mensaje = "Inicio de sesión exitoso"  
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

    
}