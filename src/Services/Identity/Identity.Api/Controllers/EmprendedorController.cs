using System.Security.Claims;
using Identity.Api.Interfaces;
using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/user/emprendedor")]
public class EmprendedorController : ControllerBase
{
    private readonly IEmprendedorService _service;

    public EmprendedorController(IEmprendedorService service)
    {
        _service = service;
    }

    [Authorize(Roles = "emprendedor")]
    [HttpPost("datos")]
    public async Task<IActionResult> SaveEmprendedor([FromBody] EmprendedorDto dto)
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

            await _service.SaveEmprendedorAsync(dto, usuarioActual);

            return Ok(new { mensaje = "Datos guardados con exito" });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Ocurrio un error interno en el servidor" });
        }
    }

    [Authorize(Roles = "emprendedor")]
    [HttpGet("datos")]
    public async Task<IActionResult> GetEmprendedor()
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        var emprendedor = await _service.GetByIdAsync(idUsuario);
        if (emprendedor == null) return NotFound(new { error = "Datos de emprendedor no encontrados." });

        return Ok(emprendedor);
    }

    [Authorize(Roles = "emprendedor")]
    [HttpPut("datos")]
    public async Task<IActionResult> UpdateEmprendedor([FromBody] EmprendedorDto dto)
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        try
        {
            var actualizado = await _service.UpdateEmprendedorAsync(idUsuario, dto);
            return Ok(new { mensaje = "Datos de emprendedor actualizados con éxito", emprendedor = actualizado });
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

    [Authorize(Roles = "emprendedor")]
    [HttpDelete("datos")]
    public async Task<IActionResult> DeleteEmprendedor()
    {
        var idString = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (string.IsNullOrEmpty(idString) || !Guid.TryParse(idString, out var idUsuario))
        {
            return Unauthorized(new { error = "ID de usuario inválido en el token." });
        }

        try
        {
            await _service.DeleteEmprendedorAsync(idUsuario);
            return Ok(new { mensaje = "Emprendedor eliminado con éxito" });
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
    public async Task<IActionResult> GetAllEmprendedores()
    {
        var emprendedores = await _service.GetAllAsync();
        return Ok(emprendedores);
    }
}
