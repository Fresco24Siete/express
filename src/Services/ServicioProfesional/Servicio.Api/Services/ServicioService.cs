using Servicio.Api.Interfaces;
using Servicio.Api.Models.Dtos;
using Servicio.Api.Models.Entities;
using Servicio.Api.Models.Enums;

namespace Servicio.Api.Services;

public class ServicioService : IServicioService
{
    private readonly IServicioRepository _repository;

    public ServicioService(IServicioRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServicioResponseDto> CreateAsync(ServicioCreateDto dto)
    {
        var costoBase = dto.CostoBase;
        var costoAdicional = dto.CostoAdicional ?? 0.00m;
        var costoTotal = costoBase + costoAdicional;

        var entity = new ServicioEntity
        {
            IdServicio = Guid.NewGuid(),
            IdCliente = dto.IdCliente,
            IdEmprendedor = dto.IdEmprendedor,
            IdCategoriaServicio = dto.IdCategoriaServicio,
            DescripcionSolicitud = dto.DescripcionSolicitud.Trim(),
            UbicacionLat = dto.UbicacionLat,
            UbicacionLon = dto.UbicacionLon,
            FechaEstimadaInicio = dto.FechaEstimadaInicio,
            FechaRealInicio = null,
            DuracionEstimadaHoras = dto.DuracionEstimadaHoras,
            DuracionRealHoras = null,
            CostoBase = costoBase,
            CostoAdicional = costoAdicional,
            CostoTotal = costoTotal,
            Estado = EstadoServicioEnum.solicitado,
            RazonCancelacion = null,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        var created = await _repository.CreateAsync(entity);
        return MapToResponse(created);
    }

    public async Task<ServicioResponseDto?> GetByIdAsync(Guid idServicio)
    {
        var entity = await _repository.GetByIdAsync(idServicio);
        return entity == null ? null : MapToResponse(entity);
    }

    public async Task<IEnumerable<ServicioResponseDto>> GetAllAsync(
        Guid? idCliente = null,
        Guid? idEmprendedor = null,
        long? idCategoria = null,
        EstadoServicioEnum? estado = null)
    {
        var list = await _repository.GetAllAsync(idCliente, idEmprendedor, idCategoria, estado);
        return list.Select(MapToResponse);
    }

    public async Task<IEnumerable<ServicioResponseDto>> GetByClienteIdAsync(Guid idCliente)
    {
        var list = await _repository.GetByClienteIdAsync(idCliente);
        return list.Select(MapToResponse);
    }

    public async Task<IEnumerable<ServicioResponseDto>> GetByEmprendedorIdAsync(Guid idEmprendedor)
    {
        var list = await _repository.GetByEmprendedorIdAsync(idEmprendedor);
        return list.Select(MapToResponse);
    }

    public async Task<ServicioResponseDto> UpdateAsync(Guid idServicio, ServicioUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(idServicio);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Servicio con ID {idServicio} no encontrado.");
        }

        if (dto.IdCategoriaServicio.HasValue)
            existing.IdCategoriaServicio = dto.IdCategoriaServicio.Value;

        if (!string.IsNullOrWhiteSpace(dto.DescripcionSolicitud))
            existing.DescripcionSolicitud = dto.DescripcionSolicitud.Trim();

        if (dto.UbicacionLat.HasValue)
            existing.UbicacionLat = dto.UbicacionLat.Value;

        if (dto.UbicacionLon.HasValue)
            existing.UbicacionLon = dto.UbicacionLon.Value;

        if (dto.FechaEstimadaInicio.HasValue)
            existing.FechaEstimadaInicio = dto.FechaEstimadaInicio.Value;

        if (dto.FechaRealInicio.HasValue)
            existing.FechaRealInicio = dto.FechaRealInicio.Value;

        if (dto.DuracionEstimadaHoras.HasValue)
            existing.DuracionEstimadaHoras = dto.DuracionEstimadaHoras.Value;

        if (dto.DuracionRealHoras.HasValue)
            existing.DuracionRealHoras = dto.DuracionRealHoras.Value;

        if (dto.CostoBase.HasValue)
            existing.CostoBase = dto.CostoBase.Value;

        if (dto.CostoAdicional.HasValue)
            existing.CostoAdicional = dto.CostoAdicional.Value;

        if (dto.CostoTotal.HasValue)
        {
            existing.CostoTotal = dto.CostoTotal.Value;
        }
        else if (dto.CostoBase.HasValue || dto.CostoAdicional.HasValue)
        {
            existing.CostoTotal = existing.CostoBase + existing.CostoAdicional;
        }

        if (dto.Estado.HasValue)
            existing.Estado = dto.Estado.Value;

        if (dto.RazonCancelacion != null)
            existing.RazonCancelacion = dto.RazonCancelacion.Trim();

        existing.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(existing);
        return MapToResponse(existing);
    }

    public async Task<ServicioResponseDto> CambiarEstadoAsync(Guid idServicio, ServicioCambiarEstadoDto dto)
    {
        var existing = await _repository.GetByIdAsync(idServicio);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Servicio con ID {idServicio} no encontrado.");
        }

        existing.Estado = dto.Estado;

        if (dto.Estado == EstadoServicioEnum.cancelado || dto.Estado == EstadoServicioEnum.rechazado)
        {
            if (!string.IsNullOrWhiteSpace(dto.RazonCancelacion))
            {
                existing.RazonCancelacion = dto.RazonCancelacion.Trim();
            }
        }

        if (dto.Estado == EstadoServicioEnum.en_proceso)
        {
            existing.FechaRealInicio = dto.FechaRealInicio ?? existing.FechaRealInicio ?? DateTimeOffset.UtcNow;
        }

        if (dto.Estado == EstadoServicioEnum.completado)
        {
            if (dto.DuracionRealHoras.HasValue)
            {
                existing.DuracionRealHoras = dto.DuracionRealHoras.Value;
            }
        }

        existing.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(existing);
        return MapToResponse(existing);
    }

    public async Task DeleteAsync(Guid idServicio)
    {
        var exists = await _repository.ExistsAsync(idServicio);
        if (!exists)
        {
            throw new KeyNotFoundException($"Servicio con ID {idServicio} no encontrado.");
        }

        await _repository.DeleteAsync(idServicio);
    }

    private static ServicioResponseDto MapToResponse(ServicioEntity entity)
    {
        return new ServicioResponseDto
        {
            IdServicio = entity.IdServicio,
            IdCliente = entity.IdCliente,
            IdEmprendedor = entity.IdEmprendedor,
            IdCategoriaServicio = entity.IdCategoriaServicio,
            DescripcionSolicitud = entity.DescripcionSolicitud,
            UbicacionLat = entity.UbicacionLat,
            UbicacionLon = entity.UbicacionLon,
            FechaEstimadaInicio = entity.FechaEstimadaInicio,
            FechaRealInicio = entity.FechaRealInicio,
            DuracionEstimadaHoras = entity.DuracionEstimadaHoras,
            DuracionRealHoras = entity.DuracionRealHoras,
            CostoBase = entity.CostoBase,
            CostoAdicional = entity.CostoAdicional,
            CostoTotal = entity.CostoTotal,
            Estado = entity.Estado,
            RazonCancelacion = entity.RazonCancelacion,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
