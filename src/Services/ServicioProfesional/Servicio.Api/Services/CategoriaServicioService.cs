using Servicio.Api.Interfaces;
using Servicio.Api.Models.Dtos;
using Servicio.Api.Models.Entities;

namespace Servicio.Api.Services;

public class CategoriaServicioService : ICategoriaServicioService
{
    private readonly ICategoriaServicioRepository _repository;

    public CategoriaServicioService(ICategoriaServicioRepository repository)
    {
        _repository = repository;
    }

    public async Task<CategoriaServicioResponseDto> CreateAsync(CategoriaServicioCreateDto dto)
    {
        var entity = new CategoriaServicioEntity
        {
            Nombre = dto.Nombre.Trim(),
            Descripcion = dto.Descripcion?.Trim(),
            DuracionPromedioHoras = dto.DuracionPromedioHoras,
            RequiereUbicacion = dto.RequiereUbicacion ?? true,
            PrecioBaseSugerido = dto.PrecioBaseSugerido ?? 0.00m,
            Activa = dto.Activa ?? true,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        var created = await _repository.CreateAsync(entity);
        return MapToResponse(created);
    }

    public async Task<CategoriaServicioResponseDto?> GetByIdAsync(long idCategoriaServicio)
    {
        var entity = await _repository.GetByIdAsync(idCategoriaServicio);
        return entity == null ? null : MapToResponse(entity);
    }

    public async Task<IEnumerable<CategoriaServicioResponseDto>> GetAllAsync(bool? soloActivas = null)
    {
        var list = await _repository.GetAllAsync(soloActivas);
        return list.Select(MapToResponse);
    }

    public async Task<CategoriaServicioResponseDto> UpdateAsync(long idCategoriaServicio, CategoriaServicioUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(idCategoriaServicio);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Categoría de servicio con ID {idCategoriaServicio} no encontrada.");
        }

        if (!string.IsNullOrWhiteSpace(dto.Nombre))
            existing.Nombre = dto.Nombre.Trim();

        if (dto.Descripcion != null)
            existing.Descripcion = dto.Descripcion.Trim();

        if (dto.DuracionPromedioHoras.HasValue)
            existing.DuracionPromedioHoras = dto.DuracionPromedioHoras.Value;

        if (dto.RequiereUbicacion.HasValue)
            existing.RequiereUbicacion = dto.RequiereUbicacion.Value;

        if (dto.PrecioBaseSugerido.HasValue)
            existing.PrecioBaseSugerido = dto.PrecioBaseSugerido.Value;

        if (dto.Activa.HasValue)
            existing.Activa = dto.Activa.Value;

        existing.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(existing);
        return MapToResponse(existing);
    }

    public async Task DeleteAsync(long idCategoriaServicio)
    {
        var exists = await _repository.ExistsAsync(idCategoriaServicio);
        if (!exists)
        {
            throw new KeyNotFoundException($"Categoría de servicio con ID {idCategoriaServicio} no encontrada.");
        }

        await _repository.DeleteAsync(idCategoriaServicio);
    }

    private static CategoriaServicioResponseDto MapToResponse(CategoriaServicioEntity entity)
    {
        return new CategoriaServicioResponseDto
        {
            IdCategoriaServicio = entity.IdCategoriaServicio,
            Nombre = entity.Nombre,
            Descripcion = entity.Descripcion,
            DuracionPromedioHoras = entity.DuracionPromedioHoras,
            RequiereUbicacion = entity.RequiereUbicacion,
            PrecioBaseSugerido = entity.PrecioBaseSugerido,
            Activa = entity.Activa,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
