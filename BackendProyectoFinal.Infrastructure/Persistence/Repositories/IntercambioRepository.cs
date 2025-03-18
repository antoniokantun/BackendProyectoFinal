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
    public class IntercambioRepository : IIntercambioRepository
    {
        private readonly ApplicationDbContext _context;

        public IntercambioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Intercambio>> GetAllAsync()
        {
            return await _context.Intercambios
                .Include(i => i.UsuarioSolicitante)
                .Include(i => i.UsuarioOfertante)
                .Include(i => i.Producto)
                .ToListAsync();
        }

        public async Task<Intercambio> GetByIdAsync(int id)
        {
            return await _context.Intercambios
                .Include(i => i.UsuarioSolicitante)
                .Include(i => i.UsuarioOfertante)
                .Include(i => i.Producto)
                .FirstOrDefaultAsync(i => i.IdIntercambio == id);
        }

        public async Task<Intercambio> CreateAsync(Intercambio intercambio)
        {
            _context.Intercambios.Add(intercambio);
            await _context.SaveChangesAsync();
            return intercambio;
        }

        public async Task UpdateAsync(Intercambio intercambio)
        {
            _context.Entry(intercambio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var intercambio = await _context.Intercambios.FindAsync(id);
            if (intercambio != null)
            {
                _context.Intercambios.Remove(intercambio);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Intercambios.AnyAsync(i => i.IdIntercambio == id);
        }
    }
}
