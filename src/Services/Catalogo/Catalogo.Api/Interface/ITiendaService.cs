using Catalogo.Api.Models.Dtos;
using Catalogo.Api.Models.Entities;

namespace Catalogo.Api.Interface;

public interface ITiendaService
{
    Task SaveTiendaAsync(TiendaCreateDto tienda);
    Task DeleteTiendaAsync(Guid idTienda);
    Task UpdateTiendaAsync(Guid idTienda, TiendaUpdateDto tienda);
    Task UpdateTiendaAsync(TiendaUpdateDto tienda);
    Task<TiendaEntity?> GetByIdAsync(Guid idTienda);
    Task<TiendaEntity?> GetTiendaEntity(string nombre);
    Task<IEnumerable<TiendaEntity>> GetAllTiendasAsync();
}