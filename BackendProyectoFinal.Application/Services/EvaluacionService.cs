using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;

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
                TituloEvaluacion = createEvaluacionDto.TituloEvaluacion,
                UsuarioId = createEvaluacionDto.UsuarioId,
                UsuarioEvaluadorId = createEvaluacionDto.UsuarioEvaluadorId,
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

            // Actualizamos solo los campos permitidos
            existingEvaluacion.TituloEvaluacion = updateEvaluacionDto.TituloEvaluacion;
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

        public async Task<EvaluacionDTO> PatchAsync(int id, PatchEvaluacionDTO patchEvaluacionDto)
        {
            var existingEvaluacion = await _evaluacionRepository.GetByIdAsync(id);

            if (existingEvaluacion == null)
                throw new KeyNotFoundException($"Evaluación con ID {id} no encontrada.");

            // Actualizar solo los campos proporcionados
            if (patchEvaluacionDto.TituloEvaluacion != null)
                existingEvaluacion.TituloEvaluacion = patchEvaluacionDto.TituloEvaluacion;

            if (patchEvaluacionDto.Comentario != null)
                existingEvaluacion.Comentario = patchEvaluacionDto.Comentario;

            if (patchEvaluacionDto.Puntuacion.HasValue)
                existingEvaluacion.Puntuacion = patchEvaluacionDto.Puntuacion.Value;

            // Siempre marcar como completado cuando se usa PATCH
            existingEvaluacion.Completado = true;

            await _evaluacionRepository.UpdateAsync(existingEvaluacion);

            // Recargar la entidad para obtener datos actualizados con relaciones
            var evaluacionActualizada = await _evaluacionRepository.GetByIdAsync(id);
            return MapToDto(evaluacionActualizada!);
        }

        // Método auxiliar para mapear la entidad a DTO
        private EvaluacionDTO MapToDto(Evaluacion evaluacion)
        {
            return new EvaluacionDTO
            {
                IdEvaluacion = evaluacion.IdEvaluacion,
                TituloEvaluacion = evaluacion.TituloEvaluacion,
                UsuarioId = evaluacion.UsuarioId,
                UsuarioEvaluadorId = evaluacion.UsuarioEvaluadorId,
                ProductoId = evaluacion.ProductoId,
                FechaCreacion = evaluacion.FechaCreacion,
                Comentario = evaluacion.Comentario,
                Puntuacion = evaluacion.Puntuacion,
                Completado = evaluacion.Completado,
                NombreUsuario = evaluacion.Usuario?.Nombre,
                NombreUsuarioEvaluador = evaluacion.UsuarioEvaluador?.Nombre,
                NombreProducto = evaluacion.Producto?.Nombre
            };
        }
    }
}