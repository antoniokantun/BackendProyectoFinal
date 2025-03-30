
using BackendProyectoFinal.Application.DTOs;

namespace BackendProyectoFinal.Application.Interfaces
{
    public interface IEstadoService
    {
        Task<IEnumerable<EstadoDTO>> GetAllAsync();
        Task<EstadoDTO> GetByIdAsync(int id);
        Task<EstadoDTO> CreateAsync(CreateEstadoDTO estadoDto);
        Task UpdateAsync(int id, UpdateEstadoDTO estadoDto);
        Task DeleteAsync(int id);
        Task<EstadoDTO> GetByNombreAsync(string nombre);
    }
}
