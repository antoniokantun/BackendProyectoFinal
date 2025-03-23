using BackendProyectoFinal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.Interfaces
{
    public interface IIntercambioService
    {
        Task<IEnumerable<IntercambioDTO>> GetAllAsync();
        Task<IntercambioDTO> GetByIdAsync(int id);
        Task<IntercambioDTO> CreateAsync(CreateIntercambioDTO intercambioDto);
        Task UpdateAsync(int id, UpdateIntercambioDTO intercambioDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<IntercambioDTO>> GetByUsuarioSolicitanteIdAsync(int usuarioSolicitanteId);
        Task<IEnumerable<IntercambioDTO>> GetByUsuarioOfertanteIdAsync(int usuarioOfertanteId);
        Task<IEnumerable<IntercambioDTO>> GetByProductoIdAsync(int productoId);
    }
}
