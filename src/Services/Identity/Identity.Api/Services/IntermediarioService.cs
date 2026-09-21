using Identity.Api.Interfaces;
using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;

namespace Identity.Api.Services;

public class IntermediarioService : IIntermediarioService
{
    private readonly IIntermediarioRepository _repository;
    public IntermediarioService(IIntermediarioRepository repository)
    {
        _repository = repository;
    }
    public async Task SaveIntermediarioAsync(IntermediarioDto dto, UsuarioEntity usuario)
    {
        var intermediario = new IntermediarioEntity
        {
          IdUsuario = usuario.IdUsuario,
          CasosResueltos = dto.CasosResueltos,
          NivelAutorizacion = "ninguno",
          EstadoActivo = false
        };

        await _repository.SaveIntermediarioAsync(intermediario);
    }

    public async Task<IntermediarioEntity?> GetByIdAsync(Guid idUsuario)
    {
        return await _repository.GetByIdAsync(idUsuario);
    }

    public async Task<IEnumerable<IntermediarioEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IntermediarioEntity> UpdateIntermediarioAsync(Guid idUsuario, IntermediarioDto dto)
    {
        var intermediario = await _repository.GetByIdAsync(idUsuario);
        if (intermediario == null)
        {
            throw new KeyNotFoundException("Datos de intermediario no encontrados.");
        }

        intermediario.CasosResueltos = dto.CasosResueltos;

        await _repository.UpdateAsync(intermediario);
        return intermediario;
    }

    public async Task DeleteIntermediarioAsync(Guid idUsuario)
    {
        var intermediario = await _repository.GetByIdAsync(idUsuario);
        if (intermediario == null)
        {
            throw new KeyNotFoundException("Datos de intermediario no encontrados.");
        }

        await _repository.DeleteAsync(idUsuario);
    }
}