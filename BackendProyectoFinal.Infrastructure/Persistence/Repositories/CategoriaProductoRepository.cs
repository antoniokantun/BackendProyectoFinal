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
    public class CategoriaProductoRepository : GenericRepository<CategoriaProducto>, ICategoriaProductoRepository
    {
        public CategoriaProductoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CategoriaProducto>> GetByProductoIdAsync(int productoId)
        {
            return await _context.Set<CategoriaProducto>()
                .Include(cp => cp.Categoria)
                .Where(cp => cp.ProductoId == productoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<CategoriaProducto>> GetByCategoriaIdAsync(int categoriaId)
        {
            return await _context.Set<CategoriaProducto>()
                .Include(cp => cp.Producto)
                .Where(cp => cp.CategoriaId == categoriaId)
                .ToListAsync();
        }
    }
}
