using Catalogo.Api.Models.Entities;

namespace Catalogo.Api.Interface;

public interface IDireccionTiendaRepository
{
    Task SaveDireccionAsync(DireccionTiendaEntity direccionTienda);
    Task DeleteDireccionAsync(long idDireccion);
    Task UpdateDireccionAsync(DireccionTiendaEntity direccionTienda);
    Task<DireccionTiendaEntity?> GetByIdAsync(long idDireccion);
    Task<DireccionTiendaEntity?> GetDireccionEntity(long idDireccion);
    Task<IEnumerable<DireccionTiendaEntity>> GetAllDireccionAsync();
}