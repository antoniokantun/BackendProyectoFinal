using BackendProyectoFinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IIntercambioRepository : IGenericRepository<Intercambio>
    {
        // Métodos específicos para Intercambio si se necesitan
        Task<IEnumerable<Intercambio>> GetByUsuarioSolicitanteIdAsync(int usuarioSolicitanteId);
        Task<IEnumerable<Intercambio>> GetByUsuarioOfertanteIdAsync(int usuarioOfertanteId);
        Task<IEnumerable<Intercambio>> GetByProductoIdAsync(int productoId);
    }
}
