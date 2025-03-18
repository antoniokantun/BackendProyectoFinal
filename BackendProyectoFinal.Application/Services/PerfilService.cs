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
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IErrorHandlingService _errorHandlingService;

        public PerfilService(IPerfilRepository perfilRepository, IUsuarioRepository usuarioRepository, IErrorHandlingService errorHandlingService)
        {
            _perfilRepository = perfilRepository;
            _usuarioRepository = usuarioRepository;
            _errorHandlingService = errorHandlingService;
        }

        public async Task<IEnumerable<PerfilDTO>> GetAllAsync()
        {
            try
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
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetAllAsync));
                throw;
            }
        }

        public async Task<PerfilDTO> GetByIdAsync(int id)
        {
            try
            {
                var perfil = await _perfilRepository.GetByIdAsync(id);
                if (perfil == null)
                {
                    await _errorHandlingService.LogErrorAsync($"Perfil con ID {id} no encontrado", nameof(GetByIdAsync), "Advertencia");
                    throw new KeyNotFoundException($"Perfil con ID {id} no encontrado");
                }

                return new PerfilDTO
                {
                    IdPerfil = perfil.IdPerfil,
                    UsuarioId = perfil.UsuarioId,
                    ImagenPerfil = perfil.ImagenPerfil,
                    NombrePerfil = perfil.NombrePerfil,
                    Descripcion = perfil.Descripcion
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

        public async Task<PerfilDTO> GetByUsuarioIdAsync(int usuarioId)
        {
            try
            {
                var perfil = await _perfilRepository.GetByUsuarioIdAsync(usuarioId);
                if (perfil == null)
                {
                    await _errorHandlingService.LogErrorAsync($"Perfil para el Usuario con ID {usuarioId} no encontrado", nameof(GetByUsuarioIdAsync), "Advertencia");
                    throw new KeyNotFoundException($"Perfil para el Usuario con ID {usuarioId} no encontrado");
                }

                return new PerfilDTO
                {
                    IdPerfil = perfil.IdPerfil,
                    UsuarioId = perfil.UsuarioId,
                    ImagenPerfil = perfil.ImagenPerfil,
                    NombrePerfil = perfil.NombrePerfil,
                    Descripcion = perfil.Descripcion
                };
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

        public async Task<PerfilDTO> CreateAsync(CreatePerfilDTO perfilDto)
        {
            try
            {
                // Validar si el usuario existe
                if (!await _usuarioRepository.ExistsAsync(perfilDto.UsuarioId))
                {
                    await _errorHandlingService.LogErrorAsync($"Usuario con ID {perfilDto.UsuarioId} no encontrado", nameof(CreateAsync), "Advertencia");
                    throw new KeyNotFoundException($"Usuario con ID {perfilDto.UsuarioId} no encontrado");
                }

                // Validar si ya existe un perfil para este usuario
                if (await _perfilRepository.ExistsByUsuarioIdAsync(perfilDto.UsuarioId))
                {
                    await _errorHandlingService.LogErrorAsync($"Ya existe un perfil para el Usuario con ID {perfilDto.UsuarioId}", nameof(CreateAsync), "Advertencia");
                    throw new InvalidOperationException($"Ya existe un perfil para el Usuario con ID {perfilDto.UsuarioId}");
                }

                var perfil = new Perfil
                {
                    UsuarioId = perfilDto.UsuarioId,
                    ImagenPerfil = perfilDto.ImagenPerfil,
                    NombrePerfil = perfilDto.NombrePerfil,
                    Descripcion = perfilDto.Descripcion
                };

                var createdPerfil = await _perfilRepository.CreateAsync(perfil);

                return new PerfilDTO
                {
                    IdPerfil = createdPerfil.IdPerfil,
                    UsuarioId = createdPerfil.UsuarioId,
                    ImagenPerfil = createdPerfil.ImagenPerfil,
                    NombrePerfil = createdPerfil.NombrePerfil,
                    Descripcion = createdPerfil.Descripcion
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

        public async Task UpdateAsync(int id, UpdatePerfilDTO perfilDto)
        {
            try
            {
                // Verificar si el perfil existe
                var perfil = await _perfilRepository.GetByIdAsync(id);
                if (perfil == null)
                {
                    await _errorHandlingService.LogErrorAsync($"Perfil con ID {id} no encontrado para actualizar", nameof(UpdateAsync), "Advertencia");
                    throw new KeyNotFoundException($"Perfil con ID {id} no encontrado");
                }

                // Actualizar propiedades del perfil
                perfil.ImagenPerfil = perfilDto.ImagenPerfil;
                perfil.NombrePerfil = perfilDto.NombrePerfil;
                perfil.Descripcion = perfilDto.Descripcion;

                await _perfilRepository.UpdateAsync(perfil);
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
                // Verificar si el perfil existe
                if (!await _perfilRepository.ExistsAsync(id))
                {
                    await _errorHandlingService.LogErrorAsync($"Perfil con ID {id} no encontrado para eliminar", nameof(DeleteAsync), "Advertencia");
                    throw new KeyNotFoundException($"Perfil con ID {id} no encontrado");
                }

                await _perfilRepository.DeleteAsync(id);
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
