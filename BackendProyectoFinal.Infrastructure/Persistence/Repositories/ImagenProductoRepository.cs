using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Infrastructure.Persistence.Repositories
{
    public class ImagenProductoRepository : GenericRepository<ImagenProducto>, IImagenProductoRepository
    {
        public ImagenProductoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ImagenProducto>> GetByProductoIdAsync(int productoId)
        {
            return await _context.Set<ImagenProducto>()
                .Include(ip => ip.Imagen)
                .Where(ip => ip.ProductoId == productoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ImagenProducto>> GetByImagenIdAsync(int imagenId)
        {
            return await _context.Set<ImagenProducto>()
                .Include(ip => ip.Producto)
                .Where(ip => ip.ImagenId == imagenId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ImagenProducto>> GetByProductoIdWithImagenesAsync(int productoId)
        {
            return await _dbSet
                .Where(ip => ip.ProductoId == productoId)
                .Include(ip => ip.Imagen)
                .ToListAsync();
        }
    }
}
