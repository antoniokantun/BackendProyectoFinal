using BackendProyectoFinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface ILogErrorRepository
    {
        Task<LogError> CreateAsync(LogError logError);
        Task<IEnumerable<LogError>> GetAllAsync();
        Task<LogError> GetByIdAsync(int id);
        Task<IEnumerable<LogError>> GetByUserIdAsync(int userId);
    }
}
