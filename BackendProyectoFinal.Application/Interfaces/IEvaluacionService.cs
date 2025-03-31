using BackendProyectoFinal.Application.DTOs;

namespace BackendProyectoFinal.Application.Interfaces
{
    public interface IEvaluacionService
    {
        Task<IEnumerable<EvaluacionDTO>> GetAllAsync();
        Task<EvaluacionDTO> GetByIdAsync(int id);
        Task<IEnumerable<EvaluacionDTO>> GetByUsuarioIdAsync(int usuarioId);
        Task<IEnumerable<EvaluacionDTO>> GetByProductoIdAsync(int productoId);
        Task<EvaluacionDTO> CreateAsync(CreateEvaluacionDTO createEvaluacionDto);
        Task UpdateAsync(int id, UpdateEvaluacionDTO updateEvaluacionDto);
        Task DeleteAsync(int id);

        Task<EvaluacionDTO> PatchAsync(int id, PatchEvaluacionDTO patchEvaluacionDto);
    }
}
