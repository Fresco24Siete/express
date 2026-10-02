using Ventas.Api.Interfaces;
using Ventas.Api.Models.Dtos;
using Ventas.Api.Models.Entities;

namespace Ventas.Api.Services;

public class ItemCarritoService : IItemCarritoService
{
    private readonly IItemCarritoRepository _repository;
    private readonly ICarritoRepository _carritoRepository;

    public ItemCarritoService(IItemCarritoRepository repository, ICarritoRepository carritoRepository)
    {
        _repository = repository;
        _carritoRepository = carritoRepository;
    }

    public async Task<ItemCarritoResponseDto> CreateAsync(ItemCarritoCreateDto dto)
    {
        var carrito = await _carritoRepository.GetByIdAsync(dto.IdCarrito, includeItems: false);
        if (carrito == null)
        {
            throw new KeyNotFoundException($"Carrito con ID {dto.IdCarrito} no encontrado.");
        }

        // Si ya existe el producto en el carrito, sumamos la cantidad
        var existingItem = await _repository.GetByCarritoAndProductoAsync(dto.IdCarrito, dto.IdProducto);
        if (existingItem != null)
        {
            existingItem.Cantidad += dto.Cantidad;
            existingItem.PrecioUnitario = dto.PrecioUnitario;
            existingItem.UpdatedAt = DateTimeOffset.UtcNow;
            await _repository.UpdateAsync(existingItem);
            await RecalculateCart(dto.IdCarrito);
            return MapToResponse(existingItem);
        }

        var entity = new ItemCarritoEntity
        {
            IdItemCarrito = Guid.NewGuid(),
            IdCarrito = dto.IdCarrito,
            IdProducto = dto.IdProducto,
            Cantidad = dto.Cantidad,
            PrecioUnitario = dto.PrecioUnitario,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        var created = await _repository.CreateAsync(entity);
        await RecalculateCart(dto.IdCarrito);
        return MapToResponse(created);
    }

    public async Task<ItemCarritoResponseDto?> GetByIdAsync(Guid idItemCarrito)
    {
        var item = await _repository.GetByIdAsync(idItemCarrito);
        return item == null ? null : MapToResponse(item);
    }

    public async Task<IEnumerable<ItemCarritoResponseDto>> GetByCarritoIdAsync(long idCarrito)
    {
        var items = await _repository.GetByCarritoIdAsync(idCarrito);
        return items.Select(MapToResponse);
    }

    public async Task<ItemCarritoResponseDto> UpdateAsync(Guid idItemCarrito, ItemCarritoUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(idItemCarrito);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Item de carrito con ID {idItemCarrito} no encontrado.");
        }

        if (dto.Cantidad.HasValue)
            existing.Cantidad = dto.Cantidad.Value;

        if (dto.PrecioUnitario.HasValue)
            existing.PrecioUnitario = dto.PrecioUnitario.Value;

        existing.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(existing);
        await RecalculateCart(existing.IdCarrito);
        return MapToResponse(existing);
    }

    public async Task DeleteAsync(Guid idItemCarrito)
    {
        var existing = await _repository.GetByIdAsync(idItemCarrito);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Item de carrito con ID {idItemCarrito} no encontrado.");
        }

        var idCarrito = existing.IdCarrito;
        await _repository.DeleteAsync(idItemCarrito);
        await RecalculateCart(idCarrito);
    }

    public async Task ClearCarritoAsync(long idCarrito)
    {
        var carrito = await _carritoRepository.GetByIdAsync(idCarrito, includeItems: false);
        if (carrito == null)
        {
            throw new KeyNotFoundException($"Carrito con ID {idCarrito} no encontrado.");
        }

        await _repository.DeleteByCarritoIdAsync(idCarrito);
        await RecalculateCart(idCarrito);
    }

    private async Task RecalculateCart(long idCarrito)
    {
        var carrito = await _carritoRepository.GetByIdAsync(idCarrito, includeItems: true);
        if (carrito == null) return;

        var items = await _repository.GetByCarritoIdAsync(idCarrito);
        carrito.NumeroArticulos = items.Sum(i => i.Cantidad);
        carrito.PrecioTotal = items.Sum(i => i.Cantidad * i.PrecioUnitario);
        carrito.UpdatedAt = DateTimeOffset.UtcNow;
        await _carritoRepository.UpdateAsync(carrito);
    }

    private static ItemCarritoResponseDto MapToResponse(ItemCarritoEntity entity)
    {
        return new ItemCarritoResponseDto
        {
            IdItemCarrito = entity.IdItemCarrito,
            IdCarrito = entity.IdCarrito,
            IdProducto = entity.IdProducto,
            Cantidad = entity.Cantidad,
            PrecioUnitario = entity.PrecioUnitario,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
