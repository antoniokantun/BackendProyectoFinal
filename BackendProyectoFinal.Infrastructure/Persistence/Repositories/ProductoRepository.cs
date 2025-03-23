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
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Producto>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _dbSet.Where(p => p.UsuarioId == usuarioId).ToListAsync();
        }

        public async Task<Producto> GetProductoCompletoByIdAsync(int id)
        {
            return await _context.Set<Producto>()
                .Include(p => p.Usuario)
                .Include(p => p.CategoriaProductos)
                    .ThenInclude(cp => cp.Categoria)
                .Include(p => p.ImagenProductos)
                    .ThenInclude(ip => ip.Imagen)
                .FirstOrDefaultAsync(p => p.IdProducto == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Productos.AnyAsync(p => p.IdProducto == id);
        }
    }
}
