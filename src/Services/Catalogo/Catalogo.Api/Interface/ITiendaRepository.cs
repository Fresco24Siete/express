using Catalogo.Api.Models.Entities;

namespace Catalogo.Api.Interface;

public interface ITiendaRepository
{
    Task SaveTiendaAsync(TiendaEntity tienda);
    Task DeleteTiendaAsync(Guid idTienda);
    Task UpdateTiendaAsync(TiendaEntity tienda);
    Task<TiendaEntity?> GetByIdAsync(Guid idTienda);
    Task<TiendaEntity?> GetTiendaEntity(string nombre);
    Task<IEnumerable<TiendaEntity>> GetAllTiendasAsync();
}