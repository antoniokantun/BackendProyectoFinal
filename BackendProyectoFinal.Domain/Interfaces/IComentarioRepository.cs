using BackendProyectoFinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IComentarioRepository
    {
        Task<IEnumerable<Comentario>> GetAllAsync();
        Task<Comentario> GetByIdAsync(int id);
        Task<IEnumerable<Comentario>> GetByProductIdAsync(int productoId);
        Task<IEnumerable<Comentario>> GetByUsuarioIdAsync(int usuarioId);
        Task<IEnumerable<Comentario>> GetRootCommentsByProductIdAsync(int productoId);
        Task<Comentario> CreateAsync(Comentario comentario);
        Task UpdateAsync(Comentario comentario);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
