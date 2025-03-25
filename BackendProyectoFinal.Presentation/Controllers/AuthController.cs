using BackendProyectoFinal.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Application.Services;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Application.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IGenericRepository<Usuario> _usuarioService;
        private readonly IGenericRepository<RefreshToken> _refreshTokenService;
        private readonly JwtService _jwtService;
        private readonly IPasswordService _passwordService;

        public AuthController(
            IGenericRepository<Usuario> usuarioService,
            IGenericRepository<RefreshToken> refreshTokenService,
            JwtService jwtService,
            IPasswordService passwordService)
        {
            _usuarioService = usuarioService;
            _refreshTokenService = refreshTokenService;
            _jwtService = jwtService;
            _passwordService = passwordService;
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            // Buscar el usuario por email
            var usuarios = await _usuarioService.GetAllAsync();
            var usuario = usuarios.FirstOrDefault(u => u.CorreoElectronico == loginDTO.CorreoElectronico);

            if (usuario == null)
            {
                return Unauthorized("El usuario ingresado no existe");
            }

            // Verificar la contraseña
            var contraseniaVerificada = _passwordService.VerifyPassword(usuario.Contrasenia, loginDTO.Contrasenia);

            if (!contraseniaVerificada)
            {
                return Unauthorized("La contraseña ingresada fue incorrecta.");
            }

            // Generar el access token (ahora incluyendo el ID)
            var accessToken = _jwtService.GenerateToken(usuario.CorreoElectronico, usuario.RolId.ToString(), usuario.IdUsuario);

            // Generar el refresh token
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Guardar el refresh token en la base de datos
            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                FechaExpiracion = DateTime.UtcNow.AddDays(7), // Expira en 7 días
                FechaCreacion = DateTime.UtcNow,
                EstaActivo = true,
                UsuarioId = usuario.IdUsuario
            };

            await _refreshTokenService.AddAsync(refreshTokenEntity);

            // Devolver ambos tokens
            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        // POST: api/Auth/RefreshToken
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            // Validar el refresh token
            var refreshTokenEntity = await _refreshTokenService.GetByTokenAsync(request.RefreshToken);

            if (refreshTokenEntity == null || refreshTokenEntity.FechaExpiracion < DateTime.UtcNow || !refreshTokenEntity.EstaActivo)
            {
                return Unauthorized("Refresh token inválido o expirado.");
            }

            // Obtener el usuario asociado al refresh token
            var usuario = await _usuarioService.GetByIdAsync(refreshTokenEntity.UsuarioId);

            if (usuario == null)
            {
                return Unauthorized("Usuario no encontrado.");
            }

            // Generar un nuevo access token (ahora incluyendo el ID)
            var newAccessToken = _jwtService.GenerateToken(usuario.CorreoElectronico, usuario.RolId.ToString(), usuario.IdUsuario);

            // Generar un nuevo refresh token
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            // Desactivar el refresh token anterior
            refreshTokenEntity.EstaActivo = false;
            await _refreshTokenService.UpdateAsync(refreshTokenEntity);

            // Guardar el nuevo refresh token en la base de datos
            var newRefreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                FechaExpiracion = DateTime.UtcNow.AddDays(7), // Expira en 7 días
                FechaCreacion = DateTime.UtcNow,
                EstaActivo = true,
                UsuarioId = usuario.IdUsuario
            };

            await _refreshTokenService.AddAsync(newRefreshTokenEntity);

            // Devolver los nuevos tokens
            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}