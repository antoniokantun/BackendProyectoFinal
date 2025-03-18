using BackendProyectoFinal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.Interfaces
{
    public interface IComentarioService
    {
        Task<IEnumerable<ComentarioDTO>> GetAllAsync();
        Task<ComentarioDTO> GetByIdAsync(int id);
        Task<IEnumerable<ComentarioDTO>> GetByProductIdAsync(int productoId);
        Task<IEnumerable<ComentarioDTO>> GetByUsuarioIdAsync(int usuarioId);
        Task<ComentarioDTO> CreateAsync(CreateComentarioDTO comentarioDto);
        Task UpdateAsync(int id, UpdateComentarioDTO comentarioDto);
        Task DeleteAsync(int id);
    }
}
