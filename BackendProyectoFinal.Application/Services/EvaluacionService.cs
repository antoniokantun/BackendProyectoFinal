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

        public EvaluacionService(IEvaluacionRepository evaluacionRepository)
        {
            _evaluacionRepository = evaluacionRepository;
        }

        public async Task<IEnumerable<EvaluacionDTO>> GetAllAsync()
        {
            var evaluaciones = await _evaluacionRepository.GetAllAsync();
            return evaluaciones.Select(MapToDto);
        }

        public async Task<EvaluacionDTO> GetByIdAsync(int id)
        {
            var evaluacion = await _evaluacionRepository.GetByIdAsync(id);

            if (evaluacion == null)
                throw new KeyNotFoundException($"Evaluación con ID {id} no encontrada.");

            return MapToDto(evaluacion);
        }

        public async Task<IEnumerable<EvaluacionDTO>> GetByUsuarioIdAsync(int usuarioId)
        {
            var evaluaciones = await _evaluacionRepository.GetByUsuarioIdAsync(usuarioId);
            return evaluaciones.Select(MapToDto);
        }

        public async Task<IEnumerable<EvaluacionDTO>> GetByProductoIdAsync(int productoId)
        {
            var evaluaciones = await _evaluacionRepository.GetByProductoIdAsync(productoId);
            return evaluaciones.Select(MapToDto);
        }

        public async Task<EvaluacionDTO> CreateAsync(CreateEvaluacionDTO createEvaluacionDto)
        {
            var evaluacion = new Evaluacion
            {
                UsuarioId = createEvaluacionDto.UsuarioId,
                ProductoId = createEvaluacionDto.ProductoId,
                FechaCreacion = DateTime.Now, // Utilizar la fecha actual para nuevas evaluaciones
                Comentario = createEvaluacionDto.Comentario,
                Puntuacion = createEvaluacionDto.Puntuacion
            };

            var createdEvaluacion = await _evaluacionRepository.AddAsync(evaluacion);

            // Recargar la entidad con sus relaciones para obtener los datos completos
            var evaluacionCompleta = await _evaluacionRepository.GetByIdAsync(createdEvaluacion.IdEvaluacion);

            return MapToDto(evaluacionCompleta!);
        }

        public async Task UpdateAsync(int id, UpdateEvaluacionDTO updateEvaluacionDto)
        {
            var existingEvaluacion = await _evaluacionRepository.GetByIdAsync(id);

            if (existingEvaluacion == null)
                throw new KeyNotFoundException($"Evaluación con ID {id} no encontrada.");

            // No actualizamos UsuarioId ni ProductoId para mantener la integridad
            // Tampoco actualizamos FechaCreacion
            existingEvaluacion.Comentario = updateEvaluacionDto.Comentario;
            existingEvaluacion.Puntuacion = updateEvaluacionDto.Puntuacion;

            await _evaluacionRepository.UpdateAsync(existingEvaluacion);
        }

        public async Task DeleteAsync(int id)
        {
            var existingEvaluacion = await _evaluacionRepository.GetByIdAsync(id);

            if (existingEvaluacion == null)
                throw new KeyNotFoundException($"Evaluación con ID {id} no encontrada.");

            await _evaluacionRepository.DeleteAsync(id);
        }

        // Método auxiliar para mapear la entidad a DTO
        private EvaluacionDTO MapToDto(Evaluacion evaluacion)
        {
            return new EvaluacionDTO
            {
                IdEvaluacion = evaluacion.IdEvaluacion,
                UsuarioId = evaluacion.UsuarioId,
                ProductoId = evaluacion.ProductoId,
                FechaCreacion = evaluacion.FechaCreacion,
                Comentario = evaluacion.Comentario,
                Puntuacion = evaluacion.Puntuacion,
                NombreUsuario = evaluacion.Usuario?.Nombre, // Suponiendo que Usuario tiene una propiedad NombreUsuario
                NombreProducto = evaluacion.Producto?.Nombre // Suponiendo que Producto tiene una propiedad Nombre
            };
        }
    }
}
