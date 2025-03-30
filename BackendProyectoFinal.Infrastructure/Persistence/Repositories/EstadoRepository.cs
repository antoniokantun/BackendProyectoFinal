using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;


namespace BackendProyectoFinal.Infrastructure.Persistence.Repositories
{
    public class EstadoRepository : GenericRepository<Estado>, IEstadoRepository
    {
        public EstadoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Estado?> GetByNombreAsync(string nombre)
        {
            return await _dbSet
                .FirstOrDefaultAsync(e => e.Nombre.ToLower() == nombre.ToLower());
        }

        // Sobreescribimos el método GetByIdAsync para incluir las entidades relacionadas
        public async new Task<Estado?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Where(e => e.IdEstado == id)
                .Include(e => e.Intercambios)
                .FirstOrDefaultAsync();
        }

        // Sobreescribimos el método GetAllAsync para incluir las entidades relacionadas
        public async new Task<IEnumerable<Estado>> GetAllAsync()
        {
            return await _dbSet
                .Include(e => e.Intercambios)
                .ToListAsync();
        }
    }
}
