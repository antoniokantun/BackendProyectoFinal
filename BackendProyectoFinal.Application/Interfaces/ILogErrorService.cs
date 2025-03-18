using BackendProyectoFinal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.Interfaces
{
    public interface ILogErrorService
    {
        Task<IEnumerable<LogErrorDTO>> GetAllAsync();
        Task<LogErrorDTO> GetByIdAsync(int id);
        Task<IEnumerable<LogErrorDTO>> GetByUserIdAsync(int userId);
    }
}
