using System.Text.RegularExpressions;
using Catalogo.Api.Interface;
using Catalogo.Api.Models.Dtos;
using Catalogo.Api.Models.Entities;

namespace Catalogo.Api.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _repository;

    public ProductoService(IProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task SaveProductoAsync(ProductoCreateDto producto)
    {
        var slug = !string.IsNullOrWhiteSpace(producto.Slug)
            ? producto.Slug
            : GenerateSlug(producto.Nombre);

        var entity = new ProductoEntity
        {
            IdProducto = Guid.NewGuid(),
            IdTienda = producto.IdTienda,
            IdCategoriaProducto = producto.IdCategoriaProducto,
            Nombre = producto.Nombre,
            Slug = slug,
            Sku = producto.Sku,
            Descripcion = producto.Descripcion,
            CantidadDisponibles = producto.CantidadDisponibles,
            CantidadReservadas = 0,
            CantidadVendidos = 0,
            Precio = producto.Precio,
            PrecioOriginal = producto.PrecioOriginal,
            DescuentoPorcentaje = producto.DescuentoPorcentaje,
            PesoKg = producto.PesoKg,
            ImagenPrincipal = producto.ImagenPrincipal,
            Estado = producto.Estado,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        await _repository.SaveProductoAsync(entity);
    }

    public async Task DeleteProductoAsync(Guid idProducto)
    {
        var existing = await _repository.GetByIdAsync(idProducto);
        if (existing == null)
        {
            throw new KeyNotFoundException("Producto no encontrado.");
        }

        await _repository.DeleteProductoAsync(idProducto);
    }

    public async Task UpdateProductoAsync(Guid idProducto, ProductoUpdateDto producto)
    {
        var existing = await _repository.GetByIdAsync(idProducto);
        if (existing == null)
        {
            throw new KeyNotFoundException("Producto no encontrado.");
        }

        if (producto.IdTienda.HasValue)
            existing.IdTienda = producto.IdTienda.Value;

        if (producto.IdCategoriaProducto.HasValue)
            existing.IdCategoriaProducto = producto.IdCategoriaProducto.Value;

        if (!string.IsNullOrWhiteSpace(producto.Nombre))
            existing.Nombre = producto.Nombre;

        if (!string.IsNullOrWhiteSpace(producto.Slug))
            existing.Slug = producto.Slug;

        if (!string.IsNullOrWhiteSpace(producto.Sku))
            existing.Sku = producto.Sku;

        if (producto.Descripcion != null)
            existing.Descripcion = producto.Descripcion;

        if (producto.CantidadDisponibles.HasValue)
            existing.CantidadDisponibles = producto.CantidadDisponibles.Value;

        if (producto.CantidadReservadas.HasValue)
            existing.CantidadReservadas = producto.CantidadReservadas.Value;

        if (producto.CantidadVendidos.HasValue)
            existing.CantidadVendidos = producto.CantidadVendidos.Value;

        if (producto.Precio.HasValue)
            existing.Precio = producto.Precio.Value;

        if (producto.PrecioOriginal.HasValue)
            existing.PrecioOriginal = producto.PrecioOriginal.Value;

        if (producto.DescuentoPorcentaje.HasValue)
            existing.DescuentoPorcentaje = producto.DescuentoPorcentaje.Value;

        if (producto.PesoKg.HasValue)
            existing.PesoKg = producto.PesoKg.Value;

        if (producto.ImagenPrincipal != null)
            existing.ImagenPrincipal = producto.ImagenPrincipal;

        if (producto.Estado.HasValue)
            existing.Estado = producto.Estado.Value;

        existing.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateProductoAsync(existing);
    }

    public async Task UpdateProductoAsync(ProductoUpdateDto producto)
    {
        if (!producto.IdProducto.HasValue)
        {
            throw new ArgumentException("El IdProducto es requerido para actualizar.");
        }

        await UpdateProductoAsync(producto.IdProducto.Value, producto);
    }

    public async Task<ProductoEntity?> GetByIdAsync(Guid idProducto)
    {
        return await _repository.GetByIdAsync(idProducto);
    }

    public async Task<ProductoEntity?> GetProductoEntity(string nombre)
    {
        return await _repository.GetProductoEntity(nombre);
    }

    public async Task<IEnumerable<ProductoEntity>> GetAllProductoAsync()
    {
        return await _repository.GetAllProductoAsync();
    }

    public async Task<IEnumerable<ProductoEntity>> GetByTiendaIdAsync(Guid idTienda)
    {
        return await _repository.GetByTiendaIdAsync(idTienda);
    }

    public async Task<IEnumerable<ProductoEntity>> GetByCategoriaIdAsync(long idCategoria)
    {
        return await _repository.GetByCategoriaIdAsync(idCategoria);
    }

    public async Task<ProductoEntity?> GetBySlugAsync(string slug)
    {
        return await _repository.GetBySlugAsync(slug);
    }

    public async Task<ProductoEntity?> GetBySkuAsync(string sku)
    {
        return await _repository.GetBySkuAsync(sku);
    }

    private static string GenerateSlug(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return Guid.NewGuid().ToString("N")[..8];

        var slug = text.ToLowerInvariant().Trim();
        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
        slug = Regex.Replace(slug, @"\s+", "-").Trim('-');

        return string.IsNullOrWhiteSpace(slug) ? Guid.NewGuid().ToString("N")[..8] : slug;
    }
}
