using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IErrorHandlingService _errorHandlingService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(
            IUsuarioService usuarioService,
            IErrorHandlingService errorHandlingService,
            ILogger<UsuarioController> logger)
        {
            _usuarioService = usuarioService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
        }

        // Método privado para obtener el ID del usuario actual (mantenido para la funcionalidad de autoeliminación)
        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim != null ? int.Parse(userIdClaim) : null;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioRespuestaDTO>>> GetUsuarios()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los usuarios");
            }
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioRespuestaDTO>> GetUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetByIdAsync(id);
                return Ok(usuario);
            }
            catch (KeyNotFoundException ex)
            {
                await _errorHandlingService.LogCustomErrorAsync(ex.Message, "Backend");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el usuario");
            }
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<ActionResult<UsuarioRespuestaDTO>> PostUsuario(UsuarioCreacionDTO usuarioDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await _errorHandlingService.LogCustomErrorAsync("Modelo de creación de usuario inválido", "Backend");
                    return BadRequest(ModelState);
                }

                var createdUsuario = await _usuarioService.CreateAsync(usuarioDto);
                return CreatedAtAction(nameof(GetUsuario), new { id = createdUsuario.IdUsuario }, createdUsuario);
            }
            catch (InvalidOperationException ex)
            {
                await _errorHandlingService.LogCustomErrorAsync(ex.Message, "Backend");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el usuario");
            }
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioActualizacionDTO usuarioDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await _errorHandlingService.LogCustomErrorAsync("Modelo de actualización de usuario inválido", "Backend");
                    return BadRequest(ModelState);
                }

                await _usuarioService.UpdateAsync(id, usuarioDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                await _errorHandlingService.LogCustomErrorAsync(ex.Message, "Backend");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                await _errorHandlingService.LogCustomErrorAsync(ex.Message, "Backend");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el usuario");
            }
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                // Verificar que un administrador no se elimine a sí mismo
                var currentUserId = GetCurrentUserId();
                if (id == currentUserId)
                {
                    await _errorHandlingService.LogCustomErrorAsync("Intento de eliminar cuenta propia", "Backend");
                    return BadRequest("No puedes eliminar tu propia cuenta");
                }

                await _usuarioService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                await _errorHandlingService.LogCustomErrorAsync(ex.Message, "Backend");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el usuario");
            }
        }

        // Método para actualizar estado de baneo
        [HttpPatch("ban-usuario")]
        public async Task<IActionResult> UpdateUserBanStatus(UpdateUserBanDTO updateBanDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await _errorHandlingService.LogCustomErrorAsync("Modelo de actualización de baneo inválido", "Backend");
                    return BadRequest(ModelState);
                }

                await _usuarioService.UpdateBanStatusAsync(updateBanDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                await _errorHandlingService.LogCustomErrorAsync(ex.Message, "Backend");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el estado de baneo del usuario");
            }
        }

        [HttpPatch("user-report")]
        public async Task<IActionResult> UpdateUserReport(UpdateUserReportDTO updateReportDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await _errorHandlingService.LogCustomErrorAsync("Modelo de actualización de reporte inválido", "Backend");
                    return BadRequest(ModelState);
                }

                await _usuarioService.UpdateUserReport(updateReportDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                await _errorHandlingService.LogCustomErrorAsync(ex.Message, "Backend");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el estado de reporte del usuario");
            }
        }
    }
}