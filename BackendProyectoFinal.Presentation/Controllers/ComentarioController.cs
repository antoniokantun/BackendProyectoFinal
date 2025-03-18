using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentarioService _comentarioService;
        private readonly IErrorHandlingService _errorHandlingService;
        private readonly ILogger<ComentarioController> _logger;

        public ComentarioController(IComentarioService comentarioService, IErrorHandlingService errorHandlingService, ILogger<ComentarioController> logger)
        {
            _comentarioService = comentarioService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
        }

        // GET: api/Comentario
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ComentarioDTO>>> GetComentarios()
        {
            _logger.LogInformation("Obteniendo comentarios");
            try
            {
                var comentarios = await _comentarioService.GetAllAsync();
                return Ok(comentarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener comentarios");
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetComentarios));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los comentarios");
            }
        }

        // GET: api/Comentario/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ComentarioDTO>> GetComentario(int id)
        {
            try
            {
                var comentario = await _comentarioService.GetByIdAsync(id);
                return Ok(comentario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetComentario));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el comentario");
            }
        }

        // GET: api/Comentario/Producto/5
        [HttpGet("Producto/{productoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ComentarioDTO>>> GetComentariosByProducto(int productoId)
        {
            try
            {
                var comentarios = await _comentarioService.GetByProductIdAsync(productoId);
                return Ok(comentarios);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetComentariosByProducto));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los comentarios del producto");
            }
        }

        // GET: api/Comentario/Usuario/5
        [HttpGet("Usuario/{usuarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ComentarioDTO>>> GetComentariosByUsuario(int usuarioId)
        {
            try
            {
                var comentarios = await _comentarioService.GetByUsuarioIdAsync(usuarioId);
                return Ok(comentarios);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetComentariosByUsuario));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los comentarios del usuario");
            }
        }

        // POST: api/Comentario
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ComentarioDTO>> PostComentario(CreateComentarioDTO comentarioDto)
        {
            try
            {
                var comentario = await _comentarioService.CreateAsync(comentarioDto);
                return CreatedAtAction(nameof(GetComentario), new { id = comentario.IdComentario }, comentario);
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
                await _errorHandlingService.LogErrorAsync(ex, nameof(PostComentario));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el comentario");
            }
        }

        // PUT: api/Comentario/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutComentario(int id, UpdateComentarioDTO comentarioDto)
        {
            try
            {
                await _comentarioService.UpdateAsync(id, comentarioDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PutComentario));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el comentario");
            }
        }

        // DELETE: api/Comentario/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            try
            {
                await _comentarioService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(DeleteComentario));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el comentario");
            }
        }
    }
}
