using Microsoft.AspNetCore.Mvc;
using Ventas.Api.Interfaces;
using Ventas.Api.Models.Dtos;

namespace Ventas.Api.Controllers;

[ApiController]
[Route("api/ventas/devolucion")]
public class DevolucionController : ControllerBase
{
    private readonly IDevolucionService _service;

    public DevolucionController(IDevolucionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DevolucionCreateDto dto)
    {
        try
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.IdDevolucion }, result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var devolucion = await _service.GetByIdAsync(id);
            if (devolucion == null)
            {
                return NotFound(new { error = $"Devolución con ID {id} no encontrada." });
            }

            return Ok(devolucion);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet("orden/{idOrden:guid}")]
    public async Task<IActionResult> GetByOrdenId([FromRoute] Guid idOrden)
    {
        try
        {
            var devoluciones = await _service.GetByOrdenIdAsync(idOrden);
            return Ok(devoluciones);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet("detalle/{idDetalleOrden:guid}")]
    public async Task<IActionResult> GetByDetalleOrdenId([FromRoute] Guid idDetalleOrden)
    {
        try
        {
            var devoluciones = await _service.GetByDetalleOrdenIdAsync(idDetalleOrden);
            return Ok(devoluciones);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] DevolucionUpdateDto dto)
    {
        try
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
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
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return Ok(new { mensaje = $"Devolución con ID {id} eliminada con éxito." });
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
