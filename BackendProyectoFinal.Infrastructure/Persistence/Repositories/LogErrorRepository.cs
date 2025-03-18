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
    public class LogErrorRepository : ILogErrorRepository
    {
        private readonly ApplicationDbContext _context;

        public LogErrorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LogError> CreateAsync(LogError logError)
        {
            _context.LogErrores.Add(logError);
            await _context.SaveChangesAsync();
            return logError;
        }

        public async Task<IEnumerable<LogError>> GetAllAsync()
        {
            return await _context.LogErrores
                .Include(l => l.Usuario)
                .OrderByDescending(l => l.FechaOcurrencia)
                .ToListAsync();
        }

        public async Task<LogError> GetByIdAsync(int id)
        {
            return await _context.LogErrores
                .Include(l => l.Usuario)
                .FirstOrDefaultAsync(l => l.IdLogError == id);
        }

        public async Task<IEnumerable<LogError>> GetByUserIdAsync(int userId)
        {
            return await _context.LogErrores
                .Where(l => l.UsuarioId == userId)
                .OrderByDescending(l => l.FechaOcurrencia)
                .ToListAsync();
        }
    }
}
