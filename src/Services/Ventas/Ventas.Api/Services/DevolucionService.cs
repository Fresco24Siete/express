using Ventas.Api.Interfaces;
using Ventas.Api.Models.Dtos;
using Ventas.Api.Models.Entities;
using Ventas.Api.Models.Enums;

namespace Ventas.Api.Services;

public class DevolucionService : IDevolucionService
{
    private readonly IDevolucionRepository _repository;
    private readonly IOrdenRepository _ordenRepository;
    private readonly IDetalleOrdenRepository _detalleRepository;

    public DevolucionService(
        IDevolucionRepository repository,
        IOrdenRepository ordenRepository,
        IDetalleOrdenRepository detalleRepository)
    {
        _repository = repository;
        _ordenRepository = ordenRepository;
        _detalleRepository = detalleRepository;
    }

    public async Task<DevolucionResponseDto> CreateAsync(DevolucionCreateDto dto)
    {
        var orden = await _ordenRepository.GetByIdAsync(dto.IdOrden, includeDetails: false);
        if (orden == null)
        {
            throw new KeyNotFoundException($"Orden con ID {dto.IdOrden} no encontrada.");
        }

        var detalle = await _detalleRepository.GetByIdAsync(dto.IdDetalleOrden);
        if (detalle == null)
        {
            throw new KeyNotFoundException($"Detalle de orden con ID {dto.IdDetalleOrden} no encontrado.");
        }

        if (detalle.IdOrden != dto.IdOrden)
        {
            throw new InvalidOperationException("El detalle de orden especificado no pertenece a la orden indicada.");
        }

        if (dto.CantidadDevuelta > detalle.Cantidad)
        {
            throw new InvalidOperationException($"La cantidad a devolver ({dto.CantidadDevuelta}) no puede superar la cantidad comprada ({detalle.Cantidad}).");
        }

        var montoReembolso = dto.MontoReembolso > 0
            ? dto.MontoReembolso
            : (dto.CantidadDevuelta * detalle.PrecioUnitario);

        var entity = new DevolucionEntity
        {
            IdDevolucion = Guid.NewGuid(),
            IdOrden = dto.IdOrden,
            IdDetalleOrden = dto.IdDetalleOrden,
            CantidadDevuelta = dto.CantidadDevuelta,
            Razon = dto.Razon,
            Estado = EstadoDevolucionEnum.solicitada,
            MontoReembolso = montoReembolso,
            FechaSolicitud = DateTimeOffset.UtcNow,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        var created = await _repository.CreateAsync(entity);
        return MapToResponse(created);
    }

    public async Task<DevolucionResponseDto?> GetByIdAsync(Guid idDevolucion)
    {
        var devolucion = await _repository.GetByIdAsync(idDevolucion);
        return devolucion == null ? null : MapToResponse(devolucion);
    }

    public async Task<IEnumerable<DevolucionResponseDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(MapToResponse);
    }

    public async Task<IEnumerable<DevolucionResponseDto>> GetByOrdenIdAsync(Guid idOrden)
    {
        var list = await _repository.GetByOrdenIdAsync(idOrden);
        return list.Select(MapToResponse);
    }

    public async Task<IEnumerable<DevolucionResponseDto>> GetByDetalleOrdenIdAsync(Guid idDetalleOrden)
    {
        var list = await _repository.GetByDetalleOrdenIdAsync(idDetalleOrden);
        return list.Select(MapToResponse);
    }

    public async Task<DevolucionResponseDto> UpdateAsync(Guid idDevolucion, DevolucionUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(idDevolucion);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Devolución con ID {idDevolucion} no encontrada.");
        }

        if (dto.Estado.HasValue)
        {
            existing.Estado = dto.Estado.Value;
            if ((dto.Estado.Value == EstadoDevolucionEnum.aprobada || dto.Estado.Value == EstadoDevolucionEnum.completada || dto.Estado.Value == EstadoDevolucionEnum.rechazada)
                && existing.FechaResolucion == null)
            {
                existing.FechaResolucion = DateTimeOffset.UtcNow;
            }
        }

        if (dto.MontoReembolso.HasValue)
            existing.MontoReembolso = dto.MontoReembolso.Value;

        if (dto.FechaResolucion.HasValue)
            existing.FechaResolucion = dto.FechaResolucion.Value;

        existing.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(existing);
        return MapToResponse(existing);
    }

    public async Task DeleteAsync(Guid idDevolucion)
    {
        var existing = await _repository.GetByIdAsync(idDevolucion);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Devolución con ID {idDevolucion} no encontrada.");
        }

        await _repository.DeleteAsync(idDevolucion);
    }

    private static DevolucionResponseDto MapToResponse(DevolucionEntity entity)
    {
        return new DevolucionResponseDto
        {
            IdDevolucion = entity.IdDevolucion,
            IdOrden = entity.IdOrden,
            IdDetalleOrden = entity.IdDetalleOrden,
            CantidadDevuelta = entity.CantidadDevuelta,
            Razon = entity.Razon,
            Estado = entity.Estado,
            MontoReembolso = entity.MontoReembolso,
            FechaSolicitud = entity.FechaSolicitud,
            FechaResolucion = entity.FechaResolucion,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
