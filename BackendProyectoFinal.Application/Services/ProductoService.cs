using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IErrorHandlingService _errorHandlingService;

        public ProductoService(IProductoRepository productoRepository, IErrorHandlingService errorHandlingService)
        {
            _productoRepository = productoRepository;
            _errorHandlingService = errorHandlingService;
        }

        public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
        {
            try
            {
                var productos = await _productoRepository.GetAllAsync();
                return productos.Select(p => new ProductoDTO
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    FechaCreacion = p.FechaCreacion,
                    ProcesoNegociacion = p.ProcesoNegociacion,
                    Intercambio = p.Intercambio,
                    UsuarioId = p.UsuarioId
                });
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetAllAsync));
                throw;
            }
        }

        public async Task<ProductoDTO> GetByIdAsync(int id)
        {
            try
            {
                var producto = await _productoRepository.GetByIdAsync(id);
                if (producto == null)
                {
                    await _errorHandlingService.LogErrorAsync($"Producto con ID {id} no encontrado", nameof(GetByIdAsync), "Advertencia");
                    throw new KeyNotFoundException($"Producto con ID {id} no encontrado");
                }

                return new ProductoDTO
                {
                    IdProducto = producto.IdProducto,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    FechaCreacion = producto.FechaCreacion,
                    ProcesoNegociacion = producto.ProcesoNegociacion,
                    Intercambio = producto.Intercambio,
                    UsuarioId = producto.UsuarioId
                };
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetByIdAsync));
                throw;
            }
        }

        public async Task<IEnumerable<ProductoDTO>> GetByUsuarioIdAsync(int usuarioId)
        {
            try
            {
                var productos = await _productoRepository.GetByUsuarioIdAsync(usuarioId);
                return productos.Select(p => new ProductoDTO
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    FechaCreacion = p.FechaCreacion,
                    ProcesoNegociacion = p.ProcesoNegociacion,
                    Intercambio = p.Intercambio,
                    UsuarioId = p.UsuarioId
                });
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetByUsuarioIdAsync));
                throw;
            }
        }

        public async Task<ProductoDTO> CreateAsync(CreateProductoDTO productoDto)
        {
            try
            {
                var producto = new Producto
                {
                    Nombre = productoDto.Nombre,
                    Descripcion = productoDto.Descripcion,
                    FechaCreacion = DateTime.Now,
                    ProcesoNegociacion = productoDto.ProcesoNegociacion,
                    Intercambio = productoDto.Intercambio,
                    UsuarioId = productoDto.UsuarioId
                };

                var createdProducto = await _productoRepository.CreateAsync(producto);

                return new ProductoDTO
                {
                    IdProducto = createdProducto.IdProducto,
                    Nombre = createdProducto.Nombre,
                    Descripcion = createdProducto.Descripcion,
                    FechaCreacion = createdProducto.FechaCreacion,
                    ProcesoNegociacion = createdProducto.ProcesoNegociacion,
                    Intercambio = createdProducto.Intercambio,
                    UsuarioId = createdProducto.UsuarioId
                };
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(CreateAsync));
                throw;
            }
        }

        public async Task UpdateAsync(int id, UpdateProductoDTO productoDto)
        {
            try
            {
                // Verificar si el producto existe
                var producto = await _productoRepository.GetByIdAsync(id);
                if (producto == null)
                {
                    await _errorHandlingService.LogErrorAsync($"Producto con ID {id} no encontrado para actualizar", nameof(UpdateAsync), "Advertencia");
                    throw new KeyNotFoundException($"Producto con ID {id} no encontrado");
                }

                producto.Nombre = productoDto.Nombre;
                producto.Descripcion = productoDto.Descripcion;
                producto.ProcesoNegociacion = productoDto.ProcesoNegociacion;
                producto.Intercambio = productoDto.Intercambio;

                await _productoRepository.UpdateAsync(producto);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(UpdateAsync));
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                // Verificar si el producto existe
                if (!await _productoRepository.ExistsAsync(id))
                {
                    await _errorHandlingService.LogErrorAsync($"Producto con ID {id} no encontrado para eliminar", nameof(DeleteAsync), "Advertencia");
                    throw new KeyNotFoundException($"Producto con ID {id} no encontrado");
                }

                await _productoRepository.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(DeleteAsync));
                throw;
            }
        }
    }
}
