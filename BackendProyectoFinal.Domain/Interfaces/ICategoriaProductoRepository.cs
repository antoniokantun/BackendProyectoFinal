using BackendProyectoFinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface ICategoriaProductoRepository : IGenericRepository<CategoriaProducto>
    {
        Task<IEnumerable<CategoriaProducto>> GetByProductoIdAsync(int productoId);
        Task<IEnumerable<CategoriaProducto>> GetByCategoriaIdAsync(int categoriaId);
    }
}
