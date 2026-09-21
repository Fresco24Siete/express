using Identity.Api.Interfaces;
using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;


namespace Identity.Api.Services;

public class DireccionService : IDireccionService
{
    private readonly IDireccionRepository _repository;

    public DireccionService(IDireccionRepository repository)
    {
        _repository = repository;
    }

    public async Task GuardarDireccion(DireccionRequestDto dto, UsuarioEntity usuario)
    {   

        var Direccion = new DireccionEntity
        {
            IdDireccion = Guid.NewGuid(),
            IdUsuario = usuario.IdUsuario,
            NumeroCalle = dto.NumeroCalle,
            AptoSuiteNumero = dto.AptoSuiteNumero,
            TipoDomicilio =  dto.TipoDomicilio,
            Ciudad = dto.Ciudad, 
            Departamento = dto.Departamento,
            Pais = dto.Pais,
            CodigoPostal = dto.CodigoPostal, 
            Latitud = dto.Latitud, 
            Longitud = dto.Longitud,
            ReferenciaAdicional = dto.ReferenciaAdicional,
            ValidadoGeolocalizacion = false,
            IsPredeterminada = dto.IsPredeterminada,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

         await _repository.AddAsync(Direccion);
    }

    public async Task<DireccionEntity?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<DireccionEntity>> GetByUsuarioIdAsync(Guid usuarioId)
    {
        return await _repository.GetByUsuarioIdAsync(usuarioId);
    }

    public async Task<DireccionEntity> UpdateDireccionAsync(Guid id, DireccionRequestDto dto, Guid usuarioId)
    {
        var direccion = await _repository.GetByIdAsync(id);
        if (direccion == null)
        {
            throw new KeyNotFoundException("Dirección no encontrada.");
        }

        if (direccion.IdUsuario != usuarioId)
        {
            throw new UnauthorizedAccessException("No tiene permiso para modificar esta dirección.");
        }

        direccion.NumeroCalle = dto.NumeroCalle;
        direccion.AptoSuiteNumero = dto.AptoSuiteNumero;
        direccion.TipoDomicilio = dto.TipoDomicilio;
        direccion.Ciudad = dto.Ciudad;
        direccion.Departamento = dto.Departamento;
        direccion.Pais = dto.Pais;
        direccion.CodigoPostal = dto.CodigoPostal;
        direccion.Latitud = dto.Latitud;
        direccion.Longitud = dto.Longitud;
        direccion.ReferenciaAdicional = dto.ReferenciaAdicional;
        direccion.IsPredeterminada = dto.IsPredeterminada;
        direccion.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(direccion);
        return direccion;
    }

    public async Task DeleteDireccionAsync(Guid id, Guid usuarioId)
    {
        var direccion = await _repository.GetByIdAsync(id);
        if (direccion == null)
        {
            throw new KeyNotFoundException("Dirección no encontrada.");
        }

        if (direccion.IdUsuario != usuarioId)
        {
            throw new UnauthorizedAccessException("No tiene permiso para eliminar esta dirección.");
        }

        await _repository.DeleteAsync(id);
    }
}