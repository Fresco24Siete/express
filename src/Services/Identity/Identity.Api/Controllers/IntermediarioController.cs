using System.Security.Claims;
using Identity.Api.Interfaces;
using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/user/intermediario")]
public class IntermediarioController : ControllerBase
{
    private readonly IIntermediarioService _service;

    public IntermediarioController(IIntermediarioService service)
    {
        _service = service;
    }

    [Authorize(Roles = "intermediario")]
    [HttpPost("datos")]
    public async Task<IActionResult> SaveIntermediarioAsync([FromBody] IntermediarioDto dto)
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

            await _service.SaveIntermediarioAsync(dto, usuarioActual);

            return Ok(new { mensaje = "Datos guardados con exito" });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrio un error interno en el servidor" });
        }
    }

    [Authorize(Roles = "intermediario")]
    [HttpGet("datos")]
    public async Task<IActionResult> GetIntermediario()
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        var intermediario = await _service.GetByIdAsync(idUsuario);
        if (intermediario == null) return NotFound(new { error = "Datos de intermediario no encontrados." });

        return Ok(intermediario);
    }

    [Authorize(Roles = "intermediario")]
    [HttpPut("datos")]
    public async Task<IActionResult> UpdateIntermediario([FromBody] IntermediarioDto dto)
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        try
        {
            var actualizado = await _service.UpdateIntermediarioAsync(idUsuario, dto);
            return Ok(new { mensaje = "Datos de intermediario actualizados con éxito", intermediario = actualizado });
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

    [Authorize(Roles = "intermediario")]
    [HttpDelete("datos")]
    public async Task<IActionResult> DeleteIntermediario()
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        try
        {
            await _service.DeleteIntermediarioAsync(idUsuario);
            return Ok(new { mensaje = "Intermediario eliminado con éxito" });
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
    public async Task<IActionResult> GetAllIntermediarios()
    {
        var intermediarios = await _service.GetAllAsync();
        return Ok(intermediarios);
    }
}