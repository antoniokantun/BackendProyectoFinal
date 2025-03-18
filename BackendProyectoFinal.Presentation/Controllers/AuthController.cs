using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IErrorHandlingService _errorHandlingService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, IErrorHandlingService errorHandlingService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _authService.LoginAsync(loginDto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Credenciales inválidas");
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(Login));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al iniciar sesión");
            }
        }
    }
}
