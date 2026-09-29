using Catalogo.Api.Models.Dtos;
using Catalogo.Api.Models.Entities;

namespace Catalogo.Api.Interface;

public interface IDireccionTiendaService
{
    Task SaveDireccionAsync(DireccionTiendaCreateDto direccion);
    Task DeleteDireccionAsync(long idDireccion);
    Task UpdateDireccionAsync(long idDireccion, DireccionTiendaUpdateDto direccion);
    Task UpdateDireccionAsync(DireccionTiendaUpdateDto direccion);
    Task<DireccionTiendaEntity?> GetByIdAsync(long idDireccion);
    Task<DireccionTiendaEntity?> GetDireccionEntity(long idDireccion);
    Task<IEnumerable<DireccionTiendaEntity>> GetAllDireccionAsync();
}
