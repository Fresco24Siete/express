using Identity.Api.Interfaces;
using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;

namespace Identity.Api.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task SaveClienteAsync(ClienteDto dto, UsuarioEntity usuario)
    {
        var cliente = new ClienteEntity
        {
            IdUsuario = usuario.IdUsuario,
            IdCarrito = dto.IdCarrito,
            CantidadResenas = dto.CantidadResenas,
            NivelConfianza = dto.NivelConfianza ?? "Nuevo",
            MetodoPago = dto.MetodoPago,
            NumeroPedidos = dto.NumeroPedidos,
            EstadoActivo = true
        };

        await _repository.SaveClienteAsync(cliente);
    }

    public async Task<ClienteEntity?> GetByIdAsync(Guid idUsuario)
    {
        return await _repository.GetByIdAsync(idUsuario);
    }

    public async Task<IEnumerable<ClienteEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ClienteEntity> UpdateClienteAsync(Guid idUsuario, ClienteDto dto)
    {
        var cliente = await _repository.GetByIdAsync(idUsuario);
        if (cliente == null)
        {
            throw new KeyNotFoundException("Datos de cliente no encontrados.");
        }

        if (dto.IdCarrito.HasValue) cliente.IdCarrito = dto.IdCarrito;
        cliente.CantidadResenas = dto.CantidadResenas;
        if (!string.IsNullOrEmpty(dto.NivelConfianza)) cliente.NivelConfianza = dto.NivelConfianza;
        cliente.MetodoPago = dto.MetodoPago;
        cliente.NumeroPedidos = dto.NumeroPedidos;
        cliente.EstadoActivo = dto.EstadoActivo;

        await _repository.UpdateAsync(cliente);
        return cliente;
    }

    public async Task DeleteClienteAsync(Guid idUsuario)
    {
        var cliente = await _repository.GetByIdAsync(idUsuario);
        if (cliente == null)
        {
            throw new KeyNotFoundException("Datos de cliente no encontrados.");
        }

        await _repository.DeleteAsync(idUsuario);
    }
}
