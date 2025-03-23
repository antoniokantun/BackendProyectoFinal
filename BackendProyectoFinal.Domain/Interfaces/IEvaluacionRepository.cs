using BackendProyectoFinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IEvaluacionRepository : IGenericRepository<Evaluacion>
    {
        // Métodos específicos para Evaluacion
        Task<IEnumerable<Evaluacion>> GetByUsuarioIdAsync(int usuarioId);
        Task<IEnumerable<Evaluacion>> GetByProductoIdAsync(int productoId);
    }
}
