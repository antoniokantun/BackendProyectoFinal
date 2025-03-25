using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using BackendProyectoFinal.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace BackendProyectoFinal.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolRepository _rolRepository;
        private readonly IPasswordService _passwordService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IRolRepository rolRepository, IPasswordService passwordService)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
            _passwordService = passwordService;
        }

        public async Task<IEnumerable<UsuarioRespuestaDTO>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllWithRolAsync();
            return usuarios.Select(u => MapToRespuestaDTO(u));
        }

        public async Task<UsuarioRespuestaDTO> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdWithRolAsync(id);

            if (usuario == null)
                throw new KeyNotFoundException($"Usuario con ID {id} no encontrado.");

            return MapToRespuestaDTO(usuario);
        }

        public async Task<UsuarioRespuestaDTO> CreateAsync(UsuarioCreacionDTO usuarioDto)
        {
            // Verificar si ya existe un usuario con ese correo
            var existingUser = await _usuarioRepository.FindByEmailAsync(usuarioDto.CorreoElectronico);
            if (existingUser != null)
                throw new InvalidOperationException($"Ya existe un usuario con el correo {usuarioDto.CorreoElectronico}");

            // Verificar si el rol existe
            var rol = await _rolRepository.GetByIdAsync(usuarioDto.RolId);
            if (rol == null)
                throw new KeyNotFoundException($"Rol con ID {usuarioDto.RolId} no encontrado.");

            var hashedPassword = _passwordService.HashPassword(usuarioDto.Contrasenia);

            var usuario = new Usuario
            {
                Nombre = usuarioDto.Nombre,
                Apellido = usuarioDto.Apellido,
                CorreoElectronico = usuarioDto.CorreoElectronico,
                Telefono = usuarioDto.Telefono,
                Contrasenia = hashedPassword,
                Baneado = usuarioDto.Baneado,
                FechaRegistro = DateTime.UtcNow,
                RolId = usuarioDto.RolId
            };

            var createdUsuario = await _usuarioRepository.AddAsync(usuario);

            // Obtenemos el usuario completo con su rol para la respuesta
            var usuarioConRol = await _usuarioRepository.GetByIdWithRolAsync(createdUsuario.IdUsuario);
            return MapToRespuestaDTO(usuarioConRol!);
        }

        // Modificamos el método UpdateAsync para aceptar el ID
        public async Task UpdateAsync(int id, UsuarioActualizacionDTO usuarioDtoPut)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);

            if (existingUsuario == null)
                throw new KeyNotFoundException($"Usuario con ID {id} no encontrado.");

            // Verificar si el correo ya está en uso por otro usuario (y no es el mismo usuario)
            if (existingUsuario.CorreoElectronico != usuarioDtoPut.CorreoElectronico)
            {
                var existingUserWithEmail = await _usuarioRepository.FindByEmailAsync(usuarioDtoPut.CorreoElectronico);
                if (existingUserWithEmail != null && existingUserWithEmail.IdUsuario != id)
                    throw new InvalidOperationException($"Ya existe un usuario con el correo {usuarioDtoPut.CorreoElectronico}");
            }

            // Verificar si el rol existe
            var rol = await _rolRepository.GetByIdAsync(usuarioDtoPut.RolId);
            if (rol == null)
                throw new KeyNotFoundException($"Rol con ID {usuarioDtoPut.RolId} no encontrado.");

            existingUsuario.Nombre = usuarioDtoPut.Nombre;
            existingUsuario.Apellido = usuarioDtoPut.Apellido;
            existingUsuario.CorreoElectronico = usuarioDtoPut.CorreoElectronico;
            existingUsuario.Telefono = usuarioDtoPut.Telefono;
            existingUsuario.Baneado = usuarioDtoPut.Baneado;
            existingUsuario.RolId = usuarioDtoPut.RolId;

            // Solo actualizar la contraseña si se proporciona una nueva
            if (!string.IsNullOrEmpty(usuarioDtoPut.Contrasenia))
            {
                existingUsuario.Contrasenia = _passwordService.HashPassword(usuarioDtoPut.Contrasenia);
            }

            await _usuarioRepository.UpdateAsync(existingUsuario);
        }

        public async Task DeleteAsync(int id)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(id);

            if (existingUsuario == null)
                throw new KeyNotFoundException($"Usuario con ID {id} no encontrado.");

            await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<UsuarioRespuestaDTO> GetByEmailAsync(string email)
        {
            var usuario = await _usuarioRepository.FindByEmailAsync(email);

            if (usuario == null)
                throw new KeyNotFoundException($"Usuario con correo {email} no encontrado.");

            var usuarioConRol = await _usuarioRepository.GetByIdWithRolAsync(usuario.IdUsuario);
            return MapToRespuestaDTO(usuarioConRol!);
        }

        public async Task<bool> VerifyCredentialsAsync(string email, string password)
        {
            var usuario = await _usuarioRepository.FindByEmailAsync(email);

            if (usuario == null)
                return false;

            return _passwordService.VerifyPassword(usuario.Contrasenia, password);
        }

        private static UsuarioRespuestaDTO MapToRespuestaDTO(Usuario usuario)
        {
            return new UsuarioRespuestaDTO
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                CorreoElectronico = usuario.CorreoElectronico,
                Telefono = usuario.Telefono,
                FechaRegistro = usuario.FechaRegistro,
                RolId = usuario.RolId,
                NombreRol = usuario.Rol?.NombreRol ?? string.Empty
            };
        }

        public async Task UpdateBanStatusAsync(UpdateUserBanDTO updateBanDTO)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(updateBanDTO.IdUsuario);

            if (existingUsuario == null)
            {
                throw new KeyNotFoundException($"Usuario con ID {updateBanDTO.IdUsuario} no encontrado.");
            }

            existingUsuario.Baneado = updateBanDTO.Baneado;

            await _usuarioRepository.UpdateAsync(existingUsuario);
        }
    }
}
