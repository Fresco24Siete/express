using Asignacion.Api.Interfaces;
using Asignacion.Api.Models.Dtos;
using Asignacion.Api.Models.Entities;
using Asignacion.Api.Models.Enums;

namespace Asignacion.Api.Services;

public class AsignacionEnvioService : IAsignacionEnvioService
{
    private readonly IAsignacionEnvioRepository _repository;

    public AsignacionEnvioService(IAsignacionEnvioRepository repository)
    {
        _repository = repository;
    }

    public async Task<AsignacionEnvioResponseDto> SaveAsignacionEnvioAsync(AsignacionEnvioCreateDto dto)
    {
        var now = DateTimeOffset.UtcNow;

        var entity = new AsignacionEnvioEntity
        {
            IdAsignacionEnvio = Guid.NewGuid(),
            IdOrden = dto.IdOrden,
            IdDomiciliario = dto.IdDomiciliario,
            Estado = dto.Estado,
            FechaAsignacion = dto.FechaAsignacion ?? now,
            FechaEstimadaEntrega = dto.FechaEstimadaEntrega,
            FechaRealEntrega = dto.FechaRealEntrega,
            LatitudActual = dto.LatitudActual,
            LongitudActual = dto.LongitudActual,
            DistanciaKm = dto.DistanciaKm,
            TiempoEstimadoMin = dto.TiempoEstimadoMin,
            NumeroIntento = dto.NumeroIntento > 0 ? dto.NumeroIntento : 1,
            RazonFallo = dto.RazonFallo,
            FirmaCliente = dto.FirmaCliente,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _repository.SaveAsignacionEnvioAsync(entity);

        return MapToResponseDto(entity);
    }

    public async Task DeleteAsignacionEnvioAsync(Guid idAsignacionEnvio)
    {
        var existing = await _repository.GetByIdAsync(idAsignacionEnvio);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Asignación de envío con ID '{idAsignacionEnvio}' no encontrada.");
        }

        await _repository.DeleteAsignacionEnvioAsync(idAsignacionEnvio);
    }

    public async Task<AsignacionEnvioResponseDto> UpdateAsignacionEnvioAsync(Guid idAsignacionEnvio, AsignacionEnvioUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(idAsignacionEnvio);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Asignación de envío con ID '{idAsignacionEnvio}' no encontrada.");
        }

        if (dto.IdOrden.HasValue)
            existing.IdOrden = dto.IdOrden.Value;

        if (dto.IdDomiciliario.HasValue)
            existing.IdDomiciliario = dto.IdDomiciliario.Value;

        if (dto.Estado.HasValue)
            existing.Estado = dto.Estado.Value;

        if (dto.FechaAsignacion.HasValue)
            existing.FechaAsignacion = dto.FechaAsignacion.Value;

        if (dto.FechaEstimadaEntrega.HasValue)
            existing.FechaEstimadaEntrega = dto.FechaEstimadaEntrega;

        if (dto.FechaRealEntrega.HasValue)
            existing.FechaRealEntrega = dto.FechaRealEntrega;

        if (dto.LatitudActual.HasValue)
            existing.LatitudActual = dto.LatitudActual;

        if (dto.LongitudActual.HasValue)
            existing.LongitudActual = dto.LongitudActual;

        if (dto.DistanciaKm.HasValue)
            existing.DistanciaKm = dto.DistanciaKm;

        if (dto.TiempoEstimadoMin.HasValue)
            existing.TiempoEstimadoMin = dto.TiempoEstimadoMin;

        if (dto.NumeroIntento.HasValue)
            existing.NumeroIntento = dto.NumeroIntento.Value;

        if (dto.RazonFallo != null)
            existing.RazonFallo = dto.RazonFallo;

        if (dto.FirmaCliente != null)
            existing.FirmaCliente = dto.FirmaCliente;

        existing.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsignacionEnvioAsync(existing);

        return MapToResponseDto(existing);
    }

    public async Task<AsignacionEnvioResponseDto?> GetByIdAsync(Guid idAsignacionEnvio)
    {
        var entity = await _repository.GetByIdAsync(idAsignacionEnvio);
        return entity == null ? null : MapToResponseDto(entity);
    }

    public async Task<IEnumerable<AsignacionEnvioResponseDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<AsignacionEnvioResponseDto>> GetByOrdenIdAsync(Guid idOrden)
    {
        var entities = await _repository.GetByOrdenIdAsync(idOrden);
        return entities.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<AsignacionEnvioResponseDto>> GetByDomiciliarioIdAsync(Guid idDomiciliario)
    {
        var entities = await _repository.GetByDomiciliarioIdAsync(idDomiciliario);
        return entities.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<AsignacionEnvioResponseDto>> GetByEstadoAsync(EstadoAsignacionEnvioEnum estado)
    {
        var entities = await _repository.GetByEstadoAsync(estado);
        return entities.Select(MapToResponseDto);
    }

    private static AsignacionEnvioResponseDto MapToResponseDto(AsignacionEnvioEntity entity)
    {
        return new AsignacionEnvioResponseDto
        {
            IdAsignacionEnvio = entity.IdAsignacionEnvio,
            IdOrden = entity.IdOrden,
            IdDomiciliario = entity.IdDomiciliario,
            Estado = entity.Estado,
            FechaAsignacion = entity.FechaAsignacion,
            FechaEstimadaEntrega = entity.FechaEstimadaEntrega,
            FechaRealEntrega = entity.FechaRealEntrega,
            LatitudActual = entity.LatitudActual,
            LongitudActual = entity.LongitudActual,
            DistanciaKm = entity.DistanciaKm,
            TiempoEstimadoMin = entity.TiempoEstimadoMin,
            NumeroIntento = entity.NumeroIntento,
            RazonFallo = entity.RazonFallo,
            FirmaCliente = entity.FirmaCliente,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
