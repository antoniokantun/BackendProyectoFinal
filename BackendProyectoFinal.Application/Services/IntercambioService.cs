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

        public IntercambioService(IIntercambioRepository intercambioRepository)
        {
            _intercambioRepository = intercambioRepository;
        }

        public async Task<IEnumerable<IntercambioDTO>> GetAllAsync()
        {
            var intercambios = await _intercambioRepository.GetAllAsync();
            return intercambios.Select(MapToDto);
        }

        public async Task<IntercambioDTO> GetByIdAsync(int id)
        {
            var intercambio = await _intercambioRepository.GetByIdAsync(id);

            if (intercambio == null)
                throw new KeyNotFoundException($"Intercambio con ID {id} no encontrado.");

            return MapToDto(intercambio);
        }

        public async Task<IntercambioDTO> CreateAsync(CreateIntercambioDTO createDto)
        {
            var intercambio = new Intercambio
            {
                UsuarioSolicitanteId = createDto.UsuarioSolicitanteId,
                UsuarioOfertanteId = createDto.UsuarioOfertanteId,
                ProductoId = createDto.ProductoId,
                FechaRegistro = DateTime.Now
            };

            var createdIntercambio = await _intercambioRepository.AddAsync(intercambio);

            return MapToDto(createdIntercambio);
        }

        public async Task UpdateAsync(int id, UpdateIntercambioDTO updateDto)
        {
            var existingIntercambio = await _intercambioRepository.GetByIdAsync(id);

            if (existingIntercambio == null)
                throw new KeyNotFoundException($"Intercambio con ID {id} no encontrado.");

            existingIntercambio.UsuarioSolicitanteId = updateDto.UsuarioSolicitanteId;
            existingIntercambio.UsuarioOfertanteId = updateDto.UsuarioOfertanteId;
            existingIntercambio.ProductoId = updateDto.ProductoId;

            await _intercambioRepository.UpdateAsync(existingIntercambio);
        }

        public async Task DeleteAsync(int id)
        {
            var existingIntercambio = await _intercambioRepository.GetByIdAsync(id);

            if (existingIntercambio == null)
                throw new KeyNotFoundException($"Intercambio con ID {id} no encontrado.");

            await _intercambioRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<IntercambioDTO>> GetByUsuarioSolicitanteIdAsync(int usuarioSolicitanteId)
        {
            var intercambios = await _intercambioRepository.GetByUsuarioSolicitanteIdAsync(usuarioSolicitanteId);
            return intercambios.Select(MapToDto);
        }

        public async Task<IEnumerable<IntercambioDTO>> GetByUsuarioOfertanteIdAsync(int usuarioOfertanteId)
        {
            var intercambios = await _intercambioRepository.GetByUsuarioOfertanteIdAsync(usuarioOfertanteId);
            return intercambios.Select(MapToDto);
        }

        public async Task<IEnumerable<IntercambioDTO>> GetByProductoIdAsync(int productoId)
        {
            var intercambios = await _intercambioRepository.GetByProductoIdAsync(productoId);
            return intercambios.Select(MapToDto);
        }

        // Método auxiliar para mapear de entidad a DTO
        private IntercambioDTO MapToDto(Intercambio intercambio)
        {
            return new IntercambioDTO
            {
                IdIntercambio = intercambio.IdIntercambio,
                UsuarioSolicitanteId = intercambio.UsuarioSolicitanteId,
                UsuarioOfertanteId = intercambio.UsuarioOfertanteId,
                ProductoId = intercambio.ProductoId,
                FechaRegistro = intercambio.FechaRegistro
            };
        }
    }

}
