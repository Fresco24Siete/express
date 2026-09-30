using Microsoft.AspNetCore.Mvc;
using Resena.Api.Models.Dtos;
using Resena.Api.Services;

namespace Resena.Api.Controllers;

[ApiController]
[Route("api/resena")]

public class ResenaController : ControllerBase
{
    public readonly ResenaService _services;

    public ResenaController(ResenaService service)
    {
        _services = service;
    }

    [HttpPost]
     public async Task<IActionResult> CreateResena ([FromBody] CreateResenaDto dto)
    {
        try
        {
            await _services.SaveResenaAsync(dto);
            return Ok(new { mensaje = "Resena creado con éxito" });
        }
        catch (Exception)
        {
            return StatusCode(500, new{error = "Error interno en el servidor"});
        }
    }

    [HttpGet]

    public async Task<IActionResult> GetAllResena()
    {
        try
        {
            var resenas = await _services.GetAllResenaAsync();
            return Ok(resenas);
        }
        catch (Exception)
        {
            return StatusCode(500, new{error = "Error interno en el servidor"});
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteResena([FromBody] Guid id)
    {
        try
        {
            await _services.DeleteResenaAsync(id);
            return Ok(new {mensaje = "resena borrada"});
        }
        catch (KeyNotFoundException ex)
        {
             return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new{error = "Error interno en el servidor"});
        }
    }

}