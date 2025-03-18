using BackendProyectoFinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IEvaluacionRepository
    {
        Task<IEnumerable<Evaluacion>> GetAllAsync();
        Task<IEnumerable<Evaluacion>> GetAllWithDetailsAsync();
        Task<Evaluacion?> GetByIdAsync(int id);
        Task<Evaluacion?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Evaluacion>> GetByProductIdAsync(int productoId);
        Task<IEnumerable<Evaluacion>> GetByUsuarioIdAsync(int usuarioId);
        Task<double> GetPromedioByProductIdAsync(int productoId);
        Task<Evaluacion> CreateAsync(Evaluacion evaluacion);
        Task UpdateAsync(Evaluacion evaluacion);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExisteEvaluacionUsuarioProductoAsync(int usuarioId, int productoId);
    }
}
