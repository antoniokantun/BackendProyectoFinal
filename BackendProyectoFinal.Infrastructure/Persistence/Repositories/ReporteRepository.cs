using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Infrastructure.Persistence.Repositories
{
    public class ReporteRepository : GenericRepository<Reporte>, IReporteRepository
    {
        public ReporteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Reporte>> GetAllReportesWithDetailsAsync()
        {
            return await _context.Reportes
                .Include(r => r.UsuarioReportes)
                    .ThenInclude(ur => ur.Usuario)
                .Include(r => r.ProductoReportes)
                    .ThenInclude(pr => pr.Producto)
                .ToListAsync();
        }

        public async Task<Reporte?> GetReporteWithDetailsAsync(int id)
        {
            return await _context.Reportes
                .Include(r => r.UsuarioReportes)
                    .ThenInclude(ur => ur.Usuario)
                .Include(r => r.ProductoReportes)
                    .ThenInclude(pr => pr.Producto)
                .FirstOrDefaultAsync(r => r.IdReporte == id);
        }
    }
}
