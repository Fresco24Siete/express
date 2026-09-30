

using Resena.Api.Models.Dtos;
using Resena.Api.Models.Entities;

namespace Resena.Api.Interfaces;

public interface IResenaService
{
    Task SaveResenaAsync(CreateResenaDto resena);
    Task DeleteResenaAsync (Guid idResena);
    Task <ResenaEntity> GetResenaAsync(Guid idResena);
    Task <IEnumerable<ResenaEntity> > GetAllResenaAsync ();

}