using BackendProyectoFinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario?> FindByEmailAsync(string email);
        Task<Usuario?> GetByIdWithRolAsync(int id);
        Task<IEnumerable<Usuario>> GetAllWithRolAsync();

        Task<bool> ExistsAsync(int usuarioId);
    }
}
