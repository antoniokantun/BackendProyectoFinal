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
    public class IntercambioService : IIntercambioService
    {
        private readonly IIntercambioRepository _intercambioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IErrorHandlingService _errorHandlingService;

        public IntercambioService(
            IIntercambioRepository intercambioRepository,
            IUsuarioRepository usuarioRepository,
            IProductoRepository productoRepository,
            IErrorHandlingService errorHandlingService)
        {
            _intercambioRepository = intercambioRepository;
            _usuarioRepository = usuarioRepository;
            _productoRepository = productoRepository;
            _errorHandlingService = errorHandlingService;
        }

        public async Task<IEnumerable<IntercambioDTO>> GetAllAsync()
        {
            try
            {
                var intercambios = await _intercambioRepository.GetAllAsync();
                return intercambios.Select(i => new IntercambioDTO
                {
                    IdIntercambio = i.IdIntercambio,
                    UsuarioSolicitanteId = i.UsuarioSolicitanteId,
                    UsuarioOfertanteId = i.UsuarioOfertanteId,
                    ProductoId = i.ProductoId,
                    FechaRegistro = i.FechaRegistro
                });
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetAllAsync));
                throw;
            }
        }

        public async Task<IntercambioDTO> GetByIdAsync(int id)
        {
            try
            {
                var intercambio = await _intercambioRepository.GetByIdAsync(id);
                if (intercambio == null)
                {
                    await _errorHandlingService.LogErrorAsync($"Intercambio con ID {id} no encontrado", nameof(GetByIdAsync), "Advertencia");
                    throw new KeyNotFoundException($"Intercambio con ID {id} no encontrado");
                }

                return new IntercambioDTO
                {
                    IdIntercambio = intercambio.IdIntercambio,
                    UsuarioSolicitanteId = intercambio.UsuarioSolicitanteId,
                    UsuarioOfertanteId = intercambio.UsuarioOfertanteId,
                    ProductoId = intercambio.ProductoId,
                    FechaRegistro = intercambio.FechaRegistro
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

        public async Task<IntercambioDTO> CreateAsync(CreateIntercambioDTO intercambioDto)
        {
            try
            {
                // Validar si existen los usuarios y el producto
                if (!await _usuarioRepository.ExistsAsync(intercambioDto.UsuarioSolicitanteId))
                {
                    await _errorHandlingService.LogErrorAsync($"Usuario solicitante con ID {intercambioDto.UsuarioSolicitanteId} no existe", nameof(CreateAsync), "Advertencia");
                    throw new KeyNotFoundException($"Usuario solicitante con ID {intercambioDto.UsuarioSolicitanteId} no existe");
                }

                if (!await _usuarioRepository.ExistsAsync(intercambioDto.UsuarioOfertanteId))
                {
                    await _errorHandlingService.LogErrorAsync($"Usuario ofertante con ID {intercambioDto.UsuarioOfertanteId} no existe", nameof(CreateAsync), "Advertencia");
                    throw new KeyNotFoundException($"Usuario ofertante con ID {intercambioDto.UsuarioOfertanteId} no existe");
                }

                if (!await _productoRepository.ExistsAsync(intercambioDto.ProductoId))
                {
                    await _errorHandlingService.LogErrorAsync($"Producto con ID {intercambioDto.ProductoId} no existe", nameof(CreateAsync), "Advertencia");
                    throw new KeyNotFoundException($"Producto con ID {intercambioDto.ProductoId} no existe");
                }

                // Validar que los usuarios sean diferentes
                if (intercambioDto.UsuarioSolicitanteId == intercambioDto.UsuarioOfertanteId)
                {
                    await _errorHandlingService.LogErrorAsync("El usuario solicitante y ofertante no pueden ser el mismo", nameof(CreateAsync), "Advertencia");
                    throw new InvalidOperationException("El usuario solicitante y ofertante no pueden ser el mismo");
                }

                var intercambio = new Intercambio
                {
                    UsuarioSolicitanteId = intercambioDto.UsuarioSolicitanteId,
                    UsuarioOfertanteId = intercambioDto.UsuarioOfertanteId,
                    ProductoId = intercambioDto.ProductoId,
                    FechaRegistro = DateTime.Now
                };

                var createdIntercambio = await _intercambioRepository.CreateAsync(intercambio);

                return new IntercambioDTO
                {
                    IdIntercambio = createdIntercambio.IdIntercambio,
                    UsuarioSolicitanteId = createdIntercambio.UsuarioSolicitanteId,
                    UsuarioOfertanteId = createdIntercambio.UsuarioOfertanteId,
                    ProductoId = createdIntercambio.ProductoId,
                    FechaRegistro = createdIntercambio.FechaRegistro
                };
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

        public async Task UpdateAsync(int id, UpdateIntercambioDTO intercambioDto)
        {
            try
            {
                // Verificar si el intercambio existe
                var intercambio = await _intercambioRepository.GetByIdAsync(id);
                if (intercambio == null)
                {
                    await _errorHandlingService.LogErrorAsync($"Intercambio con ID {id} no encontrado para actualizar", nameof(UpdateAsync), "Advertencia");
                    throw new KeyNotFoundException($"Intercambio con ID {id} no encontrado");
                }

                // Validar si existen los usuarios y el producto
                if (!await _usuarioRepository.ExistsAsync(intercambioDto.UsuarioSolicitanteId))
                {
                    await _errorHandlingService.LogErrorAsync($"Usuario solicitante con ID {intercambioDto.UsuarioSolicitanteId} no existe", nameof(UpdateAsync), "Advertencia");
                    throw new KeyNotFoundException($"Usuario solicitante con ID {intercambioDto.UsuarioSolicitanteId} no existe");
                }

                if (!await _usuarioRepository.ExistsAsync(intercambioDto.UsuarioOfertanteId))
                {
                    await _errorHandlingService.LogErrorAsync($"Usuario ofertante con ID {intercambioDto.UsuarioOfertanteId} no existe", nameof(UpdateAsync), "Advertencia");
                    throw new KeyNotFoundException($"Usuario ofertante con ID {intercambioDto.UsuarioOfertanteId} no existe");
                }

                if (!await _productoRepository.ExistsAsync(intercambioDto.ProductoId))
                {
                    await _errorHandlingService.LogErrorAsync($"Producto con ID {intercambioDto.ProductoId} no existe", nameof(UpdateAsync), "Advertencia");
                    throw new KeyNotFoundException($"Producto con ID {intercambioDto.ProductoId} no existe");
                }

                // Validar que los usuarios sean diferentes
                if (intercambioDto.UsuarioSolicitanteId == intercambioDto.UsuarioOfertanteId)
                {
                    await _errorHandlingService.LogErrorAsync("El usuario solicitante y ofertante no pueden ser el mismo", nameof(UpdateAsync), "Advertencia");
                    throw new InvalidOperationException("El usuario solicitante y ofertante no pueden ser el mismo");
                }

                intercambio.UsuarioSolicitanteId = intercambioDto.UsuarioSolicitanteId;
                intercambio.UsuarioOfertanteId = intercambioDto.UsuarioOfertanteId;
                intercambio.ProductoId = intercambioDto.ProductoId;
                // No actualizamos la fecha de registro para mantener la fecha original

                await _intercambioRepository.UpdateAsync(intercambio);
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
                await _errorHandlingService.LogErrorAsync(ex, nameof(UpdateAsync));
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                // Verificar si el intercambio existe
                if (!await _intercambioRepository.ExistsAsync(id))
                {
                    await _errorHandlingService.LogErrorAsync($"Intercambio con ID {id} no encontrado para eliminar", nameof(DeleteAsync), "Advertencia");
                    throw new KeyNotFoundException($"Intercambio con ID {id} no encontrado");
                }

                await _intercambioRepository.DeleteAsync(id);
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
