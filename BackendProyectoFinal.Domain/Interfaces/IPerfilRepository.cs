using BackendProyectoFinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IPerfilRepository
    {
        Task<IEnumerable<Perfil>> GetAllAsync();
        Task<Perfil> GetByIdAsync(int id);
        Task<Perfil> GetByUsuarioIdAsync(int usuarioId);
        Task<Perfil> CreateAsync(Perfil perfil);
        Task UpdateAsync(Perfil perfil);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByUsuarioIdAsync(int usuarioId);
    }
}
