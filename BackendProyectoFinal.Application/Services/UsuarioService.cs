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
                FechaRegistro = DateTime.UtcNow,
                RolId = usuarioDto.RolId
            };

            var createdUsuario = await _usuarioRepository.AddAsync(usuario);

            // Obtenemos el usuario completo con su rol para la respuesta
            var usuarioConRol = await _usuarioRepository.GetByIdWithRolAsync(createdUsuario.IdUsuario);
            return MapToRespuestaDTO(usuarioConRol!);
        }

        public async Task UpdateAsync(UsuarioActualizacionDTO usuarioDto)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(usuarioDto.IdUsuario);

            if (existingUsuario == null)
                throw new KeyNotFoundException($"Usuario con ID {usuarioDto.IdUsuario} no encontrado.");

            // Verificar si el correo ya está en uso por otro usuario
            if (existingUsuario.CorreoElectronico != usuarioDto.CorreoElectronico)
            {
                var existingUserWithEmail = await _usuarioRepository.FindByEmailAsync(usuarioDto.CorreoElectronico);
                if (existingUserWithEmail != null && existingUserWithEmail.IdUsuario != usuarioDto.IdUsuario)
                    throw new InvalidOperationException($"Ya existe un usuario con el correo {usuarioDto.CorreoElectronico}");
            }

            // Verificar si el rol existe
            var rol = await _rolRepository.GetByIdAsync(usuarioDto.RolId);
            if (rol == null)
                throw new KeyNotFoundException($"Rol con ID {usuarioDto.RolId} no encontrado.");

            existingUsuario.Nombre = usuarioDto.Nombre;
            existingUsuario.Apellido = usuarioDto.Apellido;
            existingUsuario.CorreoElectronico = usuarioDto.CorreoElectronico;
            existingUsuario.Telefono = usuarioDto.Telefono;
            existingUsuario.RolId = usuarioDto.RolId;

            // Solo actualizar la contraseña si se proporciona una nueva
            if (!string.IsNullOrEmpty(usuarioDto.Contrasenia))
            {
                existingUsuario.Contrasenia = _passwordService.HashPassword(usuarioDto.Contrasenia);
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
    }
}
