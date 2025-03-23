using BackendProyectoFinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IImagenProductoRepository : IGenericRepository<ImagenProducto>
    {
        Task<IEnumerable<ImagenProducto>> GetByProductoIdAsync(int productoId);
        Task<IEnumerable<ImagenProducto>> GetByImagenIdAsync(int imagenId);
    }
}
