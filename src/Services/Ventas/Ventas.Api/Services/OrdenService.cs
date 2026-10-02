using Ventas.Api.Interfaces;
using Ventas.Api.Models.Dtos;
using Ventas.Api.Models.Entities;
using Ventas.Api.Models.Enums;

namespace Ventas.Api.Services;

public class OrdenService : IOrdenService
{
    private readonly IOrdenRepository _repository;

    public OrdenService(IOrdenRepository repository)
    {
        _repository = repository;
    }

    public async Task<OrdenResponseDto> CreateAsync(OrdenCreateDto dto)
    {
        var numeroOrden = !string.IsNullOrWhiteSpace(dto.NumeroOrden)
            ? dto.NumeroOrden
            : await GenerateNumeroOrdenAsync();

        if (await _repository.ExistsNumeroOrdenAsync(numeroOrden))
        {
            throw new InvalidOperationException($"El número de orden '{numeroOrden}' ya existe.");
        }

        var ordenId = Guid.NewGuid();

        var entity = new OrdenEntity
        {
            IdOrden = ordenId,
            NumeroOrden = numeroOrden,
            IdUsuario = dto.IdUsuario,
            IdDireccionEnvio = dto.IdDireccionEnvio,
            IdCarrito = dto.IdCarrito,
            Estado = EstadoOrdenEnum.pendiente_pago,
            PrecioSubtotal = dto.PrecioSubtotal,
            PrecioDescuento = dto.PrecioDescuento,
            PrecioEnvio = dto.PrecioEnvio,
            PrecioTotal = dto.PrecioTotal,
            CodigoDescuento = dto.CodigoDescuento,
            NotasCliente = dto.NotasCliente,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        if (dto.Detalles != null && dto.Detalles.Any())
        {
            foreach (var item in dto.Detalles)
            {
                var subtotal = item.Subtotal ?? (item.Cantidad * item.PrecioUnitario);
                entity.Detalles.Add(new DetalleOrdenEntity
                {
                    IdDetalleOrden = Guid.NewGuid(),
                    IdOrden = ordenId,
                    IdProducto = item.IdProducto,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.PrecioUnitario,
                    Subtotal = subtotal
                });
            }
        }

        var created = await _repository.CreateAsync(entity);
        return MapToResponse(created);
    }

    public async Task<OrdenResponseDto?> GetByIdAsync(Guid idOrden)
    {
        var entity = await _repository.GetByIdAsync(idOrden, includeDetails: true);
        return entity == null ? null : MapToResponse(entity);
    }

    public async Task<OrdenResponseDto?> GetByNumeroOrdenAsync(string numeroOrden)
    {
        var entity = await _repository.GetByNumeroOrdenAsync(numeroOrden);
        return entity == null ? null : MapToResponse(entity);
    }

    public async Task<IEnumerable<OrdenResponseDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(MapToResponse);
    }

    public async Task<IEnumerable<OrdenResponseDto>> GetByUsuarioIdAsync(Guid idUsuario)
    {
        var list = await _repository.GetByUsuarioIdAsync(idUsuario);
        return list.Select(MapToResponse);
    }

    public async Task<IEnumerable<OrdenResponseDto>> GetByEstadoAsync(EstadoOrdenEnum estado)
    {
        var list = await _repository.GetByEstadoAsync(estado);
        return list.Select(MapToResponse);
    }

    public async Task<OrdenResponseDto> UpdateAsync(Guid idOrden, OrdenUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(idOrden, includeDetails: true);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Orden con ID {idOrden} no encontrada.");
        }

        if (dto.IdDireccionEnvio.HasValue)
            existing.IdDireccionEnvio = dto.IdDireccionEnvio.Value;

        if (dto.Estado.HasValue)
        {
            existing.Estado = dto.Estado.Value;
            if (dto.Estado.Value == EstadoOrdenEnum.pagada && existing.FechaPagada == null)
            {
                existing.FechaPagada = DateTimeOffset.UtcNow;
            }
        }

        if (dto.PrecioSubtotal.HasValue)
            existing.PrecioSubtotal = dto.PrecioSubtotal.Value;

        if (dto.PrecioDescuento.HasValue)
            existing.PrecioDescuento = dto.PrecioDescuento.Value;

        if (dto.PrecioEnvio.HasValue)
            existing.PrecioEnvio = dto.PrecioEnvio.Value;

