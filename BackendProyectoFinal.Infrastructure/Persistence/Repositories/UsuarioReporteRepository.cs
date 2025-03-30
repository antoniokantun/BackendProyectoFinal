using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;


namespace BackendProyectoFinal.Infrastructure.Persistence.Repositories
{
    public class UsuarioReporteRepository : GenericRepository<UsuarioReporte>, IUsuarioReporteRepository
    {
        public UsuarioReporteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UsuarioReporte>> GetAllUsuarioReportesWithDetailsAsync()
        {
            return await _context.UsuarioReportes
                .Include(ur => ur.Usuario)
                .Include(ur => ur.Reporte)
                .ToListAsync();
        }
    }
}
