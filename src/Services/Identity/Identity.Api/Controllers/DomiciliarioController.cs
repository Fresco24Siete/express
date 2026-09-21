using System.Security.Claims;
using Identity.Api.Interfaces;
using Identity.Api.Models.Dtos;
using Identity.Api.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/user/domiciliario")]
public class DomiciliarioController : ControllerBase
{
    private readonly IDomiciliarioService _service;

    public DomiciliarioController(IDomiciliarioService service)
    {
        _service = service;
    }

    [Authorize(Roles = "domiciliario")]
    [HttpPost("datos")]
    public async Task<IActionResult> SaveDomiciliario([FromBody] DomiciliarioDto dto)
    {
        try
        {
            var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
            {
                return Unauthorized(new { error = "ID de usuario inválido en el token." });
            }

            var usuarioActual = new UsuarioEntity
            {
                IdUsuario = idUsuario,
            };

            await _service.SaveDomiciliarioAsync(dto, usuarioActual);

            return Ok(new { mensaje = "Datos guardados con exito" });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrio un error interno en el servidor" });
        }
    }

    [Authorize(Roles = "domiciliario")]
    [HttpGet("datos")]
    public async Task<IActionResult> GetDomiciliario()
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        var domiciliario = await _service.GetByIdAsync(idUsuario);
        if (domiciliario == null) return NotFound(new { error = "Datos de domiciliario no encontrados." });

        return Ok(domiciliario);
    }

    [Authorize(Roles = "domiciliario")]
    [HttpPut("datos")]
    public async Task<IActionResult> UpdateDomiciliario([FromBody] DomiciliarioDto dto)
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        try
        {
            var actualizado = await _service.UpdateDomiciliarioAsync(idUsuario, dto);
            return Ok(new { mensaje = "Datos de domiciliario actualizados con éxito", domiciliario = actualizado });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrio un error interno en el servidor" });
        }
    }

    [Authorize(Roles = "domiciliario")]
    [HttpDelete("datos")]
    public async Task<IActionResult> DeleteDomiciliario()
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        try
        {
            await _service.DeleteDomiciliarioAsync(idUsuario);
            return Ok(new { mensaje = "Domiciliario eliminado con éxito" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrio un error interno en el servidor" });
        }
    }

    [Authorize]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllDomiciliarios()
    {
        var domiciliarios = await _service.GetAllAsync();
        return Ok(domiciliarios);
    }
}