        if (dto.PrecioTotal.HasValue)
            existing.PrecioTotal = dto.PrecioTotal.Value;

        if (dto.CodigoDescuento != null)
            existing.CodigoDescuento = dto.CodigoDescuento;

        if (dto.NotasCliente != null)
            existing.NotasCliente = dto.NotasCliente;

        if (dto.FechaPagada.HasValue)
            existing.FechaPagada = dto.FechaPagada.Value;

        if (dto.FechaCancelacion.HasValue)
            existing.FechaCancelacion = dto.FechaCancelacion.Value;

        if (dto.RazonCancelacion != null)
            existing.RazonCancelacion = dto.RazonCancelacion;

        existing.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(existing);
        return MapToResponse(existing);
    }

    public async Task<OrdenResponseDto> CancelarOrdenAsync(Guid idOrden, OrdenCancelarDto dto)
    {
        var existing = await _repository.GetByIdAsync(idOrden, includeDetails: true);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Orden con ID {idOrden} no encontrada.");
        }

        if (existing.Estado == EstadoOrdenEnum.cancelada)
        {
            throw new InvalidOperationException("La orden ya se encuentra cancelada.");
        }

        existing.Estado = EstadoOrdenEnum.cancelada;
        existing.FechaCancelacion = DateTimeOffset.UtcNow;
        existing.RazonCancelacion = dto.RazonCancelacion;
        existing.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(existing);
        return MapToResponse(existing);
    }

    public async Task DeleteAsync(Guid idOrden)
    {
        var existing = await _repository.GetByIdAsync(idOrden, includeDetails: false);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Orden con ID {idOrden} no encontrada.");
        }

        await _repository.DeleteAsync(idOrden);
    }

    private async Task<string> GenerateNumeroOrdenAsync()
    {
        var fecha = DateTime.UtcNow.ToString("yyyyMMdd");
        string numero;
        do
        {
            var randomCode = Guid.NewGuid().ToString("N")[..6].ToUpper();
            numero = $"ORD-{fecha}-{randomCode}";
        } while (await _repository.ExistsNumeroOrdenAsync(numero));

        return numero;
    }

    private static OrdenResponseDto MapToResponse(OrdenEntity entity)
    {
        return new OrdenResponseDto
        {
            IdOrden = entity.IdOrden,
            NumeroOrden = entity.NumeroOrden,
            IdUsuario = entity.IdUsuario,
            IdDireccionEnvio = entity.IdDireccionEnvio,
            IdCarrito = entity.IdCarrito,
            Estado = entity.Estado,
            PrecioSubtotal = entity.PrecioSubtotal,
            PrecioDescuento = entity.PrecioDescuento,
            PrecioEnvio = entity.PrecioEnvio,
            PrecioTotal = entity.PrecioTotal,
            CodigoDescuento = entity.CodigoDescuento,
            NotasCliente = entity.NotasCliente,
            FechaPagada = entity.FechaPagada,
            FechaCancelacion = entity.FechaCancelacion,
            RazonCancelacion = entity.RazonCancelacion,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Detalles = entity.Detalles?.Select(d => new DetalleOrdenResponseDto
            {
                IdDetalleOrden = d.IdDetalleOrden,
                IdOrden = d.IdOrden,
                IdProducto = d.IdProducto,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Subtotal = d.Subtotal
            }).ToList(),
            Pago = entity.Pago == null ? null : new PagoResponseDto
            {
                IdPago = entity.Pago.IdPago,
                IdOrden = entity.Pago.IdOrden,
                Monto = entity.Pago.Monto,
                Moneda = entity.Pago.Moneda,
                Metodo = entity.Pago.Metodo,
                Estado = entity.Pago.Estado,
                ReferenciaExterno = entity.Pago.ReferenciaExterno,
                TokenEncriptado = entity.Pago.TokenEncriptado,
                Ultimos4Digitos = entity.Pago.Ultimos4Digitos,
                FechaIntento = entity.Pago.FechaIntento,
                FechaCompletado = entity.Pago.FechaCompletado,
                RazonFallo = entity.Pago.RazonFallo,
                CreatedBy = entity.Pago.CreatedBy,
                CreatedAt = entity.Pago.CreatedAt,
                UpdatedAt = entity.Pago.UpdatedAt
            }
        };
    }
}
