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
    public class PerfilRepository : IPerfilRepository
    {
        private readonly ApplicationDbContext _context;

        public PerfilRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Perfil>> GetAllAsync()
        {
            return await _context.Perfiles.Include(p => p.Usuario).ToListAsync();
        }

        public async Task<Perfil> GetByIdAsync(int id)
        {
            return await _context.Perfiles.Include(p => p.Usuario).FirstOrDefaultAsync(p => p.IdPerfil == id);
        }

        public async Task<Perfil> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Perfiles.Include(p => p.Usuario).FirstOrDefaultAsync(p => p.UsuarioId == usuarioId);
        }

        public async Task<Perfil> CreateAsync(Perfil perfil)
        {
            _context.Perfiles.Add(perfil);
            await _context.SaveChangesAsync();
            return perfil;
        }

        public async Task UpdateAsync(Perfil perfil)
        {
            _context.Entry(perfil).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var perfil = await _context.Perfiles.FindAsync(id);
            if (perfil != null)
            {
                _context.Perfiles.Remove(perfil);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Perfiles.AnyAsync(p => p.IdPerfil == id);
        }

        public async Task<bool> ExistsByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Perfiles.AnyAsync(p => p.UsuarioId == usuarioId);
        }
    }
}
