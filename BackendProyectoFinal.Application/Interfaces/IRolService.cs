using BackendProyectoFinal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.Interfaces
{
    public interface IRolService
    {
        Task<IEnumerable<RolDTO>> GetAllAsync();
        Task<RolDTO> GetByIdAsync(int id);
        Task<RolDTO> CreateAsync(RolDTO rolDto);
        Task UpdateAsync(RolDTO rolDto);
        Task DeleteAsync(int id);
    }
}
