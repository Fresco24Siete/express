using Catalogo.Api.Interface;
using Catalogo.Api.Models.Dtos;
using Catalogo.Api.Models.Entities;
using Catalogo.Api.Models.Enums;

namespace Catalogo.Api.Services;

public class TiendaService : ITiendaService
{
    private readonly ITiendaRepository _tiendaRepository;
    private readonly IDireccionTiendaRepository _direccionRepository;

    public TiendaService(ITiendaRepository tiendaRepository, IDireccionTiendaRepository direccionRepository)
    {
        _tiendaRepository = tiendaRepository;
        _direccionRepository = direccionRepository;
    }

    public async Task SaveTiendaAsync(TiendaCreateDto tienda)
    {
        long? direccionId = tienda.IdDireccionTienda;

        if (tienda.Direccion != null && (!direccionId.HasValue || direccionId.Value == 0))
        {
            var direccionEntity = new DireccionTiendaEntity
            {
                NumeroCalle = tienda.Direccion.NumeroCalle,
                Ciudad = tienda.Direccion.Ciudad,
                Departamento = tienda.Direccion.Departamento,
                CodigoPostal = tienda.Direccion.CodigoPostal,
                Latitud = tienda.Direccion.Latitud,
                Longitud = tienda.Direccion.Longitud,
                NumeroContacto = tienda.Direccion.NumeroContacto
            };

            await _direccionRepository.SaveDireccionAsync(direccionEntity);
            direccionId = direccionEntity.IdDireccionTienda;
        }

        var entity = new TiendaEntity
        {
            IdTienda = Guid.NewGuid(),
            IdAdministrador = tienda.IdAdministrador,
            IdDireccionTienda = direccionId,
            Nombre = tienda.Nombre,
            Nit = tienda.Nit,
            Seguidores = 0,
            CategoriaPrincipal = tienda.CategoriaPrincipal,
            EstadoCertificacion = EstadoCertificacionEnum.Pendiente,
            CantidadProductos = 0,
            TerminosAceptados = tienda.TerminosAceptados,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        await _tiendaRepository.SaveTiendaAsync(entity);
    }

    public async Task DeleteTiendaAsync(Guid idTienda)
    {
        var existing = await _tiendaRepository.GetByIdAsync(idTienda);
        if (existing == null)
        {
            throw new KeyNotFoundException("Tienda no encontrada.");
        }

        await _tiendaRepository.DeleteTiendaAsync(idTienda);
    }

    public async Task UpdateTiendaAsync(Guid idTienda, TiendaUpdateDto tienda)
    {
        var existing = await _tiendaRepository.GetByIdAsync(idTienda);
        if (existing == null)
        {
            throw new KeyNotFoundException("Tienda no encontrada.");
        }

        if (!string.IsNullOrWhiteSpace(tienda.Nombre))
            existing.Nombre = tienda.Nombre;

        if (!string.IsNullOrWhiteSpace(tienda.CategoriaPrincipal))
            existing.CategoriaPrincipal = tienda.CategoriaPrincipal;

        if (tienda.EstadoCertificacion.HasValue)
            existing.EstadoCertificacion = tienda.EstadoCertificacion.Value;

        if (tienda.TerminosAceptados.HasValue)
            existing.TerminosAceptados = tienda.TerminosAceptados.Value;

        existing.UpdatedAt = DateTimeOffset.UtcNow;

        await _tiendaRepository.UpdateTiendaAsync(existing);
    }

    public async Task UpdateTiendaAsync(TiendaUpdateDto tienda)
    {
        if (!tienda.IdTienda.HasValue)
        {
            throw new ArgumentException("El IdTienda es requerido para actualizar.");
        }

        await UpdateTiendaAsync(tienda.IdTienda.Value, tienda);
    }

    public async Task<TiendaEntity?> GetByIdAsync(Guid idTienda)
    {
        return await _tiendaRepository.GetByIdAsync(idTienda);
    }

    public async Task<TiendaEntity?> GetTiendaEntity(string nombre)
    {
        return await _tiendaRepository.GetTiendaEntity(nombre);
    }

    public async Task<IEnumerable<TiendaEntity>> GetAllTiendasAsync()
    {
        return await _tiendaRepository.GetAllTiendasAsync();
    }
}
