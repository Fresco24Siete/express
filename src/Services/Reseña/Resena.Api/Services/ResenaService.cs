

using System.Reflection.Metadata;
using Resena.Api.Interfaces;
using Resena.Api.Models.Dtos;
using Resena.Api.Models.Entities;
using Resena.Api.Repositories;

namespace Resena.Api.Services;


public class ResenaService : IResenaService
{
    public readonly ResenaRepositories _repository;

    public ResenaService(ResenaRepositories repository)
    {
        _repository = repository;
    }

    public async Task SaveResenaAsync(CreateResenaDto resenaDto)
    {
        var resena = new ResenaEntity
        {
            IdResena = Guid.NewGuid(),
            IdCalificador = resenaDto.IdCalificador,
            IdCalificado = resenaDto.IdCalificado,
            tipoActividadEnum = resenaDto.tipoActividadEnum,
            IdReferencia = resenaDto.IdReferencia,
            puntuacion = resenaDto.puntuacion,
            comentario = resenaDto.comentario,
            estadoEnum = Models.Enums.EstadoEnum.Visible,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow

        };

        await _repository.SaveResenaAsync(resena);
    }
    public async Task DeleteResenaAsync (Guid idResena)
    {
        var existing = await _repository.GetResenaAsync(idResena);

        if (existing == null)
        {
            throw new KeyNotFoundException("Resena no encontrada.");
        }

        await _repository.DeleteResenaAsync(idResena);
    }
    public async Task <ResenaEntity> GetResenaAsync(Guid idResena)
    {
        var existing = await _repository.GetResenaAsync(idResena);
        
        if (existing == null)
        {
            throw new KeyNotFoundException("Resena no encontrada.");
        }
        return existing;
    }
    public async Task <IEnumerable<ResenaEntity> > GetAllResenaAsync (){
        return await _repository.GetAllResenaAsync();
    }

}
