using Ventas.Api.Interfaces;
using Ventas.Api.Models.Dtos;
using Ventas.Api.Models.Entities;
using Ventas.Api.Models.Enums;

namespace Ventas.Api.Services;

public class PagoService : IPagoService
{
    private readonly IPagoRepository _repository;
    private readonly IOrdenRepository _ordenRepository;

    public PagoService(IPagoRepository repository, IOrdenRepository ordenRepository)
    {
        _repository = repository;
        _ordenRepository = ordenRepository;
    }

    public async Task<PagoResponseDto> CreateAsync(PagoCreateDto dto)
    {
        var orden = await _ordenRepository.GetByIdAsync(dto.IdOrden, includeDetails: false);
        if (orden == null)
        {
            throw new KeyNotFoundException($"Orden con ID {dto.IdOrden} no encontrada.");
        }

        var existingPago = await _repository.GetByOrdenIdAsync(dto.IdOrden);
        if (existingPago != null)
        {
            throw new InvalidOperationException($"Ya existe un registro de pago para la orden {dto.IdOrden}.");
        }

        var entity = new PagoEntity
        {
            IdPago = Guid.NewGuid(),
            IdOrden = dto.IdOrden,
            Monto = dto.Monto,
            Moneda = string.IsNullOrWhiteSpace(dto.Moneda) ? "COP" : dto.Moneda.ToUpperInvariant(),
            Metodo = dto.Metodo,
            Estado = dto.Estado,
            ReferenciaExterno = dto.ReferenciaExterno,
            TokenEncriptado = dto.TokenEncriptado,
            Ultimos4Digitos = dto.Ultimos4Digitos,
            FechaIntento = DateTimeOffset.UtcNow,
            FechaCompletado = dto.Estado == EstadoPagoEnum.completado ? (dto.FechaCompletado ?? DateTimeOffset.UtcNow) : dto.FechaCompletado,
            RazonFallo = dto.RazonFallo,
            CreatedBy = dto.CreatedBy,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        var created = await _repository.CreateAsync(entity);

        // Si el pago es exitoso, actualizar estado de la orden
        if (entity.Estado == EstadoPagoEnum.completado)
        {
            orden.Estado = EstadoOrdenEnum.pagada;
            orden.FechaPagada = entity.FechaCompletado ?? DateTimeOffset.UtcNow;
            orden.UpdatedAt = DateTimeOffset.UtcNow;
            await _ordenRepository.UpdateAsync(orden);
        }

        return MapToResponse(created);
    }

    public async Task<PagoResponseDto?> GetByIdAsync(Guid idPago)
    {
        var pago = await _repository.GetByIdAsync(idPago);
        return pago == null ? null : MapToResponse(pago);
    }

    public async Task<PagoResponseDto?> GetByOrdenIdAsync(Guid idOrden)
    {
        var pago = await _repository.GetByOrdenIdAsync(idOrden);
        return pago == null ? null : MapToResponse(pago);
    }

    public async Task<IEnumerable<PagoResponseDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(MapToResponse);
    }

    public async Task<PagoResponseDto> UpdateAsync(Guid idPago, PagoUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(idPago);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Pago con ID {idPago} no encontrado.");
        }

        if (dto.Estado.HasValue)
        {
            existing.Estado = dto.Estado.Value;
            if (dto.Estado.Value == EstadoPagoEnum.completado && existing.FechaCompletado == null)
            {
                existing.FechaCompletado = DateTimeOffset.UtcNow;

                // Actualizar orden
                var orden = await _ordenRepository.GetByIdAsync(existing.IdOrden, includeDetails: false);
                if (orden != null)
                {
                    orden.Estado = EstadoOrdenEnum.pagada;
                    orden.FechaPagada = existing.FechaCompletado;
                    orden.UpdatedAt = DateTimeOffset.UtcNow;
                    await _ordenRepository.UpdateAsync(orden);
                }
            }
        }

        if (dto.ReferenciaExterno != null)
            existing.ReferenciaExterno = dto.ReferenciaExterno;

        if (dto.TokenEncriptado != null)
            existing.TokenEncriptado = dto.TokenEncriptado;

        if (dto.Ultimos4Digitos != null)
            existing.Ultimos4Digitos = dto.Ultimos4Digitos;

        if (dto.FechaCompletado.HasValue)
            existing.FechaCompletado = dto.FechaCompletado.Value;

        if (dto.RazonFallo != null)
            existing.RazonFallo = dto.RazonFallo;

        existing.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(existing);
        return MapToResponse(existing);
    }

    public async Task DeleteAsync(Guid idPago)
    {
        var existing = await _repository.GetByIdAsync(idPago);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Pago con ID {idPago} no encontrado.");
        }

        await _repository.DeleteAsync(idPago);
    }

    private static PagoResponseDto MapToResponse(PagoEntity entity)
    {
        return new PagoResponseDto
        {
            IdPago = entity.IdPago,
            IdOrden = entity.IdOrden,
            Monto = entity.Monto,
            Moneda = entity.Moneda,
            Metodo = entity.Metodo,
            Estado = entity.Estado,
            ReferenciaExterno = entity.ReferenciaExterno,
            TokenEncriptado = entity.TokenEncriptado,
            Ultimos4Digitos = entity.Ultimos4Digitos,
            FechaIntento = entity.FechaIntento,
            FechaCompletado = entity.FechaCompletado,
            RazonFallo = entity.RazonFallo,
            CreatedBy = entity.CreatedBy,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
