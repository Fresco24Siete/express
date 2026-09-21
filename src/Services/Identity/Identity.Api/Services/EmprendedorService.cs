using Identity.Api.Interfaces;
using Identity.Api.Models.DTOs;
using Identity.Api.Models.Entities;

namespace Identity.Api.Services;

public class EmprendedorService : IEmprendedorService
{
    private readonly IEmprendedorRepository _repository;

    public EmprendedorService(IEmprendedorRepository repository)
    {
        _repository = repository;
    }

    public async Task SaveEmprendedorAsync(EmprendedorDto dto, UsuarioEntity usuario)
    {
        var emprendedor = new EmprendedorEntity
        {
            IdUsuario = usuario.IdUsuario,
            IdCategoriaServicio = dto.IdCategoriaServicio,
            NumeroServicios = dto.NumeroServicios,
            EstadoCertidicado = dto.EstadoCertidicado,
            DisponibilidadActiva = dto.DisponibilidadActiva,
            PrecioBaseHora = dto.PrecioBaseHora,
            DescripcionServicio = dto.DescripcionServicio ?? string.Empty
        };

        await _repository.SaveEmprendedorAsync(emprendedor);
    }

    public async Task<EmprendedorEntity?> GetByIdAsync(Guid idUsuario)
    {
        return await _repository.GetByIdAsync(idUsuario);
    }

    public async Task<IEnumerable<EmprendedorEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<EmprendedorEntity> UpdateEmprendedorAsync(Guid idUsuario, EmprendedorDto dto)
    {
        var emprendedor = await _repository.GetByIdAsync(idUsuario);
        if (emprendedor == null)
        {
            throw new KeyNotFoundException("Datos de emprendedor no encontrados.");
        }

        emprendedor.IdCategoriaServicio = dto.IdCategoriaServicio;
        emprendedor.NumeroServicios = dto.NumeroServicios;
        emprendedor.EstadoCertidicado = dto.EstadoCertidicado;
        emprendedor.DisponibilidadActiva = dto.DisponibilidadActiva;
        emprendedor.PrecioBaseHora = dto.PrecioBaseHora;
        if (!string.IsNullOrEmpty(dto.DescripcionServicio)) emprendedor.DescripcionServicio = dto.DescripcionServicio;

        await _repository.UpdateAsync(emprendedor);
        return emprendedor;
    }

    public async Task DeleteEmprendedorAsync(Guid idUsuario)
    {
        var emprendedor = await _repository.GetByIdAsync(idUsuario);
        if (emprendedor == null)
        {
            throw new KeyNotFoundException("Datos de emprendedor no encontrados.");
        }

        await _repository.DeleteAsync(idUsuario);
    }
}
