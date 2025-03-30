using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoService _estadoService;
        private readonly IErrorHandlingService _errorHandlingService;
        private readonly ILogger<EstadoController> _logger;

        public EstadoController(
            IEstadoService estadoService,
            IErrorHandlingService errorHandlingService,
            ILogger<EstadoController> logger)
        {
            _estadoService = estadoService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
        }

        // GET: api/Estado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoDTO>>> GetEstados()
        {
            try
            {
                var estados = await _estadoService.GetAllAsync();
                return Ok(estados);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetEstados));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los estados");
            }
        }

        // GET: api/Estado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoDTO>> GetEstado(int id)
        {
            try
            {
                var estado = await _estadoService.GetByIdAsync(id);
                return Ok(estado);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetEstado));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el estado");
            }
        }

        // GET: api/Estado/nombre/{nombre}
        [HttpGet("nombre/{nombre}")]
        public async Task<ActionResult<EstadoDTO>> GetEstadoPorNombre(string nombre)
        {
            try
            {
                var estado = await _estadoService.GetByNombreAsync(nombre);
                return Ok(estado);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetEstadoPorNombre));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el estado por nombre");
            }
        }

        // POST: api/Estado
        [HttpPost]
        public async Task<ActionResult<EstadoDTO>> PostEstado(CreateEstadoDTO createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdEstado = await _estadoService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetEstado), new { id = createdEstado.IdEstado }, createdEstado);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PostEstado));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el estado");
            }
        }

        // PUT: api/Estado/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, UpdateEstadoDTO updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _estadoService.UpdateAsync(id, updateDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PutEstado));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el estado");
            }
        }

        // DELETE: api/Estado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            try
            {
                await _estadoService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(DeleteEstado));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el estado");
            }
        }
    }
}
