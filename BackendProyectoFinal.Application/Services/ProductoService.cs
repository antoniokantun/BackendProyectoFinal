﻿using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;
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
        private readonly IImagenRepository _imagenRepository;
        private readonly IImagenProductoRepository _imagenProductoRepository;
        private readonly ICategoriaProductoRepository _categoriaProductoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProductoService(
            IProductoRepository productoRepository,
            IImagenRepository imagenRepository,
            IImagenProductoRepository imagenProductoRepository,
            ICategoriaProductoRepository categoriaProductoRepository,
            ICategoriaRepository categoriaRepository)
        {
            _productoRepository = productoRepository;
            _imagenRepository = imagenRepository;
            _imagenProductoRepository = imagenProductoRepository;
            _categoriaProductoRepository = categoriaProductoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
        {
            var productos = await _productoRepository.GetAllAsync();
            var resultList = new List<ProductoDTO>();

            foreach (var producto in productos)
            {
                // Obtener imágenes del producto
                var imagenesProducto = await _imagenProductoRepository.GetByProductoIdWithImagenesAsync(producto.IdProducto);
                var imagenes = imagenesProducto.Select(ip => new ImagenDTO
                {
                    IdImagen = ip.Imagen.IdImagen,
                    UrlImagen = ip.Imagen.UrlImagen
                }).ToList();

                // Obtener categorías del producto
                var categoriasProducto = await _categoriaProductoRepository.GetByProductoIdAsync(producto.IdProducto);
                var categorias = categoriasProducto.Select(cp => new CategoriaDTO
                {
                    IdCategoria = cp.Categoria.IdCategoria,
                    Nombre = cp.Categoria.Nombre
                }).ToList();

                resultList.Add(new ProductoDTO
                {
                    IdProducto = producto.IdProducto,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    FechaCreacion = producto.FechaCreacion,
                    ProcesoNegociacion = producto.ProcesoNegociacion,
                    Intercambio = producto.Intercambio,
                    NoVisible= producto.NoVisible,
                    Reportado = producto.Reportado,
                    UsuarioId = producto.UsuarioId,
                    Imagenes = imagenes,
                    Categorias = categorias
                });
            }

            return resultList;
        }

        public async Task<ProductoDTO> GetByIdAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);

            if (producto == null)
                throw new KeyNotFoundException($"Producto con ID {id} no encontrado.");

            // Obtener imágenes del producto
            var imagenesProducto = await _imagenProductoRepository.GetByProductoIdAsync(producto.IdProducto);
            var imagenes = imagenesProducto.Select(ip => new ImagenDTO
            {
                IdImagen = ip.Imagen.IdImagen,
                UrlImagen = ip.Imagen.UrlImagen
            }).ToList();

            // Obtener categorías del producto
            var categoriasProducto = await _categoriaProductoRepository.GetByProductoIdAsync(producto.IdProducto);
            var categorias = categoriasProducto.Select(cp => new CategoriaDTO
            {
                IdCategoria = cp.Categoria.IdCategoria,
                Nombre = cp.Categoria.Nombre
            }).ToList();

            return new ProductoDTO
            {
                IdProducto = producto.IdProducto,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                FechaCreacion = producto.FechaCreacion,
                ProcesoNegociacion = producto.ProcesoNegociacion,
                Intercambio = producto.Intercambio,
                NoVisible = producto.NoVisible,
                Reportado = producto.Reportado,
                UsuarioId = producto.UsuarioId,
                Imagenes = imagenes,
                Categorias = categorias
            };
        }

        public async Task<ProductoDetailDTO> GetDetailByIdAsync(int id)
        {
            var producto = await _productoRepository.GetProductoCompletoByIdAsync(id);

            if (producto == null)
                throw new KeyNotFoundException($"Producto con ID {id} no encontrado.");

            var result = new ProductoDetailDTO
            {
                IdProducto = producto.IdProducto,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                FechaCreacion = producto.FechaCreacion,
                ProcesoNegociacion = producto.ProcesoNegociacion,
                Intercambio = producto.Intercambio,
                NoVisible     = producto.NoVisible,
                Reportado = producto.Reportado,
                UsuarioId = producto.UsuarioId,
                NombreUsuario = producto.Usuario?.Nombre ?? "Usuario no disponible",
                Imagenes = producto.ImagenProductos?.Select(ip => new ImagenDTO
                {
                    IdImagen = ip.Imagen.IdImagen,
                    UrlImagen = ip.Imagen.UrlImagen
                }).ToList() ?? new List<ImagenDTO>(),
                Categorias = producto.CategoriaProductos?.Select(cp => new CategoriaDTO
                {
                    IdCategoria = cp.Categoria.IdCategoria,
                    Nombre = cp.Categoria.Nombre
                }).ToList() ?? new List<CategoriaDTO>()
            };

            return result;
        }

        public async Task<ProductoDTO> CreateAsync(ProductoDTO productoDto)
        {
            var producto = new Producto
            {
                Nombre = productoDto.Nombre,
                Descripcion = productoDto.Descripcion,
                FechaCreacion = productoDto.FechaCreacion,
                ProcesoNegociacion = productoDto.ProcesoNegociacion,
                Intercambio = productoDto.Intercambio,
                NoVisible = productoDto.NoVisible,
                Reportado = productoDto.Reportado,
                UsuarioId = productoDto.UsuarioId
            };

            var createdProducto = await _productoRepository.AddAsync(producto);

            return new ProductoDTO
            {
                IdProducto = createdProducto.IdProducto,
                Nombre = createdProducto.Nombre,
                Descripcion = createdProducto.Descripcion,
                FechaCreacion = createdProducto.FechaCreacion,
                ProcesoNegociacion = createdProducto.ProcesoNegociacion,
                Intercambio = createdProducto.Intercambio,
                NoVisible = productoDto.NoVisible,
                Reportado = productoDto.Reportado,
                UsuarioId = createdProducto.UsuarioId
            };
        }

        public async Task<ProductoDTO> CreateProductoCompletoAsync(ProductoCreateDTO productoCreateDto)
        {
            // 1. Crear el producto
            var producto = new Producto
            {
                Nombre = productoCreateDto.Nombre,
                Descripcion = productoCreateDto.Descripcion,
                FechaCreacion = DateTime.Now,
                ProcesoNegociacion = productoCreateDto.ProcesoNegociacion,
                Intercambio = productoCreateDto.Intercambio,
                NoVisible = productoCreateDto.NoVisible,
                Reportado = productoCreateDto.Reportado,
                UsuarioId = productoCreateDto.UsuarioId
            };

            var createdProducto = await _productoRepository.AddAsync(producto);

            // 2. Procesar las imágenes
            var imagenes = new List<ImagenDTO>();
            foreach (var urlImagen in productoCreateDto.ImagenesUrl)
            {
                // Crear la imagen
                var imagen = new Imagen
                {
                    UrlImagen = urlImagen
                };
                var createdImagen = await _imagenRepository.AddAsync(imagen);

                // Crear la relación entre producto e imagen
                var imagenProducto = new ImagenProducto
                {
                    ProductoId = createdProducto.IdProducto,
                    ImagenId = createdImagen.IdImagen
                };
                await _imagenProductoRepository.AddAsync(imagenProducto);

                imagenes.Add(new ImagenDTO
                {
                    IdImagen = createdImagen.IdImagen,
                    UrlImagen = createdImagen.UrlImagen
                });
            }

            // 3. Procesar las categorías
            var categorias = new List<CategoriaDTO>();
            foreach (var categoriaId in productoCreateDto.CategoriasIds)
            {
                // Verificar si la categoría existe
                var categoria = await _categoriaRepository.GetByIdAsync(categoriaId);
                if (categoria == null)
                    continue;

                // Crear la relación entre producto y categoría
                var categoriaProducto = new CategoriaProducto
                {
                    ProductoId = createdProducto.IdProducto,
                    CategoriaId = categoriaId
                };
                await _categoriaProductoRepository.AddAsync(categoriaProducto);

                categorias.Add(new CategoriaDTO
                {
                    IdCategoria = categoria.IdCategoria,
                    Nombre = categoria.Nombre
                });
            }

            // 4. Devolver el producto creado con sus relaciones
            return new ProductoDTO
            {
                IdProducto = createdProducto.IdProducto,
                Nombre = createdProducto.Nombre,
                Descripcion = createdProducto.Descripcion,
                FechaCreacion = createdProducto.FechaCreacion,
                ProcesoNegociacion = createdProducto.ProcesoNegociacion,
                Intercambio = createdProducto.Intercambio,
                NoVisible = createdProducto.NoVisible,
                Reportado = createdProducto.Reportado,
                UsuarioId = createdProducto.UsuarioId,
                Imagenes = imagenes,
                Categorias = categorias
            };
        }

        public async Task UpdateAsync(ProductoDTO productoDto)
        {
            var existingProducto = await _productoRepository.GetByIdAsync(productoDto.IdProducto);

            if (existingProducto == null)
                throw new KeyNotFoundException($"Producto con ID {productoDto.IdProducto} no encontrado.");

            existingProducto.Nombre = productoDto.Nombre;
            existingProducto.Descripcion = productoDto.Descripcion;
            existingProducto.ProcesoNegociacion = productoDto.ProcesoNegociacion;
            existingProducto.Intercambio = productoDto.Intercambio;
            existingProducto.NoVisible = productoDto.NoVisible;
            existingProducto.Reportado = productoDto.Reportado;
            existingProducto.UsuarioId = productoDto.UsuarioId;

            await _productoRepository.UpdateAsync(existingProducto);
        }

        public async Task UpdateProductoCompletoAsync(int id, ProductoCreateDTO productoUpdateDto)
        {
            // 1. Verificar si el producto existe
            var existingProducto = await _productoRepository.GetByIdAsync(id);
            if (existingProducto == null)
                throw new KeyNotFoundException($"Producto con ID {id} no encontrado.");

            // 2. Actualizar datos básicos del producto
            existingProducto.Nombre = productoUpdateDto.Nombre;
            existingProducto.Descripcion = productoUpdateDto.Descripcion;
            existingProducto.ProcesoNegociacion = productoUpdateDto.ProcesoNegociacion;
            existingProducto.Intercambio = productoUpdateDto.Intercambio;
            existingProducto.NoVisible = productoUpdateDto.NoVisible;
            existingProducto.Reportado= productoUpdateDto.Reportado;
            existingProducto.UsuarioId = productoUpdateDto.UsuarioId;

            await _productoRepository.UpdateAsync(existingProducto);

            // 3. Actualizar imágenes (eliminar las existentes y agregar las nuevas)
            // 3.1 Obtener las relaciones de imágenes actuales
            var imagenesProductoActuales = await _imagenProductoRepository.GetByProductoIdAsync(id);

            // 3.2 Eliminar las relaciones y las imágenes actuales
            foreach (var imagenProducto in imagenesProductoActuales)
            {
                await _imagenProductoRepository.DeleteAsync(imagenProducto.IdImagenProducto);
                await _imagenRepository.DeleteAsync(imagenProducto.ImagenId);
            }

            // 3.3 Agregar las nuevas imágenes
            foreach (var urlImagen in productoUpdateDto.ImagenesUrl)
            {
                // Crear la imagen
                var imagen = new Imagen
                {
                    UrlImagen = urlImagen
                };
                var createdImagen = await _imagenRepository.AddAsync(imagen);

                // Crear la relación entre producto e imagen
                var imagenProducto = new ImagenProducto
                {
                    ProductoId = id,
                    ImagenId = createdImagen.IdImagen
                };
                await _imagenProductoRepository.AddAsync(imagenProducto);
            }

            // 4. Actualizar categorías (eliminar las existentes y agregar las nuevas)
            // 4.1 Obtener las relaciones de categorías actuales
            var categoriasProductoActuales = await _categoriaProductoRepository.GetByProductoIdAsync(id);

            // 4.2 Eliminar las relaciones actuales
            foreach (var categoriaProducto in categoriasProductoActuales)
            {
                await _categoriaProductoRepository.DeleteAsync(categoriaProducto.IdCategoriaProducto);
            }

            // 4.3 Agregar las nuevas categorías
            foreach (var categoriaId in productoUpdateDto.CategoriasIds)
            {
                // Verificar si la categoría existe
                var categoria = await _categoriaRepository.GetByIdAsync(categoriaId);
                if (categoria == null)
                    continue;

                // Crear la relación entre producto y categoría
                var categoriaProducto = new CategoriaProducto
                {
                    ProductoId = id,
                    CategoriaId = categoriaId
                };
                await _categoriaProductoRepository.AddAsync(categoriaProducto);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var existingProducto = await _productoRepository.GetByIdAsync(id);

            if (existingProducto == null)
                throw new KeyNotFoundException($"Producto con ID {id} no encontrado.");

            await _productoRepository.DeleteAsync(id);
        }

        public async Task DeleteProductoCompletoAsync(int id)
        {
            // 1. Verificar si el producto existe
            var existingProducto = await _productoRepository.GetByIdAsync(id);
            if (existingProducto == null)
                throw new KeyNotFoundException($"Producto con ID {id} no encontrado.");

            // 2. Eliminar primero las relaciones con imágenes y las imágenes
            var imagenesProducto = await _imagenProductoRepository.GetByProductoIdAsync(id);
            foreach (var imagenProducto in imagenesProducto)
            {
                // Eliminar la relación
                await _imagenProductoRepository.DeleteAsync(imagenProducto.IdImagenProducto);

                // Eliminar la imagen
                await _imagenRepository.DeleteAsync(imagenProducto.ImagenId);
            }

            // 3. Eliminar las relaciones con categorías
            var categoriasProducto = await _categoriaProductoRepository.GetByProductoIdAsync(id);
            foreach (var categoriaProducto in categoriasProducto)
            {
                await _categoriaProductoRepository.DeleteAsync(categoriaProducto.IdCategoriaProducto);
            }

            // 4. Finalmente, eliminar el producto
            await _productoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductoDTO>> GetByUsuarioIdAsync(int usuarioId)
        {
            var productos = await _productoRepository.GetByUsuarioIdAsync(usuarioId);
            var resultList = new List<ProductoDTO>();

            foreach (var producto in productos)
            {
                // Obtener imágenes del producto
                var imagenesProducto = await _imagenProductoRepository.GetByProductoIdAsync(producto.IdProducto);
                var imagenes = imagenesProducto.Select(ip => new ImagenDTO
                {
                    IdImagen = ip.Imagen.IdImagen,
                    UrlImagen = ip.Imagen.UrlImagen
                }).ToList();

                // Obtener categorías del producto
                var categoriasProducto = await _categoriaProductoRepository.GetByProductoIdAsync(producto.IdProducto);
                var categorias = categoriasProducto.Select(cp => new CategoriaDTO
                {
                    IdCategoria = cp.Categoria.IdCategoria,
                    Nombre = cp.Categoria.Nombre
                }).ToList();

                resultList.Add(new ProductoDTO
                {
                    IdProducto = producto.IdProducto,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    FechaCreacion = producto.FechaCreacion,
                    ProcesoNegociacion = producto.ProcesoNegociacion,
                    Intercambio = producto.Intercambio,
                    NoVisible= producto.NoVisible,
                    Reportado= producto.Reportado,
                    UsuarioId = producto.UsuarioId,
                    Imagenes = imagenes,
                    Categorias = categorias
                });
            }

            return resultList;
        }

        // BackendProyectoFinal.Application/Services/ProductoService.cs
        // Añadir este método a la clase existente
        public async Task<ProductoDTO> UpdateProductoCompletoConImagenesAsync(int id, List<int> imagenesExistentesIds, List<string> nuevasImagenesUrls, ProductoCreateDTO productoUpdateDto)
        {
            // 1. Verificar si el producto existe
            var existingProducto = await _productoRepository.GetByIdAsync(id);
            if (existingProducto == null)
                throw new KeyNotFoundException($"Producto con ID {id} no encontrado.");

            // 2. Actualizar datos básicos del producto¿'¿'
            existingProducto.Nombre = productoUpdateDto.Nombre;
            existingProducto.Descripcion = productoUpdateDto.Descripcion;
            existingProducto.ProcesoNegociacion = productoUpdateDto.ProcesoNegociacion;
            existingProducto.Intercambio = productoUpdateDto.Intercambio;
            existingProducto.NoVisible = productoUpdateDto.NoVisible;
            existingProducto.Reportado = productoUpdateDto.Reportado;
            existingProducto.UsuarioId = productoUpdateDto.UsuarioId;

            await _productoRepository.UpdateAsync(existingProducto);

            // 3. Actualizar imágenes (mantener las seleccionadas, eliminar las no seleccionadas y agregar las nuevas)
            // 3.1 Obtener las imágenes actuales
            var imagenesProductoActuales = await _imagenProductoRepository.GetByProductoIdAsync(id);

            // 3.2 Identificar y eliminar las imágenes que ya no se quieren mantener
            var imagenesAEliminar = imagenesProductoActuales
                .Where(ip => !imagenesExistentesIds.Contains(ip.ImagenId))
                .ToList();

            foreach (var imagenProducto in imagenesAEliminar)
            {
                await _imagenProductoRepository.DeleteAsync(imagenProducto.IdImagenProducto);
                await _imagenRepository.DeleteAsync(imagenProducto.ImagenId);
            }

            // 3.3 Agregar las nuevas imágenes
            foreach (var urlImagen in nuevasImagenesUrls)
            {
                var imagen = new Imagen { UrlImagen = urlImagen };
                var createdImagen = await _imagenRepository.AddAsync(imagen);

                var imagenProducto = new ImagenProducto
                {
                    ProductoId = id,
                    ImagenId = createdImagen.IdImagen
                };
                await _imagenProductoRepository.AddAsync(imagenProducto);
            }

            // 4. Actualizar categorías (eliminar las existentes y agregar las nuevas)
            // 4.1 Obtener las relaciones de categorías actuales
            var categoriasProductoActuales = await _categoriaProductoRepository.GetByProductoIdAsync(id);

            // 4.2 Eliminar las relaciones actuales
            foreach (var categoriaProducto in categoriasProductoActuales)
            {
                await _categoriaProductoRepository.DeleteAsync(categoriaProducto.IdCategoriaProducto);
            }

            // 4.3 Agregar las nuevas categorías
            foreach (var categoriaId in productoUpdateDto.CategoriasIds)
            {
                var categoria = await _categoriaRepository.GetByIdAsync(categoriaId);
                if (categoria == null)
                    continue;

                var categoriaProducto = new CategoriaProducto
                {
                    ProductoId = id,
                    CategoriaId = categoriaId
                };
                await _categoriaProductoRepository.AddAsync(categoriaProducto);
            }

            // 5. Devolver el producto actualizado con sus nuevas relaciones
            return await GetByIdAsync(id);
        }

        //Servicio para actualizar 
        public async Task UpdateProductVisibility(ProductoUpdateVisibilityDTO productoUpdateVisibilityDTO)
        {
            var producto = await _productoRepository.GetProductoCompletoByIdAsync(productoUpdateVisibilityDTO.IdProducto);

            if (producto == null)
            {
                // Manejar el caso donde el producto no existe.
                throw new KeyNotFoundException($"Producto con ID {productoUpdateVisibilityDTO.IdProducto} no encontrado.");
            }

            producto.NoVisible = productoUpdateVisibilityDTO.NoVisible;

            await _productoRepository.UpdateAsync(producto);
        }

        //Servicio para actualizar productos reportados

        public async Task UpdateProductReport(ProductReportDTO productReportDTO)
        {
            var producto = await _productoRepository.GetProductoCompletoByIdAsync(productReportDTO.IdProducto);

            if (producto == null)
            {
                // Manejar el caso donde el producto no existe.
                throw new KeyNotFoundException($"Producto con ID {productReportDTO.IdProducto} no encontrado.");
            }

            producto.Reportado = productReportDTO.Reportado;

            await _productoRepository.UpdateAsync(producto);

        }

        public async Task UpdateProductoParcialAsync(ProductPatchDTO productoUpdateDto)
        {
            // 1. Verificar si el producto existe
            var existingProducto = await _productoRepository.GetByIdAsync(productoUpdateDto.IdProducto);
            if (existingProducto == null)
                throw new KeyNotFoundException($"Producto con ID {productoUpdateDto.IdProducto} no encontrado.");

            // 2. Actualizar campos básicos del producto (si se proporcionan)
            if (productoUpdateDto.Nombre != null)
                existingProducto.Nombre = productoUpdateDto.Nombre;
            if (productoUpdateDto.Descripcion != null)
                existingProducto.Descripcion = productoUpdateDto.Descripcion;
            if (productoUpdateDto.Intercambio.HasValue)
                existingProducto.Intercambio = productoUpdateDto.Intercambio.Value;

            await _productoRepository.UpdateAsync(existingProducto);

            // 3. Actualizar imágenes (si se proporcionan)
            if (productoUpdateDto.Imagenes != null)
            {
                // 3.1 Obtener las relaciones de imágenes actuales
                var imagenesProductoActuales = await _imagenProductoRepository.GetByProductoIdAsync(productoUpdateDto.IdProducto);

                // 3.2 Eliminar las relaciones y las imágenes actuales
                foreach (var imagenProducto in imagenesProductoActuales)
                {
                    await _imagenProductoRepository.DeleteAsync(imagenProducto.IdImagenProducto);
                    await _imagenRepository.DeleteAsync(await _imagenRepository.GetByIdAsync(imagenProducto.ImagenId));
                }

                // 3.3 Agregar las nuevas imágenes
                foreach (var imagenDto in productoUpdateDto.Imagenes)
                {
                    // Crear la imagen
                    var imagen = new Imagen
                    {
                        UrlImagen = imagenDto.UrlImagen
                    };
                    var createdImagen = await _imagenRepository.AddAsync(imagen);

                    // Crear la relación entre producto e imagen
                    var imagenProducto = new ImagenProducto
                    {
                        ProductoId = productoUpdateDto.IdProducto,
                        ImagenId = createdImagen.IdImagen
                    };
                    await _imagenProductoRepository.AddAsync(imagenProducto);
                }
            }

            if (productoUpdateDto.CategoriasIds != null)
            {
                // 4.1 Obtener las relaciones de categorías actuales
                var categoriasProductoActuales = await _categoriaProductoRepository.GetByProductoIdAsync(productoUpdateDto.IdProducto);

                // 4.2 Eliminar las relaciones actuales
                foreach (var categoriaProducto in categoriasProductoActuales)
                {
                    await _categoriaProductoRepository.DeleteAsync(categoriaProducto.IdCategoriaProducto);
                }

                // 4.3 Agregar las nuevas categorías
                foreach (var categoriaId in productoUpdateDto.CategoriasIds) // Cambiamos a categoriaId
                {
                    // Verificar si la categoría existe
                    var categoria = await _categoriaRepository.GetByIdAsync(categoriaId);
                    if (categoria == null)
                        continue;

                    // Crear la relación entre producto y categoría
                    var categoriaProducto = new CategoriaProducto
                    {
                        ProductoId = productoUpdateDto.IdProducto,
                        CategoriaId = categoriaId // Usamos categoriaId directamente
                    };
                    await _categoriaProductoRepository.AddAsync(categoriaProducto);
                }
            }
        }



    }
}
