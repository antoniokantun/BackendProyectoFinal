using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLogController : ControllerBase
    {
        private readonly IErrorHandlingService _errorHandlingService;
        private readonly ILogger<ErrorLogController> _logger;

        public ErrorLogController(
            IErrorHandlingService errorHandlingService,
            ILogger<ErrorLogController> logger)
        {
            _errorHandlingService = errorHandlingService;
            _logger = logger;
        }

        // GET: api/ErrorLog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogErrorDTO>>> GetAllLogs()
        {
            try
            {
                var logs = await _errorHandlingService.GetAllLogsAsync();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener logs de errores");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los logs");
            }
        }

        // POST: api/ErrorLog
        [HttpPost]
        public async Task<ActionResult<LogErrorDTO>> LogError(CreateLogErrorDTO createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var loggedError = await _errorHandlingService.LogCustomErrorAsync(createDto.Mensaje, createDto.Origen);
                return CreatedAtAction(nameof(GetAllLogs), new { id = loggedError.IdLogError }, loggedError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar log de error");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al registrar el error");
            }
        }
    }
}
