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
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _perfilRepository;

        public PerfilService(IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

        public async Task<IEnumerable<PerfilDTO>> GetAllAsync()
        {
            var perfiles = await _perfilRepository.GetAllAsync();
            return perfiles.Select(p => new PerfilDTO
            {
                IdPerfil = p.IdPerfil,
                UsuarioId = p.UsuarioId,
                ImagenPerfil = p.ImagenPerfil,
                NombrePerfil = p.NombrePerfil,
                Descripcion = p.Descripcion
            });
        }

        public async Task<PerfilDTO> GetByIdAsync(int id)
        {
            var perfil = await _perfilRepository.GetByIdAsync(id);

            if (perfil == null)
                throw new KeyNotFoundException($"Perfil con ID {id} no encontrado.");

            return new PerfilDTO
            {
                IdPerfil = perfil.IdPerfil,
                UsuarioId = perfil.UsuarioId,
                ImagenPerfil = perfil.ImagenPerfil,
                NombrePerfil = perfil.NombrePerfil,
                Descripcion = perfil.Descripcion
            };
        }

        public async Task<IEnumerable<PerfilDTO>> GetByUsuarioIdAsync(int usuarioId)
        {
            var perfiles = await _perfilRepository.GetPerfilesByUsuarioIdAsync(usuarioId);
            return perfiles.Select(p => new PerfilDTO
            {
                IdPerfil = p.IdPerfil,
                UsuarioId = p.UsuarioId,
                ImagenPerfil = p.ImagenPerfil,
                NombrePerfil = p.NombrePerfil,
                Descripcion = p.Descripcion
            });
        }

        public async Task<PerfilDTO> CreateAsync(CreatePerfilDTO perfilDto)
        {
            var perfil = new Perfil
            {
                UsuarioId = perfilDto.UsuarioId,
                ImagenPerfil = perfilDto.ImagenPerfil,
                NombrePerfil = perfilDto.NombrePerfil,
                Descripcion = perfilDto.Descripcion
            };

            var createdPerfil = await _perfilRepository.AddAsync(perfil);

            return new PerfilDTO
            {
                IdPerfil = createdPerfil.IdPerfil,
                UsuarioId = createdPerfil.UsuarioId,
                ImagenPerfil = createdPerfil.ImagenPerfil,
                NombrePerfil = createdPerfil.NombrePerfil,
                Descripcion = createdPerfil.Descripcion
            };
        }

        public async Task UpdateAsync(int id, UpdatePerfilDTO perfilDto)
        {
            var existingPerfil = await _perfilRepository.GetByIdAsync(id);

            if (existingPerfil == null)
                throw new KeyNotFoundException($"Perfil con ID {id} no encontrado.");

            existingPerfil.ImagenPerfil = perfilDto.ImagenPerfil;
            existingPerfil.NombrePerfil = perfilDto.NombrePerfil;
            existingPerfil.Descripcion = perfilDto.Descripcion;

            await _perfilRepository.UpdateAsync(existingPerfil);
        }

        public async Task DeleteAsync(int id)
        {
            var existingPerfil = await _perfilRepository.GetByIdAsync(id);

            if (existingPerfil == null)
                throw new KeyNotFoundException($"Perfil con ID {id} no encontrado.");

            await _perfilRepository.DeleteAsync(id);
        }
    }
}
