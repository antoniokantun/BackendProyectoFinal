using BackendProyectoFinal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDTO>> GetAllAsync();
        Task<ProductoDTO> GetByIdAsync(int id);
        Task<ProductoDTO> CreateAsync(CreateProductoDTO productoDto);
        Task UpdateAsync(int id, UpdateProductoDTO productoDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ProductoDTO>> GetByUsuarioIdAsync(int usuarioId);
    }
}
