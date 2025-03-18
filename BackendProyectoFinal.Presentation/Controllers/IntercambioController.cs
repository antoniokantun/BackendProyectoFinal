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

        public IntercambioController(IIntercambioService intercambioService, IErrorHandlingService errorHandlingService, ILogger<IntercambioController> logger)
        {
            _intercambioService = intercambioService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
        }

        // GET: api/Intercambio
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<IntercambioDTO>>> GetIntercambios()
        {
            _logger.LogInformation("Obteniendo intercambios");
            try
            {
                var intercambios = await _intercambioService.GetAllAsync();
                return Ok(intercambios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener intercambios");
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetIntercambios));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los intercambios");
            }
        }

        // GET: api/Intercambio/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        // POST: api/Intercambio
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IntercambioDTO>> PostIntercambio(CreateIntercambioDTO intercambioDto)
        {
            try
            {
                var intercambio = await _intercambioService.CreateAsync(intercambioDto);
                return CreatedAtAction(nameof(GetIntercambio), new { id = intercambio.IdIntercambio }, intercambio);
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
                await _errorHandlingService.LogErrorAsync(ex, nameof(PostIntercambio));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el intercambio");
            }
        }

        // PUT: api/Intercambio/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutIntercambio(int id, UpdateIntercambioDTO intercambioDto)
        {
            try
            {
                await _intercambioService.UpdateAsync(id, intercambioDto);
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
                await _errorHandlingService.LogErrorAsync(ex, nameof(PutIntercambio));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el intercambio");
            }
        }

        // DELETE: api/Intercambio/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
