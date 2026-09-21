using System.Security.Claims;
using Identity.Api.Interfaces;
using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/user/cliente")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _service;

    public ClienteController(IClienteService service)
    {
        _service = service;
    }

    [Authorize(Roles = "cliente")]
    [HttpPost("datos")]
    public async Task<IActionResult> SaveCliente([FromBody] ClienteDto dto)
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

            await _service.SaveClienteAsync(dto, usuarioActual);

            return Ok(new { mensaje = "Datos guardados con exito" });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrio un error interno en el servidor" });
        }
    }

    [Authorize(Roles = "cliente")]
    [HttpGet("datos")]
    public async Task<IActionResult> GetCliente()
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        var cliente = await _service.GetByIdAsync(idUsuario);
        if (cliente == null) return NotFound(new { error = "Datos de cliente no encontrados." });

        return Ok(cliente);
    }

    [Authorize(Roles = "cliente")]
    [HttpPut("datos")]
    public async Task<IActionResult> UpdateCliente([FromBody] ClienteDto dto)
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        try
        {
            var actualizado = await _service.UpdateClienteAsync(idUsuario, dto);
            return Ok(new { mensaje = "Datos de cliente actualizados con éxito", cliente = actualizado });
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

    [Authorize(Roles = "cliente")]
    [HttpDelete("datos")]
    public async Task<IActionResult> DeleteCliente()
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        try
        {
            await _service.DeleteClienteAsync(idUsuario);
            return Ok(new { mensaje = "Cliente eliminado con éxito" });
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
    public async Task<IActionResult> GetAllClientes()
    {
        var clientes = await _service.GetAllAsync();
        return Ok(clientes);
    }
}
