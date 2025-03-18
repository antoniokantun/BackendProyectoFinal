using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Infrastructure.Persistence.Data;
using BackendProyectoFinal.Infrastructure.RepositoriesBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Usuario?> FindByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.CorreoElectronico == email);
        }

        public async Task<Usuario?> GetByIdWithRolAsync(int id)
        {
            return await _dbSet
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task<IEnumerable<Usuario>> GetAllWithRolAsync()
        {
            return await _dbSet
                .Include(u => u.Rol)
                .ToListAsync();
        }
    }
}
