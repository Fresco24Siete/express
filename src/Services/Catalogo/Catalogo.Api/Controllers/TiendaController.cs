using Catalogo.Api.Interface;
using Catalogo.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.Controllers;

[ApiController]
[Route("api/catalogo/tienda")]
public class TiendaController : ControllerBase
{
    private readonly ITiendaService _service;

    public TiendaController(ITiendaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTienda([FromBody] TiendaCreateDto dto)
    {
        try
        {
            await _service.SaveTiendaAsync(dto);
            return Ok(new { mensaje = "Tienda creada con éxito" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTiendas()
    {
        var tiendas = await _service.GetAllTiendasAsync();
        return Ok(tiendas);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var tienda = await _service.GetByIdAsync(id);
        if (tienda == null)
        {
            return NotFound(new { error = "Tienda no encontrada." });
        }

        return Ok(tienda);
    }

    [HttpGet("nombre/{nombre}")]
    public async Task<IActionResult> GetByNombre([FromRoute] string nombre)
    {
        var tienda = await _service.GetTiendaEntity(nombre);
        if (tienda == null)
        {
            return NotFound(new { error = "Tienda no encontrada." });
        }

        return Ok(tienda);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTienda([FromRoute] Guid id, [FromBody] TiendaUpdateDto dto)
    {
        try
        {
            await _service.UpdateTiendaAsync(id, dto);
            return Ok(new { mensaje = "Tienda actualizada con éxito" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTienda([FromRoute] Guid id)
    {
        try
        {
            await _service.DeleteTiendaAsync(id);
            return Ok(new { mensaje = "Tienda eliminada con éxito" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }
}
