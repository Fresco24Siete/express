using Servicio.Api.Models.Dtos;

namespace Servicio.Api.Interfaces;

public interface ICategoriaServicioService
{
    Task<CategoriaServicioResponseDto> CreateAsync(CategoriaServicioCreateDto dto);
    Task<CategoriaServicioResponseDto?> GetByIdAsync(long idCategoriaServicio);
    Task<IEnumerable<CategoriaServicioResponseDto>> GetAllAsync(bool? soloActivas = null);
    Task<CategoriaServicioResponseDto> UpdateAsync(long idCategoriaServicio, CategoriaServicioUpdateDto dto);
    Task DeleteAsync(long idCategoriaServicio);
}
