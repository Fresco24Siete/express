using Catalogo.Api.Interface;
using Catalogo.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.Controllers;

[ApiController]
[Route("api/catalogo/categoria")]
public class CategoriaProductoController : ControllerBase
{
    private readonly ICategoriaProductoService _service;

    public CategoriaProductoController(ICategoriaProductoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategoria([FromBody] CategoriaProductoCreateDto dto)
    {
        try
        {
            await _service.SaveCategoriaAsync(dto);
            return Ok(new { mensaje = "Categoría creada con éxito" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategorias()
    {
        var categorias = await _service.GetAllCategoriasAsync();
        return Ok(categorias);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        var categoria = await _service.GetByIdAsync(id);
        if (categoria == null)
        {
            return NotFound(new { error = "Categoría no encontrada." });
        }

        return Ok(categoria);
    }

    [HttpGet("nombre/{nombre}")]
    public async Task<IActionResult> GetByNombre([FromRoute] string nombre)
    {
        var categoria = await _service.GetCategoriaEntity(nombre);
        if (categoria == null)
        {
            return NotFound(new { error = "Categoría no encontrada." });
        }

        return Ok(categoria);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateCategoria([FromRoute] long id, [FromBody] CategoriaProductoUpdateDto dto)
    {
        try
        {
            await _service.UpdateCategoriaAsync(id, dto);
            return Ok(new { mensaje = "Categoría actualizada con éxito" });
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
    public async Task<IActionResult> DeleteCategoria([FromRoute] long id)
    {
        try
        {
            await _service.DeleteCategoriaAsync(id);
            return Ok(new { mensaje = "Categoría eliminada con éxito" });
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
