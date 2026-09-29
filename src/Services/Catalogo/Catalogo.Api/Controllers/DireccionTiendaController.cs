using Catalogo.Api.Interface;
using Catalogo.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.Controllers;

[ApiController]
[Route("api/catalogo/direccion-tienda")]
public class DireccionTiendaController : ControllerBase
{
    private readonly IDireccionTiendaService _service;

    public DireccionTiendaController(IDireccionTiendaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDireccion([FromBody] DireccionTiendaCreateDto dto)
    {
        try
        {
            await _service.SaveDireccionAsync(dto);
            return Ok(new { mensaje = "Dirección de tienda creada con éxito" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDirecciones()
    {
        var direcciones = await _service.GetAllDireccionAsync();
        return Ok(direcciones);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        var direccion = await _service.GetByIdAsync(id);
        if (direccion == null)
        {
            return NotFound(new { error = "Dirección de tienda no encontrada." });
        }

        return Ok(direccion);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateDireccion([FromRoute] long id, [FromBody] DireccionTiendaUpdateDto dto)
    {
        try
        {
            await _service.UpdateDireccionAsync(id, dto);
            return Ok(new { mensaje = "Dirección de tienda actualizada con éxito" });
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

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteDireccion([FromRoute] long id)
    {
        try
        {
            await _service.DeleteDireccionAsync(id);
            return Ok(new { mensaje = "Dirección de tienda eliminada con éxito" });
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
