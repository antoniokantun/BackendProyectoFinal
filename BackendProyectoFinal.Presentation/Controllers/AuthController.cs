using BackendProyectoFinal.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Application.Services;
using BackendProyectoFinal.Domain.Interfaces;
using System.Diagnostics;
using BackendProyectoFinal.Application.Interfaces;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IAuthService _authService;
        //private readonly IErrorHandlingService _errorHandlingService;
        //private readonly ILogger<AuthController> _logger;

        private readonly IGenericRepository<Usuario> _usuarioService;
        private readonly JwtService _jwtService;
        private readonly IPasswordService _passwordService;

        public AuthController(IGenericRepository<Usuario> usuarioService, JwtService jwtService, IPasswordService passwordService)
        {
            //_authService = authService;
            //_errorHandlingService = errorHandlingService;
            //_logger = logger;
            _jwtService = jwtService;
            _usuarioService = usuarioService;
            _passwordService = passwordService;
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        //[AllowAnonymous]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            // Buscar el usuario por email
            var usuarios = await _usuarioService.GetAllAsync();
            var usuario = usuarios.FirstOrDefault(u => u.CorreoElectronico == loginDTO.CorreoElectronico);

            if (usuario == null)
            {
                return Unauthorized("El usuario ingresado no existe");
            }

            var contraseniaVerificada = _passwordService.VerifyPassword(usuario.Contrasenia, loginDTO.Contrasenia);

            // Verificar la contraseña (comparación directa en texto plano)
            if (contraseniaVerificada == false)
            {
                return Unauthorized("La contraseña ingresada fue incorrecta.");
            }

            // Generar el token JWT
            var token = _jwtService.GenerateToken(usuario.CorreoElectronico, usuario.RolId.ToString());

            // Devolver el token
            return Ok(new { Token = token });
        }
    }
}
