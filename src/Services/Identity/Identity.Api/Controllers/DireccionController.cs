

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Identity.Api.Interfaces;
using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;
using Identity.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/user/direccion")]

public class DireccionController : ControllerBase
{
    private readonly IDireccionService _service;

    public DireccionController(IDireccionService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpPost("guardar")]
    public async Task<IActionResult> GuardarDireccion([FromBody]DireccionRequestDto dto)
    {   

        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub); // busca "sub"

        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        var usuarioActual = new UsuarioEntity
        {
            IdUsuario = idUsuario,
        };

        try
        {
            await _service.GuardarDireccion(dto, usuarioActual);

            return Ok(new
            {
                mensaje = "Guardado exitoso"
            });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrio un error interno en el servidor" });
        }
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetMisDirecciones()
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        var direcciones = await _service.GetByUsuarioIdAsync(idUsuario);
        return Ok(direcciones);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDireccionById([FromRoute] Guid id)
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        var direccion = await _service.GetByIdAsync(id);
        if (direccion == null) return NotFound(new { error = "Dirección no encontrada." });

        if (direccion.IdUsuario != idUsuario)
        {
            return StatusCode(403, new { error = "No tiene permiso para acceder a esta dirección." });
        }

        return Ok(direccion);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateDireccion([FromRoute] Guid id, [FromBody] DireccionRequestDto dto)
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        try
        {
            var actualizada = await _service.UpdateDireccionAsync(id, dto, idUsuario);
            return Ok(new { mensaje = "Dirección actualizada con éxito", direccion = actualizada });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(403, new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrio un error interno en el servidor" });
        }
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteDireccion([FromRoute] Guid id)
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        try
        {
            await _service.DeleteDireccionAsync(id, idUsuario);
            return Ok(new { mensaje = "Dirección eliminada con éxito" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(403, new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrio un error interno en el servidor" });
        }
    }
}