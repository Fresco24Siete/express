using Asignacion.Api.Interfaces;
using Asignacion.Api.Models.Dtos;
using Asignacion.Api.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Asignacion.Api.Controllers;

[ApiController]
[Route("api/asignacion/envio")]
public class AsignacionEnvioController : ControllerBase
{
    private readonly IAsignacionEnvioService _service;

    public AsignacionEnvioController(IAsignacionEnvioService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsignacionEnvio([FromBody] AsignacionEnvioCreateDto dto)
    {
        try
        {
            var result = await _service.SaveAsignacionEnvioAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.IdAsignacionEnvio }, result);
        }
        catch (ArgumentException ex)
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
            var results = await _service.GetAllAsync();
            return Ok(results);
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
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound(new { error = $"Asignación de envío con ID '{id}' no encontrada." });
            }

            return Ok(result);
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
            var results = await _service.GetByOrdenIdAsync(idOrden);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet("domiciliario/{idDomiciliario:guid}")]
    public async Task<IActionResult> GetByDomiciliarioId([FromRoute] Guid idDomiciliario)
    {
        try
        {
            var results = await _service.GetByDomiciliarioIdAsync(idDomiciliario);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet("estado/{estado}")]
    public async Task<IActionResult> GetByEstado([FromRoute] EstadoAsignacionEnvioEnum estado)
    {
        try
        {
            var results = await _service.GetByEstadoAsync(estado);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsignacionEnvio([FromRoute] Guid id, [FromBody] AsignacionEnvioUpdateDto dto)
    {
        try
        {
            var result = await _service.UpdateAsignacionEnvioAsync(id, dto);
            return Ok(new { mensaje = "Asignación de envío actualizada con éxito", data = result });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsignacionEnvio([FromRoute] Guid id)
    {
        try
        {
            await _service.DeleteAsignacionEnvioAsync(id);
            return Ok(new { mensaje = "Asignación de envío eliminada con éxito" });
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
