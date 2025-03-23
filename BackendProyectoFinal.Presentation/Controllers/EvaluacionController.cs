using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluacionController : ControllerBase
    {
        private readonly IEvaluacionService _evaluacionService;
        private readonly IErrorHandlingService _errorHandlingService;
        private readonly ILogger<EvaluacionController> _logger;

        public EvaluacionController(IEvaluacionService evaluacionService, IErrorHandlingService errorHandlingService, ILogger<EvaluacionController> logger)
        {
            _evaluacionService = evaluacionService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
        }

        // GET: api/Evaluacion
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EvaluacionDTO>>> GetEvaluaciones()
        {
            try
            {
                var evaluaciones = await _evaluacionService.GetAllAsync();
                return Ok(evaluaciones);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetEvaluaciones));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener las evaluaciones");
            }
        }

        // GET: api/Evaluacion/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EvaluacionDTO>> GetEvaluacion(int id)
        {
            try
            {
                var evaluacion = await _evaluacionService.GetByIdAsync(id);
                return Ok(evaluacion);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetEvaluacion));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener la evaluación");
            }
        }

        // GET: api/Evaluacion/Usuario/5
        [HttpGet("Usuario/{usuarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EvaluacionDTO>>> GetEvaluacionesPorUsuario(int usuarioId)
        {
            try
            {
                var evaluaciones = await _evaluacionService.GetByUsuarioIdAsync(usuarioId);
                return Ok(evaluaciones);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetEvaluacionesPorUsuario));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener las evaluaciones del usuario");
            }
        }

        // GET: api/Evaluacion/Producto/5
        [HttpGet("Producto/{productoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EvaluacionDTO>>> GetEvaluacionesPorProducto(int productoId)
        {
            try
            {
                var evaluaciones = await _evaluacionService.GetByProductoIdAsync(productoId);
                return Ok(evaluaciones);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetEvaluacionesPorProducto));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener las evaluaciones del producto");
            }
        }

        // POST: api/Evaluacion
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EvaluacionDTO>> PostEvaluacion(CreateEvaluacionDTO createEvaluacionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdEvaluacion = await _evaluacionService.CreateAsync(createEvaluacionDto);
                return CreatedAtAction(nameof(GetEvaluacion), new { id = createdEvaluacion.IdEvaluacion }, createdEvaluacion);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PostEvaluacion));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la evaluación");
            }
        }

        // PUT: api/Evaluacion/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutEvaluacion(int id, UpdateEvaluacionDTO updateEvaluacionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _evaluacionService.UpdateAsync(id, updateEvaluacionDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PutEvaluacion));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar la evaluación");
            }
        }

        // DELETE: api/Evaluacion/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEvaluacion(int id)
        {
            try
            {
                await _evaluacionService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(DeleteEvaluacion));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar la evaluación");
            }
        }
    }
}
