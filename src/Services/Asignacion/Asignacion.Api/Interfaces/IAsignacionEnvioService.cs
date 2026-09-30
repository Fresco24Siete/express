using Asignacion.Api.Models.Dtos;
using Asignacion.Api.Models.Entities;
using Asignacion.Api.Models.Enums;

namespace Asignacion.Api.Interfaces;

public interface IAsignacionEnvioService
{
    Task<AsignacionEnvioResponseDto> SaveAsignacionEnvioAsync(AsignacionEnvioCreateDto dto);
    Task DeleteAsignacionEnvioAsync(Guid idAsignacionEnvio);
    Task<AsignacionEnvioResponseDto> UpdateAsignacionEnvioAsync(Guid idAsignacionEnvio, AsignacionEnvioUpdateDto dto);
    Task<AsignacionEnvioResponseDto?> GetByIdAsync(Guid idAsignacionEnvio);
    Task<IEnumerable<AsignacionEnvioResponseDto>> GetAllAsync();
    Task<IEnumerable<AsignacionEnvioResponseDto>> GetByOrdenIdAsync(Guid idOrden);
    Task<IEnumerable<AsignacionEnvioResponseDto>> GetByDomiciliarioIdAsync(Guid idDomiciliario);
    Task<IEnumerable<AsignacionEnvioResponseDto>> GetByEstadoAsync(EstadoAsignacionEnvioEnum estado);
}
