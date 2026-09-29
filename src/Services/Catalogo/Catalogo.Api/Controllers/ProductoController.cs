using Catalogo.Api.Interface;
using Catalogo.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.Controllers;

[ApiController]
[Route("api/catalogo/producto")]
public class ProductoController : ControllerBase
{
    private readonly IProductoService _service;

    public ProductoController(IProductoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProducto([FromBody] ProductoCreateDto dto)
    {
        try
        {
            await _service.SaveProductoAsync(dto);
            return Ok(new { mensaje = "Producto creado con éxito" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error interno en el servidor", detalle = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductos()
    {
        var productos = await _service.GetAllProductoAsync();
        return Ok(productos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var producto = await _service.GetByIdAsync(id);
        if (producto == null)
        {
            return NotFound(new { error = "Producto no encontrado." });
        }

        return Ok(producto);
    }

    [HttpGet("nombre/{nombre}")]
    public async Task<IActionResult> GetByNombre([FromRoute] string nombre)
    {
        var producto = await _service.GetProductoEntity(nombre);
        if (producto == null)
        {
            return NotFound(new { error = "Producto no encontrado." });
        }

        return Ok(producto);
    }

    [HttpGet("tienda/{idTienda:guid}")]
    public async Task<IActionResult> GetByTiendaId([FromRoute] Guid idTienda)
    {
        var productos = await _service.GetByTiendaIdAsync(idTienda);
        return Ok(productos);
    }

    [HttpGet("categoria/{idCategoria:long}")]
    public async Task<IActionResult> GetByCategoriaId([FromRoute] long idCategoria)
    {
        var productos = await _service.GetByCategoriaIdAsync(idCategoria);
        return Ok(productos);
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug([FromRoute] string slug)
    {
        var producto = await _service.GetBySlugAsync(slug);
        if (producto == null)
        {
            return NotFound(new { error = "Producto no encontrado." });
        }

        return Ok(producto);
    }

    [HttpGet("sku/{sku}")]
    public async Task<IActionResult> GetBySku([FromRoute] string sku)
    {
        var producto = await _service.GetBySkuAsync(sku);
        if (producto == null)
        {
            return NotFound(new { error = "Producto no encontrado." });
        }

        return Ok(producto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProducto([FromRoute] Guid id, [FromBody] ProductoUpdateDto dto)
    {
        try
        {
            await _service.UpdateProductoAsync(id, dto);
            return Ok(new { mensaje = "Producto actualizado con éxito" });
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
    public async Task<IActionResult> DeleteProducto([FromRoute] Guid id)
    {
        try
        {
            await _service.DeleteProductoAsync(id);
            return Ok(new { mensaje = "Producto eliminado con éxito" });
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
