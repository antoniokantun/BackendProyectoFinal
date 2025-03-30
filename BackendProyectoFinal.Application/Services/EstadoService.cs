using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;


namespace BackendProyectoFinal.Application.Services
{
    public class EstadoService : IEstadoService
    {
        private readonly IEstadoRepository _estadoRepository;

        public EstadoService(IEstadoRepository estadoRepository)
        {
            _estadoRepository = estadoRepository;
        }

        public async Task<IEnumerable<EstadoDTO>> GetAllAsync()
        {
            var estados = await _estadoRepository.GetAllAsync();
            return estados.Select(MapToDto);
        }

        public async Task<EstadoDTO> GetByIdAsync(int id)
        {
            var estado = await _estadoRepository.GetByIdAsync(id);

            if (estado == null)
                throw new KeyNotFoundException($"Estado con ID {id} no encontrado.");

            return MapToDto(estado);
        }

        public async Task<EstadoDTO> CreateAsync(CreateEstadoDTO createDto)
        {
            var estado = new Estado
            {
                Nombre = createDto.Nombre
            };

            var createdEstado = await _estadoRepository.AddAsync(estado);

            return MapToDto(createdEstado);
        }

        public async Task UpdateAsync(int id, UpdateEstadoDTO updateDto)
        {
            var existingEstado = await _estadoRepository.GetByIdAsync(id);

            if (existingEstado == null)
                throw new KeyNotFoundException($"Estado con ID {id} no encontrado.");

            existingEstado.Nombre = updateDto.Nombre;

            await _estadoRepository.UpdateAsync(existingEstado);
        }

        public async Task DeleteAsync(int id)
        {
            var existingEstado = await _estadoRepository.GetByIdAsync(id);

            if (existingEstado == null)
                throw new KeyNotFoundException($"Estado con ID {id} no encontrado.");

            await _estadoRepository.DeleteAsync(id);
        }

        public async Task<EstadoDTO> GetByNombreAsync(string nombre)
        {
            var estado = await _estadoRepository.GetByNombreAsync(nombre);

            if (estado == null)
                throw new KeyNotFoundException($"Estado con Nombre {nombre} no encontrado.");

            return MapToDto(estado);
        }

        // Método auxiliar para mapear de entidad a DTO
        private EstadoDTO MapToDto(Estado estado)
        {
            return new EstadoDTO
            {
                IdEstado = estado.IdEstado,
                Nombre = estado.Nombre
            };
        }
    }
}
