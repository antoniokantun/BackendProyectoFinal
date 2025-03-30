using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;

namespace BackendProyectoFinal.Application.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<IEnumerable<RolDTO>> GetAllAsync()
        {
            var roles = await _rolRepository.GetAllAsync();
            return roles.Select(r => new RolDTO
            {
                IdRol = r.IdRol,
                NombreRol = r.NombreRol
            });
        }

        public async Task<RolDTO> GetByIdAsync(int id)
        {
            var rol = await _rolRepository.GetByIdAsync(id);

            if (rol == null)
                throw new KeyNotFoundException($"Rol con ID {id} no encontrado.");

            return new RolDTO
            {
                IdRol = rol.IdRol,
                NombreRol = rol.NombreRol
            };
        }

        public async Task<RolDTO> CreateAsync(RolDTO rolDto)
        {
            var rol = new Rol
            {
                NombreRol = rolDto.NombreRol
            };

            var createdRol = await _rolRepository.AddAsync(rol);

            return new RolDTO
            {
                IdRol = createdRol.IdRol,
                NombreRol = createdRol.NombreRol
            };
        }

        public async Task UpdateAsync(RolDTO rolDto)
        {
            var existingRol = await _rolRepository.GetByIdAsync(rolDto.IdRol);

            if (existingRol == null)
                throw new KeyNotFoundException($"Rol con ID {rolDto.IdRol} no encontrado.");

            existingRol.NombreRol = rolDto.NombreRol;

            await _rolRepository.UpdateAsync(existingRol);
        }

        public async Task DeleteAsync(int id)
        {
            var existingRol = await _rolRepository.GetByIdAsync(id);

            if (existingRol == null)
                throw new KeyNotFoundException($"Rol con ID {id} no encontrado.");

            await _rolRepository.DeleteAsync(id);
        }
    }
}
