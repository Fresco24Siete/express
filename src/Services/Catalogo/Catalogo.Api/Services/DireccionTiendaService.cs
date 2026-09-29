using Catalogo.Api.Interface;
using Catalogo.Api.Models.Dtos;
using Catalogo.Api.Models.Entities;

namespace Catalogo.Api.Services;

public class DireccionTiendaService : IDireccionTiendaService
{
    private readonly IDireccionTiendaRepository _repository;

    public DireccionTiendaService(IDireccionTiendaRepository repository)
    {
        _repository = repository;
    }

    public async Task SaveDireccionAsync(DireccionTiendaCreateDto direccion)
    {
        var entity = new DireccionTiendaEntity
        {
            NumeroCalle = direccion.NumeroCalle,
            Ciudad = direccion.Ciudad,
            Departamento = direccion.Departamento,
            CodigoPostal = direccion.CodigoPostal,
            Latitud = direccion.Latitud,
            Longitud = direccion.Longitud,
            NumeroContacto = direccion.NumeroContacto
        };

        await _repository.SaveDireccionAsync(entity);
    }

    public async Task DeleteDireccionAsync(long idDireccion)
    {
        var existing = await _repository.GetByIdAsync(idDireccion);
        if (existing == null)
        {
            throw new KeyNotFoundException("Dirección de tienda no encontrada.");
        }

        await _repository.DeleteDireccionAsync(idDireccion);
    }

    public async Task UpdateDireccionAsync(long idDireccion, DireccionTiendaUpdateDto direccion)
    {
        var existing = await _repository.GetByIdAsync(idDireccion);
        if (existing == null)
        {
            throw new KeyNotFoundException("Dirección de tienda no encontrada.");
        }

        if (!string.IsNullOrWhiteSpace(direccion.NumeroCalle))
            existing.NumeroCalle = direccion.NumeroCalle;

        if (!string.IsNullOrWhiteSpace(direccion.Ciudad))
            existing.Ciudad = direccion.Ciudad;

        if (!string.IsNullOrWhiteSpace(direccion.Departamento))
            existing.Departamento = direccion.Departamento;

        if (direccion.CodigoPostal != null)
            existing.CodigoPostal = direccion.CodigoPostal;

        if (direccion.Latitud.HasValue)
            existing.Latitud = direccion.Latitud;

        if (direccion.Longitud.HasValue)
            existing.Longitud = direccion.Longitud;

        if (direccion.NumeroContacto != null)
            existing.NumeroContacto = direccion.NumeroContacto;

        await _repository.UpdateDireccionAsync(existing);
    }

    public async Task UpdateDireccionAsync(DireccionTiendaUpdateDto direccion)
    {
        if (!direccion.IdDireccionTienda.HasValue)
        {
            throw new ArgumentException("El IdDireccionTienda es requerido para actualizar.");
        }

        await UpdateDireccionAsync(direccion.IdDireccionTienda.Value, direccion);
    }

    public async Task<DireccionTiendaEntity?> GetByIdAsync(long idDireccion)
    {
        return await _repository.GetByIdAsync(idDireccion);
    }

    public async Task<DireccionTiendaEntity?> GetDireccionEntity(long idDireccion)
    {
        return await _repository.GetDireccionEntity(idDireccion);
    }

    public async Task<IEnumerable<DireccionTiendaEntity>> GetAllDireccionAsync()
    {
        return await _repository.GetAllDireccionAsync();
    }
}
