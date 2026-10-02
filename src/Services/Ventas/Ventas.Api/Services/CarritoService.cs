using Ventas.Api.Interfaces;
using Ventas.Api.Models.Dtos;
using Ventas.Api.Models.Entities;

namespace Ventas.Api.Services;

public class CarritoService : ICarritoService
{
    private readonly ICarritoRepository _repository;
    private readonly IItemCarritoRepository _itemRepository;

    public CarritoService(ICarritoRepository repository, IItemCarritoRepository itemRepository)
    {
        _repository = repository;
        _itemRepository = itemRepository;
    }

    public async Task<CarritoResponseDto> CreateAsync(CarritoCreateDto dto)
    {
        var entity = new CarritoEntity
        {
            PrecioTotal = dto.PrecioTotal ?? 0.00m,
            NumeroArticulos = dto.NumeroArticulos ?? 0,
            ExpiradoEn = dto.ExpiradoEn,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        var created = await _repository.CreateAsync(entity);
        return MapToResponse(created);
    }

    public async Task<CarritoResponseDto?> GetByIdAsync(long idCarrito)
    {
        var entity = await _repository.GetByIdAsync(idCarrito, includeItems: true);
        return entity == null ? null : MapToResponse(entity);
    }

    public async Task<IEnumerable<CarritoResponseDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(MapToResponse);
    }

    public async Task<CarritoResponseDto> UpdateAsync(long idCarrito, CarritoUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(idCarrito, includeItems: true);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Carrito con ID {idCarrito} no encontrado.");
        }

        if (dto.PrecioTotal.HasValue)
            existing.PrecioTotal = dto.PrecioTotal.Value;

        if (dto.NumeroArticulos.HasValue)
            existing.NumeroArticulos = dto.NumeroArticulos.Value;

        if (dto.ExpiradoEn.HasValue)
            existing.ExpiradoEn = dto.ExpiradoEn.Value;

        existing.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(existing);
        return MapToResponse(existing);
    }

    public async Task DeleteAsync(long idCarrito)
    {
        var existing = await _repository.GetByIdAsync(idCarrito, includeItems: false);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Carrito con ID {idCarrito} no encontrado.");
        }

        await _repository.DeleteAsync(idCarrito);
    }

    public async Task RecalculateTotalsAsync(long idCarrito)
    {
        var existing = await _repository.GetByIdAsync(idCarrito, includeItems: true);
        if (existing == null) return;

        var items = await _itemRepository.GetByCarritoIdAsync(idCarrito);
        existing.NumeroArticulos = items.Sum(i => i.Cantidad);
        existing.PrecioTotal = items.Sum(i => i.Cantidad * i.PrecioUnitario);
        existing.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(existing);
    }

    private static CarritoResponseDto MapToResponse(CarritoEntity entity)
    {
        return new CarritoResponseDto
        {
            IdCarrito = entity.IdCarrito,
            PrecioTotal = entity.PrecioTotal,
            NumeroArticulos = entity.NumeroArticulos,
            ExpiradoEn = entity.ExpiradoEn,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Items = entity.Items?.Select(i => new ItemCarritoResponseDto
            {
                IdItemCarrito = i.IdItemCarrito,
                IdCarrito = i.IdCarrito,
                IdProducto = i.IdProducto,
                Cantidad = i.Cantidad,
                PrecioUnitario = i.PrecioUnitario,
                CreatedAt = i.CreatedAt,
                UpdatedAt = i.UpdatedAt
            }).ToList()
        };
    }
}
