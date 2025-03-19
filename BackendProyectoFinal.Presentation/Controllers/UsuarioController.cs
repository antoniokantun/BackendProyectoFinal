using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IErrorHandlingService _errorHandlingService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioService usuarioService, IErrorHandlingService errorHandlingService, ILogger<UsuarioController> logger)
        {
            _usuarioService = usuarioService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
        }

        // GET: api/Usuario
        [HttpGet]
        /*[Authorize(Roles = "Administrador")] */// Solo administradores pueden ver todos los usuarios
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UsuarioRespuestaDTO>>> GetUsuarios()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetUsuarios));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los usuarios");
            }
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioRespuestaDTO>> GetUsuario(int id)
        {
            try
            {
                // Verificar si el usuario está intentando acceder a su propio perfil
                // var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                // Solo administradores o el propio usuario pueden acceder al perfil
                // if (userRole != "Administrador" && id != currentUserId)
                // {
                //     return Forbid();
                // }

                var usuario = await _usuarioService.GetByIdAsync(id);
                return Ok(usuario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetUsuario));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el usuario");
            }
        }

        // POST: api/Usuario
        [HttpPost]
        /*[Authorize(Roles = "Administrador")]*/ // Solo administradores pueden crear usuarios
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioRespuestaDTO>> PostUsuario(UsuarioCreacionDTO usuarioDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdUsuario = await _usuarioService.CreateAsync(usuarioDto);
                return CreatedAtAction(nameof(GetUsuario), new { id = createdUsuario.IdUsuario }, createdUsuario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PostUsuario));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el usuario");
            }
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutUsuario(int id, UsuarioActualizacionDTO usuarioDto)
        {
            try
            {
                if (id != usuarioDto.IdUsuario)
                    return BadRequest("El ID no coincide con el ID del usuario proporcionado");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // // Verificar si el usuario está intentando modificar su propio perfil
                // var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                // var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                // // Solo administradores o el propio usuario pueden modificar el perfil
                // if (userRole != "Administrador" && id != currentUserId)
                // {
                //     return Forbid();
                // }

                // // Si no es administrador, no puede cambiar su propio rol
                // if (userRole != "Administrador" && id == currentUserId)
                // {
                //     var currentUser = await _usuarioService.GetByIdAsync(currentUserId);
                //     if (currentUser.RolId != usuarioDto.RolId)
                //     {
                //         return BadRequest("No tienes permiso para cambiar tu propio rol");
                //     }
                // }

                await _usuarioService.UpdateAsync(usuarioDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PutUsuario));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el usuario");
            }
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        /*[Authorize(Roles = "Administrador")]*/ // Solo administradores pueden eliminar usuarios
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                // Verificar que un administrador no se elimine a sí mismo
                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (id == currentUserId)
                {
                    return BadRequest("No puedes eliminar tu propia cuenta");
                }

                await _usuarioService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(DeleteUsuario));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el usuario");
            }
        }
    }
}
