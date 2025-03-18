using BackendProyectoFinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto> GetByIdAsync(int id);
        Task<IEnumerable<Producto>> GetByUsuarioIdAsync(int usuarioId);
        Task<Producto> CreateAsync(Producto producto);
        Task UpdateAsync(Producto producto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
