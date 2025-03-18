using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginDto)
        {
            // Verificar credenciales
            var isValid = await _usuarioService.VerifyCredentialsAsync(loginDto.CorreoElectronico, loginDto.Contrasenia);
            if (!isValid)
            {
                throw new UnauthorizedAccessException("Credenciales inválidas");
            }

            // Obtener información del usuario
            var usuario = await _usuarioService.GetByEmailAsync(loginDto.CorreoElectronico);

            // Generar token JWT
            var token = GenerateJwtToken(usuario);

            return new LoginResponseDTO
            {
                Token = token,
                Usuario = usuario
            };
        }

        private string GenerateJwtToken(UsuarioRespuestaDTO usuario)
        {
            var jwtSecret = _configuration["Jwt:Secret"];
            if (string.IsNullOrEmpty(jwtSecret))
            {
                throw new InvalidOperationException("JWT Secret no está configurado");
            }

            var key = Encoding.ASCII.GetBytes(jwtSecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Email, usuario.CorreoElectronico),
                    new Claim(ClaimTypes.Role, usuario.NombreRol)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
