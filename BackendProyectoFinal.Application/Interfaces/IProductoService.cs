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
        Task<ProductoDetailDTO> GetDetailByIdAsync(int id);
        Task<ProductoDTO> GetByIdAsync(int id);
        Task<ProductoDTO> CreateAsync(ProductoDTO productoDto);
        Task<ProductoDTO> CreateProductoCompletoAsync(ProductoCreateDTO productoCreateDto);
        Task UpdateAsync(ProductoDTO productoDto);
        Task UpdateProductoCompletoAsync(int id, ProductoCreateDTO productoUpdateDto);
        Task DeleteAsync(int id);
        Task DeleteProductoCompletoAsync(int id);
        Task<IEnumerable<ProductoDTO>> GetByUsuarioIdAsync(int usuarioId);
        Task<ProductoDTO> UpdateProductoCompletoConImagenesAsync(int id, List<int> imagenesExistentesIds, List<string> nuevasImagenesUrls, ProductoCreateDTO productoUpdateDto);

        Task UpdateProductVisibility(ProductoUpdateVisibilityDTO productoUpdateVisibilityDTO);

        Task UpdateProductReport(ProductReportDTO productReportDTO);

        
    }
}
