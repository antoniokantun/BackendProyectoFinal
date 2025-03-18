using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilService _perfilService;
        private readonly IErrorHandlingService _errorHandlingService;
        private readonly ILogger<PerfilController> _logger;

        public PerfilController(IPerfilService perfilService, IErrorHandlingService errorHandlingService, ILogger<PerfilController> logger)
        {
            _perfilService = perfilService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
        }

        // GET: api/Perfil
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PerfilDTO>>> GetPerfiles()
        {
            _logger.LogInformation("Obteniendo perfiles");
            try
            {
                var perfiles = await _perfilService.GetAllAsync();
                return Ok(perfiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener perfiles");
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetPerfiles));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los perfiles");
            }
        }

        // GET: api/Perfil/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PerfilDTO>> GetPerfil(int id)
        {
            try
            {
                var perfil = await _perfilService.GetByIdAsync(id);
                return Ok(perfil);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetPerfil));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el perfil");
            }
        }

        // GET: api/Perfil/Usuario/5
        [HttpGet("Usuario/{usuarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PerfilDTO>> GetPerfilByUsuarioId(int usuarioId)
        {
            try
            {
                var perfil = await _perfilService.GetByUsuarioIdAsync(usuarioId);
                return Ok(perfil);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetPerfilByUsuarioId));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el perfil por usuario");
            }
        }

        // POST: api/Perfil
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PerfilDTO>> PostPerfil(CreatePerfilDTO perfilDto)
        {
            try
            {
                var perfil = await _perfilService.CreateAsync(perfilDto);
                return CreatedAtAction(nameof(GetPerfil), new { id = perfil.IdPerfil }, perfil);
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
                await _errorHandlingService.LogErrorAsync(ex, nameof(PostPerfil));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el perfil");
            }
        }

        // PUT: api/Perfil/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutPerfil(int id, UpdatePerfilDTO perfilDto)
        {
            try
            {
                await _perfilService.UpdateAsync(id, perfilDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PutPerfil));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el perfil");
            }
        }

        // DELETE: api/Perfil/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePerfil(int id)
        {
            try
            {
                await _perfilService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(DeletePerfil));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el perfil");
            }
        }
    }
}
