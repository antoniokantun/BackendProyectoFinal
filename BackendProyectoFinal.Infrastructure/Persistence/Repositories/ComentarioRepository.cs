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
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly ApplicationDbContext _context;

        public ComentarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comentario>> GetAllAsync()
        {
            return await _context.Comentarios
                .Include(c => c.Usuario)
                .Include(c => c.Producto)
                .Include(c => c.ComentariosHijos)
                .ToListAsync();
        }

        public async Task<Comentario> GetByIdAsync(int id)
        {
            return await _context.Comentarios
                .Include(c => c.Usuario)
                .Include(c => c.Producto)
                .Include(c => c.ComentariosHijos)
                .FirstOrDefaultAsync(c => c.IdComentario == id);
        }

        public async Task<IEnumerable<Comentario>> GetByProductIdAsync(int productoId)
        {
            return await _context.Comentarios
                .Include(c => c.Usuario)
                .Include(c => c.ComentariosHijos)
                .Where(c => c.ProductoId == productoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comentario>> GetRootCommentsByProductIdAsync(int productoId)
        {
            return await _context.Comentarios
                .Include(c => c.Usuario)
                .Include(c => c.ComentariosHijos)
                    .ThenInclude(ch => ch.Usuario)
                .Where(c => c.ProductoId == productoId && c.ComentarioPadreId == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comentario>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Comentarios
                .Include(c => c.Producto)
                .Include(c => c.ComentariosHijos)
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<Comentario> CreateAsync(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();
            return comentario;
        }

        public async Task UpdateAsync(Comentario comentario)
        {
            _context.Entry(comentario).State = EntityState.Modified;
            // Evitar actualizar estos campos
            _context.Entry(comentario).Property(x => x.FechaCreacion).IsModified = false;
            _context.Entry(comentario).Property(x => x.UsuarioId).IsModified = false;
            _context.Entry(comentario).Property(x => x.ProductoId).IsModified = false;
            _context.Entry(comentario).Property(x => x.ComentarioPadreId).IsModified = false;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario != null)
            {
                _context.Comentarios.Remove(comentario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Comentarios.AnyAsync(c => c.IdComentario == id);
        }
    }
}
