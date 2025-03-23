using BackendProyectoFinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IProductoRepository : IGenericRepository<Producto>
    {
        Task<IEnumerable<Producto>> GetByUsuarioIdAsync(int usuarioId);
        Task<Producto> GetProductoCompletoByIdAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}
