using BackendProyectoFinal.Domain.Entities;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IEvaluacionRepository : IGenericRepository<Evaluacion>
    {
        // Métodos específicos para Evaluacion
        Task<IEnumerable<Evaluacion>> GetByUsuarioIdAsync(int usuarioId);
        Task<IEnumerable<Evaluacion>> GetByProductoIdAsync(int productoId);
    }
}
