using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Infrastructure.Persistence.Repositories
{
    public class EvaluacionRepository : GenericRepository<Evaluacion>, IEvaluacionRepository
    {
        public EvaluacionRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Implementación de métodos específicos
        public async Task<IEnumerable<Evaluacion>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _dbSet
                .Where(e => e.UsuarioId == usuarioId)
                .Include(e => e.Usuario)
                .Include(e => e.UsuarioEvaluador)
                .Include(e => e.Producto)
                .ToListAsync();
        }

        public async Task<IEnumerable<Evaluacion>> GetByProductoIdAsync(int productoId)
        {
            return await _dbSet
                .Where(e => e.ProductoId == productoId)
                .Include(e => e.Usuario)
                .Include(e => e.UsuarioEvaluador)
                .Include(e => e.Producto)
                .ToListAsync();
        }

        // Sobrescribir GetAllAsync para incluir relaciones
        public new async Task<IEnumerable<Evaluacion>> GetAllAsync()
        {
            return await _dbSet
                .Include(e => e.Usuario)
                .Include(e => e.UsuarioEvaluador)
                .Include(e => e.Producto)
                .ToListAsync();
        }

        // Sobrescribir GetByIdAsync para incluir relaciones
        public new async Task<Evaluacion?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(e => e.Usuario)
                .Include(e => e.UsuarioEvaluador)
                .Include(e => e.Producto)
                .FirstOrDefaultAsync(e => e.IdEvaluacion == id);
        }
    }
}