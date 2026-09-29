using Catalogo.Api.Interface;
using Catalogo.Api.Models.Dtos;
using Catalogo.Api.Models.Entities;

namespace Catalogo.Api.Services;

public class CategoriaProductoService : ICategoriaProductoService
{
    private readonly ICategoriaProductoRepository _repository;

    public CategoriaProductoService(ICategoriaProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task SaveCategoriaAsync(CategoriaProductoCreateDto categoria)
    {
        var entity = new CategoriaProductoEntity
        {
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion,
            Activa = categoria.Activa
        };

        await _repository.SaveCategoriaAsync(entity);
    }

    public async Task DeleteCategoriaAsync(long idCategoria)
    {
        var existing = await _repository.GetByIdAsync(idCategoria);
        if (existing == null)
        {
            throw new KeyNotFoundException("Categoría de producto no encontrada.");
        }

        await _repository.DeleteCategoriaAsync(idCategoria);
    }

    public async Task UpdateCategoriaAsync(long idCategoria, CategoriaProductoUpdateDto categoria)
    {
        var existing = await _repository.GetByIdAsync(idCategoria);
        if (existing == null)
        {
            throw new KeyNotFoundException("Categoría de producto no encontrada.");
        }

        if (!string.IsNullOrWhiteSpace(categoria.Nombre))
            existing.Nombre = categoria.Nombre;

        if (categoria.Descripcion != null)
            existing.Descripcion = categoria.Descripcion;

        if (categoria.Activa.HasValue)
            existing.Activa = categoria.Activa.Value;

        await _repository.UpdateCategoriaAsync(existing);
    }

    public async Task UpdateCategoriaAsync(CategoriaProductoUpdateDto categoria)
    {
        if (!categoria.IdCategoriaProducto.HasValue)
        {
            throw new ArgumentException("El IdCategoriaProducto es requerido para actualizar.");
        }

        await UpdateCategoriaAsync(categoria.IdCategoriaProducto.Value, categoria);
    }

    public async Task<CategoriaProductoEntity?> GetByIdAsync(long idCategoria)
    {
        return await _repository.GetByIdAsync(idCategoria);
    }

    public async Task<CategoriaProductoEntity?> GetCategoriaEntity(string nombre)
    {
        return await _repository.GetCategoriaEntity(nombre);
    }

    public async Task<IEnumerable<CategoriaProductoEntity>> GetAllCategoriasAsync()
    {
        return await _repository.GetAllCategoriasAsync();
    }
}
