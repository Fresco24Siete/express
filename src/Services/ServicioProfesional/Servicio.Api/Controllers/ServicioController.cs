using Microsoft.AspNetCore.Mvc;
using Servicio.Api.Interfaces;
using Servicio.Api.Models.Dtos;
using Servicio.Api.Models.Enums;

namespace Servicio.Api.Controllers;

[ApiController]
[Route("api/servicio-profesional/servicio")]
public class ServicioController : ControllerBase
{
    private readonly IServicioService _service;

    public ServicioController(IServicioService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ServicioCreateDto dto)
    {
        try
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.IdServicio }, result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] Guid? idCliente,
        [FromQuery] Guid? idEmprendedor,
        [FromQuery] long? idCategoria,
        [FromQuery] EstadoServicioEnum? estado)
    {
        try
        {
            var list = await _service.GetAllAsync(idCliente, idEmprendedor, idCategoria, estado);
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
            var servicio = await _service.GetByIdAsync(id);
            if (servicio == null)
            {
                return NotFound(new { error = $"Servicio con ID {id} no encontrado." });
            }

            return Ok(servicio);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet("cliente/{idCliente:guid}")]
    public async Task<IActionResult> GetByClienteId([FromRoute] Guid idCliente)
    {
        try
        {
            var list = await _service.GetByClienteIdAsync(idCliente);
            return Ok(list);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet("emprendedor/{idEmprendedor:guid}")]
    public async Task<IActionResult> GetByEmprendedorId([FromRoute] Guid idEmprendedor)
    {
        try
        {
            var list = await _service.GetByEmprendedorIdAsync(idEmprendedor);
            return Ok(list);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ServicioUpdateDto dto)
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

    [HttpPatch("{id:guid}/estado")]
    public async Task<IActionResult> CambiarEstado([FromRoute] Guid id, [FromBody] ServicioCambiarEstadoDto dto)
    {
        try
        {
            var result = await _service.CambiarEstadoAsync(id, dto);
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
            return Ok(new { mensaje = $"Servicio con ID {id} eliminado con éxito." });
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
