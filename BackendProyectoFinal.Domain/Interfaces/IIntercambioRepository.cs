using BackendProyectoFinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces
{
    public interface IIntercambioRepository
    {
        Task<IEnumerable<Intercambio>> GetAllAsync();
        Task<Intercambio> GetByIdAsync(int id);
        Task<Intercambio> CreateAsync(Intercambio intercambio);
        Task UpdateAsync(Intercambio intercambio);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
