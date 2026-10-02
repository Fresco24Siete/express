using Servicio.Api.Models.Dtos;
using Servicio.Api.Models.Enums;

namespace Servicio.Api.Interfaces;

public interface IServicioService
{
    Task<ServicioResponseDto> CreateAsync(ServicioCreateDto dto);
    Task<ServicioResponseDto?> GetByIdAsync(Guid idServicio);
    Task<IEnumerable<ServicioResponseDto>> GetAllAsync(Guid? idCliente = null, Guid? idEmprendedor = null, long? idCategoria = null, EstadoServicioEnum? estado = null);
    Task<IEnumerable<ServicioResponseDto>> GetByClienteIdAsync(Guid idCliente);
    Task<IEnumerable<ServicioResponseDto>> GetByEmprendedorIdAsync(Guid idEmprendedor);
    Task<ServicioResponseDto> UpdateAsync(Guid idServicio, ServicioUpdateDto dto);
    Task<ServicioResponseDto> CambiarEstadoAsync(Guid idServicio, ServicioCambiarEstadoDto dto);
    Task DeleteAsync(Guid idServicio);
}
