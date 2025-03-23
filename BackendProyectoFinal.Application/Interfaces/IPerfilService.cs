using BackendProyectoFinal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.Interfaces
{
    public interface IPerfilService
    {
        Task<IEnumerable<PerfilDTO>> GetAllAsync();
        Task<PerfilDTO> GetByIdAsync(int id);
        Task<IEnumerable<PerfilDTO>> GetByUsuarioIdAsync(int usuarioId);
        Task<PerfilDTO> CreateAsync(CreatePerfilDTO perfilDto);
        Task UpdateAsync(int id, UpdatePerfilDTO perfilDto);
        Task DeleteAsync(int id);
    }
}
