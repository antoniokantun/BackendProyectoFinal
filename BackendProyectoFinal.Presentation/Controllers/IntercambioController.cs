using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntercambioController : ControllerBase
    {
        private readonly IIntercambioService _intercambioService;
        private readonly IErrorHandlingService _errorHandlingService;
        private readonly ILogger<IntercambioController> _logger;

        public IntercambioController(
            IIntercambioService intercambioService,
            IErrorHandlingService errorHandlingService,
            ILogger<IntercambioController> logger)
        {
            _intercambioService = intercambioService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
        }

        // GET: api/Intercambio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IntercambioDTO>>> GetIntercambios()
        {
            try
            {
                var intercambios = await _intercambioService.GetAllAsync();
                return Ok(intercambios);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetIntercambios));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los intercambios");
            }
        }

        // GET: api/Intercambio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IntercambioDTO>> GetIntercambio(int id)
        {
            try
            {
                var intercambio = await _intercambioService.GetByIdAsync(id);
                return Ok(intercambio);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetIntercambio));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el intercambio");
            }
        }

        // GET: api/Intercambio/solicitante/5
        [HttpGet("solicitante/{usuarioSolicitanteId}")]
        public async Task<ActionResult<IEnumerable<IntercambioDTO>>> GetIntercambiosPorSolicitante(int usuarioSolicitanteId)
        {
            try
            {
                var intercambios = await _intercambioService.GetByUsuarioSolicitanteIdAsync(usuarioSolicitanteId);
                return Ok(intercambios);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetIntercambiosPorSolicitante));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los intercambios por solicitante");
            }
        }

        // GET: api/Intercambio/ofertante/5
        [HttpGet("ofertante/{usuarioOfertanteId}")]
        public async Task<ActionResult<IEnumerable<IntercambioDTO>>> GetIntercambiosPorOfertante(int usuarioOfertanteId)
        {
            try
            {
                var intercambios = await _intercambioService.GetByUsuarioOfertanteIdAsync(usuarioOfertanteId);
                return Ok(intercambios);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetIntercambiosPorOfertante));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los intercambios por ofertante");
            }
        }

        // GET: api/Intercambio/producto/5
        [HttpGet("producto/{productoId}")]
        public async Task<ActionResult<IEnumerable<IntercambioDTO>>> GetIntercambiosPorProducto(int productoId)
        {
            try
            {
                var intercambios = await _intercambioService.GetByProductoIdAsync(productoId);
                return Ok(intercambios);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetIntercambiosPorProducto));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los intercambios por producto");
            }
        }

        // POST: api/Intercambio
        [HttpPost]
        public async Task<ActionResult<IntercambioDTO>> PostIntercambio(CreateIntercambioDTO createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdIntercambio = await _intercambioService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetIntercambio), new { id = createdIntercambio.IdIntercambio }, createdIntercambio);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PostIntercambio));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el intercambio");
            }
        }

        // PUT: api/Intercambio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntercambio(int id, UpdateIntercambioDTO updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _intercambioService.UpdateAsync(id, updateDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PutIntercambio));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el intercambio");
            }
        }

        // DELETE: api/Intercambio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntercambio(int id)
        {
            try
            {
                await _intercambioService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(DeleteIntercambio));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el intercambio");
            }
        }
    }
}
