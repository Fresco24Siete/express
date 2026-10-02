using Microsoft.AspNetCore.Mvc;
using Servicio.Api.Interfaces;
using Servicio.Api.Models.Dtos;

namespace Servicio.Api.Controllers;

[ApiController]
[Route("api/servicio-profesional/categoria")]
public class CategoriaServicioController : ControllerBase
{
    private readonly ICategoriaServicioService _service;

    public CategoriaServicioController(ICategoriaServicioService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoriaServicioCreateDto dto)
    {
        try
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.IdCategoriaServicio }, result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool? soloActivas)
    {
        try
        {
            var list = await _service.GetAllAsync(soloActivas);
            return Ok(list);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        try
        {
            var categoria = await _service.GetByIdAsync(id);
            if (categoria == null)
            {
                return NotFound(new { error = $"Categoría de servicio con ID {id} no encontrada." });
            }

            return Ok(categoria);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update([FromRoute] long id, [FromBody] CategoriaServicioUpdateDto dto)
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

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return Ok(new { mensaje = $"Categoría de servicio con ID {id} eliminada con éxito." });
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
