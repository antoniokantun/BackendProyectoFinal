using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;


namespace BackendProyectoFinal.Infrastructure.Persistence.Repositories
{
    public class IntercambioRepository : GenericRepository<Intercambio>, IIntercambioRepository
    {
        public IntercambioRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Intercambio>> GetByUsuarioSolicitanteIdAsync(int usuarioSolicitanteId)
        {
            return await _dbSet
                .Where(i => i.UsuarioSolicitanteId == usuarioSolicitanteId)
                .Include(i => i.UsuarioSolicitante)
                .Include(i => i.UsuarioOfertante)
                .Include(i => i.Producto)
                .ToListAsync();
        }

        public async Task<IEnumerable<Intercambio>> GetByUsuarioOfertanteIdAsync(int usuarioOfertanteId)
        {
            return await _dbSet
                .Where(i => i.UsuarioOfertanteId == usuarioOfertanteId)
                .Include(i => i.UsuarioSolicitante)
                .Include(i => i.UsuarioOfertante)
                .Include(i => i.Producto)
                .ToListAsync();
        }

        public async Task<IEnumerable<Intercambio>> GetByProductoIdAsync(int productoId)
        {
            return await _dbSet
                .Where(i => i.ProductoId == productoId)
                .Include(i => i.UsuarioSolicitante)
                .Include(i => i.UsuarioOfertante)
                .Include(i => i.Producto)
                .ToListAsync();
        }

        // Sobreescribimos el método GetByIdAsync para incluir las entidades relacionadas
        public async new Task<Intercambio?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Where(i => i.IdIntercambio == id)
                .Include(i => i.UsuarioSolicitante)
                .Include(i => i.UsuarioOfertante)
                .Include(i => i.Producto)
                .FirstOrDefaultAsync();
        }

        // Sobreescribimos el método GetAllAsync para incluir las entidades relacionadas
        public async new Task<IEnumerable<Intercambio>> GetAllAsync()
        {
            return await _dbSet
                .Include(i => i.UsuarioSolicitante)
                .Include(i => i.UsuarioOfertante)
                .Include(i => i.Producto)
                .ToListAsync();
        }
    }
}
