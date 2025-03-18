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
    public class EvaluacionRepository : IEvaluacionRepository
    {
        private readonly ApplicationDbContext _context;

        public EvaluacionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Evaluacion>> GetAllAsync()
        {
            return await _context.Evaluaciones.ToListAsync();
        }

        public async Task<IEnumerable<Evaluacion>> GetAllWithDetailsAsync()
        {
            return await _context.Evaluaciones
                .Include(e => e.Usuario)
                .Include(e => e.Producto)
                .ToListAsync();
        }

        public async Task<Evaluacion?> GetByIdAsync(int id)
        {
            return await _context.Evaluaciones.FindAsync(id);
        }

        public async Task<Evaluacion?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Evaluaciones
                .Include(e => e.Usuario)
                .Include(e => e.Producto)
                .FirstOrDefaultAsync(e => e.IdEvaluacion == id);
        }

        public async Task<IEnumerable<Evaluacion>> GetByProductIdAsync(int productoId)
        {
            return await _context.Evaluaciones
                .Include(e => e.Usuario)
                .Include(e => e.Producto)
                .Where(e => e.ProductoId == productoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Evaluacion>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Evaluaciones
                .Include(e => e.Usuario)
                .Include(e => e.Producto)
                .Where(e => e.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<double> GetPromedioByProductIdAsync(int productoId)
        {
            var evaluaciones = await _context.Evaluaciones
                .Where(e => e.ProductoId == productoId)
                .ToListAsync();

            if (!evaluaciones.Any())
                return 0;

            return evaluaciones.Average(e => e.Puntuacion);
        }

        public async Task<Evaluacion> CreateAsync(Evaluacion evaluacion)
        {
            _context.Evaluaciones.Add(evaluacion);
            await _context.SaveChangesAsync();
            return evaluacion;
        }

        public async Task UpdateAsync(Evaluacion evaluacion)
        {
            _context.Entry(evaluacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var evaluacion = await _context.Evaluaciones.FindAsync(id);
            if (evaluacion != null)
            {
                _context.Evaluaciones.Remove(evaluacion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Evaluaciones.AnyAsync(e => e.IdEvaluacion == id);
        }

        public async Task<bool> ExisteEvaluacionUsuarioProductoAsync(int usuarioId, int productoId)
        {
            return await _context.Evaluaciones.AnyAsync(e =>
                e.UsuarioId == usuarioId && e.ProductoId == productoId);
        }
    }
}
