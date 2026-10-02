using Microsoft.AspNetCore.Mvc;
using Ventas.Api.Interfaces;
using Ventas.Api.Models.Dtos;
using Ventas.Api.Models.Enums;

namespace Ventas.Api.Controllers;

[ApiController]
[Route("api/ventas/orden")]
public class OrdenController : ControllerBase
{
    private readonly IOrdenService _service;

    public OrdenController(IOrdenService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrdenCreateDto dto)
    {
        try
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.IdOrden }, result);
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
            var orden = await _service.GetByIdAsync(id);
            if (orden == null)
            {
                return NotFound(new { error = $"Orden con ID {id} no encontrada." });
            }

            return Ok(orden);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet("numero/{numeroOrden}")]
    public async Task<IActionResult> GetByNumeroOrden([FromRoute] string numeroOrden)
    {
        try
        {
            var orden = await _service.GetByNumeroOrdenAsync(numeroOrden);
            if (orden == null)
            {
                return NotFound(new { error = $"Orden con número '{numeroOrden}' no encontrada." });
            }

            return Ok(orden);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet("usuario/{idUsuario:guid}")]
    public async Task<IActionResult> GetByUsuarioId([FromRoute] Guid idUsuario)
    {
        try
        {
            var ordenes = await _service.GetByUsuarioIdAsync(idUsuario);
            return Ok(ordenes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet("estado/{estado}")]
    public async Task<IActionResult> GetByEstado([FromRoute] EstadoOrdenEnum estado)
    {
        try
        {
            var ordenes = await _service.GetByEstadoAsync(estado);
            return Ok(ordenes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] OrdenUpdateDto dto)
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

    [HttpPost("{id:guid}/cancelar")]
    public async Task<IActionResult> Cancelar([FromRoute] Guid id, [FromBody] OrdenCancelarDto dto)
    {
        try
        {
            var result = await _service.CancelarOrdenAsync(id, dto);
            return Ok(result);
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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return Ok(new { mensaje = $"Orden con ID {id} eliminada con éxito." });
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
