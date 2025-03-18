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
    public class ComentarioService : IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IErrorHandlingService _errorHandlingService;

        public ComentarioService(IComentarioRepository comentarioRepository, IErrorHandlingService errorHandlingService)
        {
            _comentarioRepository = comentarioRepository;
            _errorHandlingService = errorHandlingService;
        }

        public async Task<IEnumerable<ComentarioDTO>> GetAllAsync()
        {
            try
            {
                var comentarios = await _comentarioRepository.GetAllAsync();
                return MapToComentariosDTOs(comentarios);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetAllAsync));
                throw;
            }
        }

        public async Task<ComentarioDTO> GetByIdAsync(int id)
        {
            try
            {
                var comentario = await _comentarioRepository.GetByIdAsync(id);
                if (comentario == null)
                {
                    await _errorHandlingService.LogErrorAsync($"Comentario con ID {id} no encontrado", nameof(GetByIdAsync), "Advertencia");
                    throw new KeyNotFoundException($"Comentario con ID {id} no encontrado");
                }

                return MapToComentarioDTO(comentario);
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

        public async Task<IEnumerable<ComentarioDTO>> GetByProductIdAsync(int productoId)
        {
            try
            {
                var comentarios = await _comentarioRepository.GetRootCommentsByProductIdAsync(productoId);
                return MapToComentariosDTOs(comentarios);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetByProductIdAsync));
                throw;
            }
        }

        public async Task<IEnumerable<ComentarioDTO>> GetByUsuarioIdAsync(int usuarioId)
        {
            try
            {
                var comentarios = await _comentarioRepository.GetByUsuarioIdAsync(usuarioId);
                return MapToComentariosDTOs(comentarios);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetByUsuarioIdAsync));
                throw;
            }
        }

        public async Task<ComentarioDTO> CreateAsync(CreateComentarioDTO comentarioDto)
        {
            try
            {
                var comentario = new Comentario
                {
                    Contenido = comentarioDto.Contenido,
                    UsuarioId = comentarioDto.UsuarioId,
                    ProductoId = comentarioDto.ProductoId,
                    ComentarioPadreId = comentarioDto.ComentarioPadreId,
                    FechaCreacion = DateTime.Now
                };

                // Verificar si el comentario padre existe si se proporciona un ID
                if (comentarioDto.ComentarioPadreId.HasValue)
                {
                    var comentarioPadre = await _comentarioRepository.GetByIdAsync(comentarioDto.ComentarioPadreId.Value);
                    if (comentarioPadre == null)
                    {
                        await _errorHandlingService.LogErrorAsync($"Comentario padre con ID {comentarioDto.ComentarioPadreId} no encontrado", nameof(CreateAsync), "Advertencia");
                        throw new KeyNotFoundException($"Comentario padre con ID {comentarioDto.ComentarioPadreId} no encontrado");
                    }

                    // Verificar que ambos comentarios pertenecen al mismo producto
                    if (comentarioPadre.ProductoId != comentarioDto.ProductoId)
                    {
                        await _errorHandlingService.LogErrorAsync($"El comentario padre pertenece a un producto diferente", nameof(CreateAsync), "Advertencia");
                        throw new InvalidOperationException("El comentario padre debe pertenecer al mismo producto");
                    }
                }

                var createdComentario = await _comentarioRepository.CreateAsync(comentario);
                return await GetByIdAsync(createdComentario.IdComentario);
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

        public async Task UpdateAsync(int id, UpdateComentarioDTO comentarioDto)
        {
            try
            {
                var comentario = await _comentarioRepository.GetByIdAsync(id);
                if (comentario == null)
                {
                    await _errorHandlingService.LogErrorAsync($"Comentario con ID {id} no encontrado para actualizar", nameof(UpdateAsync), "Advertencia");
                    throw new KeyNotFoundException($"Comentario con ID {id} no encontrado");
                }

                comentario.Contenido = comentarioDto.Contenido;

                await _comentarioRepository.UpdateAsync(comentario);
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
                if (!await _comentarioRepository.ExistsAsync(id))
                {
                    await _errorHandlingService.LogErrorAsync($"Comentario con ID {id} no encontrado para eliminar", nameof(DeleteAsync), "Advertencia");
                    throw new KeyNotFoundException($"Comentario con ID {id} no encontrado");
                }

                await _comentarioRepository.DeleteAsync(id);
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

        private IEnumerable<ComentarioDTO> MapToComentariosDTOs(IEnumerable<Comentario> comentarios)
        {
            var resultado = new List<ComentarioDTO>();

            foreach (var comentario in comentarios)
            {
                resultado.Add(MapToComentarioDTO(comentario));
            }

            return resultado;
        }

        private ComentarioDTO MapToComentarioDTO(Comentario comentario)
        {
            return new ComentarioDTO
            {
                IdComentario = comentario.IdComentario,
                Contenido = comentario.Contenido,
                UsuarioId = comentario.UsuarioId,
                NombreUsuario = comentario.Usuario?.Nombre ?? "Usuario Desconocido",
                ProductoId = comentario.ProductoId,
                FechaCreacion = comentario.FechaCreacion,
                ComentarioPadreId = comentario.ComentarioPadreId,
                ComentariosHijos = comentario.ComentariosHijos
                    .Select(ch => new ComentarioDTO
                    {
                        IdComentario = ch.IdComentario,
                        Contenido = ch.Contenido,
                        UsuarioId = ch.UsuarioId,
                        NombreUsuario = ch.Usuario?.Nombre ?? "Usuario Desconocido",
                        ProductoId = ch.ProductoId,
                        FechaCreacion = ch.FechaCreacion,
                        ComentarioPadreId = ch.ComentarioPadreId
                    })
                    .ToList()
            };
        }
    }
}
