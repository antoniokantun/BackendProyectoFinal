using BackendProyectoFinal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.Interfaces
{
    public interface IEvaluacionService
    {
        Task<IEnumerable<EvaluacionDTO>> GetAllAsync();
        Task<EvaluacionDTO> GetByIdAsync(int id);
        Task<IEnumerable<EvaluacionDTO>> GetByProductIdAsync(int productoId);
        Task<IEnumerable<EvaluacionDTO>> GetByUsuarioIdAsync(int usuarioId);
        Task<double> GetPromedioByProductIdAsync(int productoId);
        Task<EvaluacionDTO> CreateAsync(CreateEvaluacionDTO evaluacionDto);
        Task UpdateAsync(int id, UpdateEvaluacionDTO evaluacionDto);
        Task DeleteAsync(int id);
        Task<bool> ExisteEvaluacionUsuarioProductoAsync(int usuarioId, int productoId);
    }
}
