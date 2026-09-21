using Identity.Api.Interfaces;
using Identity.Api.Models.Dtos;
using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;

namespace Identity.Api.Services;

public class DomiciliarioService : IDomiciliarioService
{
    private readonly IDomiciliarioRepository _repository;

    public DomiciliarioService(IDomiciliarioRepository repository)
    {
        _repository = repository;
    }

    public async Task SaveDomiciliarioAsync(
        DomiciliarioDto dto,
        UsuarioEntity usuario)
    {
        var domiciliario = new DomiciliarioEntity
        {
            IdUsuario = usuario.IdUsuario,
            PlacaVehiculo = dto.PlacaVehiculo,
            TipoVehiculo = dto.TipoVehiculo,
            CapacidadCarga = dto.CapacidadCarga,
            EstadoPanelTareas = "ocupado",
            NumeroEntregas = 0,
            NumeroEntregasExitosas = 0,
            EstadoActivo = false,
            Valoracion = 0
        };

        await _repository.SaveDomiciliarioAsync(domiciliario);
    }

    public async Task<DomiciliarioEntity?> GetByIdAsync(Guid idUsuario)
    {
        return await _repository.GetByIdAsync(idUsuario);
    }

    public async Task<IEnumerable<DomiciliarioEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<DomiciliarioEntity> UpdateDomiciliarioAsync(Guid idUsuario, DomiciliarioDto dto)
    {
        var domiciliario = await _repository.GetByIdAsync(idUsuario);
        if (domiciliario == null)
        {
            throw new KeyNotFoundException("Datos de domiciliario no encontrados.");
        }

        domiciliario.PlacaVehiculo = dto.PlacaVehiculo;
        domiciliario.TipoVehiculo = dto.TipoVehiculo;
        domiciliario.CapacidadCarga = dto.CapacidadCarga;

        await _repository.UpdateAsync(domiciliario);
        return domiciliario;
    }

    public async Task DeleteDomiciliarioAsync(Guid idUsuario)
    {
        var domiciliario = await _repository.GetByIdAsync(idUsuario);
        if (domiciliario == null)
        {
            throw new KeyNotFoundException("Datos de domiciliario no encontrados.");
        }

        await _repository.DeleteAsync(idUsuario);
    }
}