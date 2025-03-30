using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;


namespace BackendProyectoFinal.Infrastructure.Persistence.Repositories
{
    public class ProductoReporteRepository : GenericRepository<ProductoReporte>, IProductoReporteRepository
    {
        public ProductoReporteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProductoReporte>> GetAllProductoReportesWithDetailsAsync()
        {
            return await _context.ProductoReportes
                .Include(pr => pr.Producto)
                .Include(pr => pr.Reporte)
                .ToListAsync();
        }
    }
}
