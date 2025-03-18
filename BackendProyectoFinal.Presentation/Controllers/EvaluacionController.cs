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
            _logger.LogInformation("Obteniendo evaluaciones");
            try
            {
                var evaluaciones = await _evaluacionService.GetAllAsync();
                return Ok(evaluaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluaciones");
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

        // GET: api/Evaluacion/producto/5
        [HttpGet("producto/{productoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EvaluacionDTO>>> GetEvaluacionesByProducto(int productoId)
        {
            try
            {
                var evaluaciones = await _evaluacionService.GetByProductIdAsync(productoId);
                return Ok(evaluaciones);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetEvaluacionesByProducto));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener las evaluaciones del producto");
            }
        }

        // GET: api/Evaluacion/usuario/5
        [HttpGet("usuario/{usuarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EvaluacionDTO>>> GetEvaluacionesByUsuario(int usuarioId)
        {
            try
            {
                var evaluaciones = await _evaluacionService.GetByUsuarioIdAsync(usuarioId);
                return Ok(evaluaciones);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetEvaluacionesByUsuario));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener las evaluaciones del usuario");
            }
        }

        // GET: api/Evaluacion/promedio/5
        [HttpGet("promedio/{productoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<double>> GetPromedioEvaluacionesByProducto(int productoId)
        {
            try
            {
                var promedio = await _evaluacionService.GetPromedioByProductIdAsync(productoId);
                return Ok(promedio);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetPromedioEvaluacionesByProducto));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el promedio de evaluaciones del producto");
            }
        }

        // POST: api/Evaluacion
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EvaluacionDTO>> PostEvaluacion(CreateEvaluacionDTO evaluacionDto)
        {
            try
            {
                var evaluacion = await _evaluacionService.CreateAsync(evaluacionDto);
                return CreatedAtAction(nameof(GetEvaluacion), new { id = evaluacion.IdEvaluacion }, evaluacion);
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
        public async Task<IActionResult> PutEvaluacion(int id, UpdateEvaluacionDTO evaluacionDto)
        {
            try
            {
                await _evaluacionService.UpdateAsync(id, evaluacionDto);
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

        // GET: api/Evaluacion/existe/1/2
        [HttpGet("existe/{usuarioId}/{productoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> ExisteEvaluacionUsuarioProducto(int usuarioId, int productoId)
        {
            try
            {
                var existe = await _evaluacionService.ExisteEvaluacionUsuarioProductoAsync(usuarioId, productoId);
                return Ok(existe);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(ExisteEvaluacionUsuarioProducto));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al verificar la existencia de la evaluación");
            }
        }
    }
}
