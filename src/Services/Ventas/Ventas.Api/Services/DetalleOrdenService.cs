using Ventas.Api.Interfaces;
using Ventas.Api.Models.Dtos;
using Ventas.Api.Models.Entities;

namespace Ventas.Api.Services;

public class DetalleOrdenService : IDetalleOrdenService
{
    private readonly IDetalleOrdenRepository _repository;
    private readonly IOrdenRepository _ordenRepository;

    public DetalleOrdenService(IDetalleOrdenRepository repository, IOrdenRepository ordenRepository)
    {
        _repository = repository;
        _ordenRepository = ordenRepository;
    }

    public async Task<DetalleOrdenResponseDto> CreateAsync(DetalleOrdenCreateDto dto)
    {
        if (!dto.IdOrden.HasValue)
        {
            throw new ArgumentException("El id_orden es obligatorio.");
        }

        var orden = await _ordenRepository.GetByIdAsync(dto.IdOrden.Value, includeDetails: false);
        if (orden == null)
        {
            throw new KeyNotFoundException($"Orden con ID {dto.IdOrden.Value} no encontrada.");
        }

        var subtotal = dto.Subtotal ?? (dto.Cantidad * dto.PrecioUnitario);

        var entity = new DetalleOrdenEntity
        {
            IdDetalleOrden = Guid.NewGuid(),
            IdOrden = dto.IdOrden.Value,
            IdProducto = dto.IdProducto,
            Cantidad = dto.Cantidad,
            PrecioUnitario = dto.PrecioUnitario,
            Subtotal = subtotal
        };

        var created = await _repository.CreateAsync(entity);
        return MapToResponse(created);
    }

    public async Task<DetalleOrdenResponseDto?> GetByIdAsync(Guid idDetalleOrden)
    {
        var detalle = await _repository.GetByIdAsync(idDetalleOrden);
        return detalle == null ? null : MapToResponse(detalle);
    }

    public async Task<IEnumerable<DetalleOrdenResponseDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(MapToResponse);
    }

    public async Task<IEnumerable<DetalleOrdenResponseDto>> GetByOrdenIdAsync(Guid idOrden)
    {
        var list = await _repository.GetByOrdenIdAsync(idOrden);
        return list.Select(MapToResponse);
    }

    public async Task<DetalleOrdenResponseDto> UpdateAsync(Guid idDetalleOrden, DetalleOrdenUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(idDetalleOrden);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Detalle de orden con ID {idDetalleOrden} no encontrado.");
        }

        if (dto.Cantidad.HasValue)
            existing.Cantidad = dto.Cantidad.Value;

        if (dto.PrecioUnitario.HasValue)
            existing.PrecioUnitario = dto.PrecioUnitario.Value;

        if (dto.Subtotal.HasValue)
            existing.Subtotal = dto.Subtotal.Value;
        else if (dto.Cantidad.HasValue || dto.PrecioUnitario.HasValue)
            existing.Subtotal = existing.Cantidad * existing.PrecioUnitario;

        await _repository.UpdateAsync(existing);
        return MapToResponse(existing);
    }

    public async Task DeleteAsync(Guid idDetalleOrden)
    {
        var existing = await _repository.GetByIdAsync(idDetalleOrden);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Detalle de orden con ID {idDetalleOrden} no encontrado.");
        }

        await _repository.DeleteAsync(idDetalleOrden);
    }

    private static DetalleOrdenResponseDto MapToResponse(DetalleOrdenEntity entity)
    {
        return new DetalleOrdenResponseDto
        {
            IdDetalleOrden = entity.IdDetalleOrden,
            IdOrden = entity.IdOrden,
            IdProducto = entity.IdProducto,
            Cantidad = entity.Cantidad,
            PrecioUnitario = entity.PrecioUnitario,
            Subtotal = entity.Subtotal
        };
    }
}
