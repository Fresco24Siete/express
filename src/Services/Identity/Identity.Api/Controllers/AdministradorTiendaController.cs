using System.Security.Claims;
using Identity.Api.Interfaces;
using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/user/administrador-tienda")]
public class AdministradorTiendaController : ControllerBase
{
    private readonly IAdministradorTiendaService _service;

    public AdministradorTiendaController(IAdministradorTiendaService service)
    {
        _service = service;
    }

    [Authorize(Roles = "administrador_tienda")]
    [HttpPost("datos")]
    public async Task<IActionResult> SaveAdministradorTienda([FromBody] AdministradorTiendaDto dto)
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

            await _service.SaveAdministradorTiendaAsync(dto, usuarioActual);

            return Ok(new { mensaje = "Datos guardados con exito" });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrio un error interno en el servidor" });
        }
    }

    [Authorize(Roles = "administrador_tienda")]
    [HttpGet("datos")]
    public async Task<IActionResult> GetAdministradorTienda()
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        var admin = await _service.GetByIdAsync(idUsuario);
        if (admin == null) return NotFound(new { error = "Datos de administrador de tienda no encontrados." });

        return Ok(admin);
    }

    [Authorize(Roles = "administrador_tienda")]
    [HttpPut("datos")]
    public async Task<IActionResult> UpdateAdministradorTienda([FromBody] AdministradorTiendaDto dto)
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        try
        {
            var actualizado = await _service.UpdateAdministradorTiendaAsync(idUsuario, dto);
            return Ok(new { mensaje = "Datos de administrador de tienda actualizados con éxito", administrador = actualizado });
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

    [Authorize(Roles = "administrador_tienda")]
    [HttpDelete("datos")]
    public async Task<IActionResult> DeleteAdministradorTienda()
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        try
        {
            await _service.DeleteAdministradorTiendaAsync(idUsuario);
            return Ok(new { mensaje = "Administrador de tienda eliminado con éxito" });
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
    public async Task<IActionResult> GetAllAdministradoresTienda()
    {
        var admins = await _service.GetAllAsync();
        return Ok(admins);
    }
}
