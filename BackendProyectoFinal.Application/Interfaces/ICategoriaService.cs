using BackendProyectoFinal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDTO>> GetAllAsync();
        Task<CategoriaDTO> GetByIdAsync(int id);
        Task<CategoriaDTO> CreateAsync(CategoriaDTO categoriaDto);
        Task UpdateAsync(CategoriaDTO categoriaDto);
        Task DeleteAsync(int id);
    }
}
