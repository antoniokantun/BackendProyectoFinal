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
    public class PerfilRepository : GenericRepository<Perfil>, IPerfilRepository
    {
        public PerfilRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Perfil>> GetPerfilesByUsuarioIdAsync(int usuarioId)
        {
            return await _dbSet.Where(p => p.UsuarioId == usuarioId).ToListAsync();
        }
    }
}
