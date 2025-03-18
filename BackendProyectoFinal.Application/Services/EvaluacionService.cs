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
    public class EvaluacionService : IEvaluacionService
    {
        private readonly IEvaluacionRepository _evaluacionRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IErrorHandlingService _errorHandlingService;

        public EvaluacionService(
            IEvaluacionRepository evaluacionRepository,
            IProductoRepository productoRepository,
            IUsuarioRepository usuarioRepository,
            IErrorHandlingService errorHandlingService)
        {
            _evaluacionRepository = evaluacionRepository;
            _productoRepository = productoRepository;
            _usuarioRepository = usuarioRepository;
            _errorHandlingService = errorHandlingService;
        }

        public async Task<IEnumerable<EvaluacionDTO>> GetAllAsync()
        {
            try
            {
                var evaluaciones = await _evaluacionRepository.GetAllWithDetailsAsync();
                return MapToEvaluacionesDTO(evaluaciones);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetAllAsync));
                throw;
            }
        }

        public async Task<EvaluacionDTO> GetByIdAsync(int id)
        {
            try
            {
                var evaluacion = await _evaluacionRepository.GetByIdWithDetailsAsync(id);
                if (evaluacion == null)
                {
                    await _errorHandlingService.LogErrorAsync($"Evaluación con ID {id} no encontrada", nameof(GetByIdAsync), "Advertencia");
                    throw new KeyNotFoundException($"Evaluación con ID {id} no encontrada");
                }

                return MapToEvaluacionDTO(evaluacion);
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

        public async Task<IEnumerable<EvaluacionDTO>> GetByProductIdAsync(int productoId)
        {
            try
            {
                // Verificar si el producto existe
                if (!await _productoRepository.ExistsAsync(productoId))
                {
                    await _errorHandlingService.LogErrorAsync($"Producto con ID {productoId} no encontrado", nameof(GetByProductIdAsync), "Advertencia");
                    throw new KeyNotFoundException($"Producto con ID {productoId} no encontrado");
                }

                var evaluaciones = await _evaluacionRepository.GetByProductIdAsync(productoId);
                return MapToEvaluacionesDTO(evaluaciones);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetByProductIdAsync));
                throw;
            }
        }

        public async Task<IEnumerable<EvaluacionDTO>> GetByUsuarioIdAsync(int usuarioId)
        {
            try
            {
                // Verificar si el usuario existe
                if (!await _usuarioRepository.ExistsAsync(usuarioId))
                {
                    await _errorHandlingService.LogErrorAsync($"Usuario con ID {usuarioId} no encontrado", nameof(GetByUsuarioIdAsync), "Advertencia");
                    throw new KeyNotFoundException($"Usuario con ID {usuarioId} no encontrado");
                }

                var evaluaciones = await _evaluacionRepository.GetByUsuarioIdAsync(usuarioId);
                return MapToEvaluacionesDTO(evaluaciones);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetByUsuarioIdAsync));
                throw;
            }
        }

        public async Task<double> GetPromedioByProductIdAsync(int productoId)
        {
            try
            {
                // Verificar si el producto existe
                if (!await _productoRepository.ExistsAsync(productoId))
                {
                    await _errorHandlingService.LogErrorAsync($"Producto con ID {productoId} no encontrado", nameof(GetPromedioByProductIdAsync), "Advertencia");
                    throw new KeyNotFoundException($"Producto con ID {productoId} no encontrado");
                }

                return await _evaluacionRepository.GetPromedioByProductIdAsync(productoId);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetPromedioByProductIdAsync));
                throw;
            }
        }

        public async Task<EvaluacionDTO> CreateAsync(CreateEvaluacionDTO evaluacionDto)
        {
            try
            {
                // Verificar si el producto existe
                if (!await _productoRepository.ExistsAsync(evaluacionDto.ProductoId))
                {
                    await _errorHandlingService.LogErrorAsync($"Producto con ID {evaluacionDto.ProductoId} no encontrado", nameof(CreateAsync), "Advertencia");
                    throw new KeyNotFoundException($"Producto con ID {evaluacionDto.ProductoId} no encontrado");
                }

                // Verificar si el usuario existe
                if (!await _usuarioRepository.ExistsAsync(evaluacionDto.UsuarioId))
                {
                    await _errorHandlingService.LogErrorAsync($"Usuario con ID {evaluacionDto.UsuarioId} no encontrado", nameof(CreateAsync), "Advertencia");
                    throw new KeyNotFoundException($"Usuario con ID {evaluacionDto.UsuarioId} no encontrado");
                }

                // Verificar si ya existe una evaluación del usuario para este producto
                if (await _evaluacionRepository.ExisteEvaluacionUsuarioProductoAsync(evaluacionDto.UsuarioId, evaluacionDto.ProductoId))
                {
                    await _errorHandlingService.LogErrorAsync($"Ya existe una evaluación del usuario {evaluacionDto.UsuarioId} para el producto {evaluacionDto.ProductoId}", nameof(CreateAsync), "Advertencia");
                    throw new InvalidOperationException($"Ya existe una evaluación del usuario para este producto");
                }

                var evaluacion = new Evaluacion
                {
                    UsuarioId = evaluacionDto.UsuarioId,
                    ProductoId = evaluacionDto.ProductoId,
                    FechaCreacion = DateTime.Now,
                    Comentario = evaluacionDto.Comentario,
                    Puntuacion = evaluacionDto.Puntuacion
                };

                var createdEvaluacion = await _evaluacionRepository.CreateAsync(evaluacion);

                // Obtener la evaluación con todos los detalles para el DTO
                var evaluacionConDetalles = await _evaluacionRepository.GetByIdWithDetailsAsync(createdEvaluacion.IdEvaluacion);

                return MapToEvaluacionDTO(evaluacionConDetalles!);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(CreateAsync));
                throw;
            }
        }

        public async Task UpdateAsync(int id, UpdateEvaluacionDTO evaluacionDto)
        {
            try
            {
                // Verificar si la evaluación existe
                var evaluacion = await _evaluacionRepository.GetByIdAsync(id);
                if (evaluacion == null)
                {
                    await _errorHandlingService.LogErrorAsync($"Evaluación con ID {id} no encontrada para actualizar", nameof(UpdateAsync), "Advertencia");
                    throw new KeyNotFoundException($"Evaluación con ID {id} no encontrada");
                }

                evaluacion.Comentario = evaluacionDto.Comentario;
                evaluacion.Puntuacion = evaluacionDto.Puntuacion;

                await _evaluacionRepository.UpdateAsync(evaluacion);
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
                // Verificar si la evaluación existe
                if (!await _evaluacionRepository.ExistsAsync(id))
                {
                    await _errorHandlingService.LogErrorAsync($"Evaluación con ID {id} no encontrada para eliminar", nameof(DeleteAsync), "Advertencia");
                    throw new KeyNotFoundException($"Evaluación con ID {id} no encontrada");
                }

                await _evaluacionRepository.DeleteAsync(id);
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

        public async Task<bool> ExisteEvaluacionUsuarioProductoAsync(int usuarioId, int productoId)
        {
            try
            {
                return await _evaluacionRepository.ExisteEvaluacionUsuarioProductoAsync(usuarioId, productoId);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(ExisteEvaluacionUsuarioProductoAsync));
                throw;
            }
        }

        // Métodos privados para mapear entidades a DTOs
        private EvaluacionDTO MapToEvaluacionDTO(Evaluacion evaluacion)
        {
            return new EvaluacionDTO
            {
                IdEvaluacion = evaluacion.IdEvaluacion,
                UsuarioId = evaluacion.UsuarioId,
                NombreUsuario = evaluacion.Usuario?.Nombre ?? "Usuario Desconocido",
                ProductoId = evaluacion.ProductoId,
                NombreProducto = evaluacion.Producto?.Nombre ?? "Producto Desconocido",
                FechaCreacion = evaluacion.FechaCreacion,
                Comentario = evaluacion.Comentario,
                Puntuacion = evaluacion.Puntuacion
            };
        }

        private IEnumerable<EvaluacionDTO> MapToEvaluacionesDTO(IEnumerable<Evaluacion> evaluaciones)
        {
            return evaluaciones.Select(e => MapToEvaluacionDTO(e));
        }
    }

}
