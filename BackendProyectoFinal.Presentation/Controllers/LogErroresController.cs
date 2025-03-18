using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogErroresController : ControllerBase
    {
        private readonly ILogErrorService _logErrorService;
        private readonly IErrorHandlingService _errorHandlingService;

        public LogErroresController(ILogErrorService logErrorService, IErrorHandlingService errorHandlingService)
        {
            _logErrorService = logErrorService;
            _errorHandlingService = errorHandlingService;
        }

        // GET: api/LogErrores
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LogErrorDTO>>> GetLogErrores()
        {
            try
            {
                var logs = await _logErrorService.GetAllAsync();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetLogErrores));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los logs de errores");
            }
        }

        // GET: api/LogErrores/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LogErrorDTO>> GetLogError(int id)
        {
            try
            {
                var log = await _logErrorService.GetByIdAsync(id);
                return Ok(log);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetLogError));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el log de error");
            }
        }

        // GET: api/LogErrores/usuario/5
        [HttpGet("usuario/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LogErrorDTO>>> GetLogErroresByUsuario(int userId)
        {
            try
            {
                var logs = await _logErrorService.GetByUserIdAsync(userId);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetLogErroresByUsuario));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los logs de errores por usuario");
            }
        }
    }
}
