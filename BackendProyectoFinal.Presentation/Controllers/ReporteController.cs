using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteService _reporteService;
        private readonly IErrorHandlingService _errorHandlingService;
        private readonly ILogger<ReporteController> _logger;

        public ReporteController(
            IReporteService reporteService,
            IErrorHandlingService errorHandlingService,
            ILogger<ReporteController> logger)
        {
            _reporteService = reporteService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
        }

        // Método privado para obtener el ID del usuario actual
        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim != null ? int.Parse(userIdClaim) : null;
        }

        // GET: api/Reporte
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReporteDTO>>> GetReportes()
        {
            try
            {
                var reportes = await _reporteService.GetAllReportesAsync();
                return Ok(reportes);
            }
            catch (Exception ex)
            {
                // Registrar el error en la base de datos
                await _errorHandlingService.LogErrorAsync(
                    ex,
                    nameof(GetReportes),
                    "Error",
                    GetCurrentUserId()
                );

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error al obtener los reportes"
                );
            }
        }

        // GET: api/Reporte/usuarios
        [HttpGet("usuarios")]
        public async Task<ActionResult<IEnumerable<ReporteUsuarioDTO>>> GetReportesUsuario()
        {
            try
            {
                var reportes = await _reporteService.GetAllReportesUsuarioAsync();
                return Ok(reportes);
            }
            catch (Exception ex)
            {
                // Registrar el error en la base de datos
                await _errorHandlingService.LogErrorAsync(
                    ex,
                    nameof(GetReportesUsuario),
                    "Error",
                    GetCurrentUserId()
                );

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error al obtener los reportes de usuarios"
                );
            }
        }

        // GET: api/Reporte/productos
        [HttpGet("productos")]
        public async Task<ActionResult<IEnumerable<ReporteProductoDTO>>> GetReportesProducto()
        {
            try
            {
                var reportes = await _reporteService.GetAllReportesProductoAsync();
                return Ok(reportes);
            }
            catch (Exception ex)
            {
                // Registrar el error en la base de datos
                await _errorHandlingService.LogErrorAsync(
                    ex,
                    nameof(GetReportesProducto),
                    "Error",
                    GetCurrentUserId()
                );

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error al obtener los reportes de productos"
                );
            }
        }

        // POST: api/Reporte/usuario
        [HttpPost("usuario")]
        public async Task<ActionResult<ReporteUsuarioDTO>> ReportarUsuario(ReporteUsuarioCreateForm reporteForm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await _errorHandlingService.LogErrorAsync(
                        "Modelo de reporte de usuario inválido",
                        nameof(ReportarUsuario),
                        "Advertencia",
                        GetCurrentUserId()
                    );
                    return BadRequest(ModelState);
                }

                var createdReporte = await _reporteService.ReportarUsuarioAsync(reporteForm);
                return CreatedAtAction(nameof(GetReportes), new { id = createdReporte.IdReporte }, createdReporte);
            }
            catch (KeyNotFoundException ex)
            {
                await _errorHandlingService.LogErrorAsync(
                    ex.Message,
                    nameof(ReportarUsuario),
                    "Advertencia",
                    GetCurrentUserId()
                );
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(
                    ex,
                    nameof(ReportarUsuario),
                    "Error",
                    GetCurrentUserId()
                );

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error al crear el reporte de usuario"
                );
            }
        }

        // POST: api/Reporte/producto
        [HttpPost("producto")]
        public async Task<ActionResult<ReporteProductoDTO>> ReportarProducto(ReporteProductoCreateForm reporteForm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await _errorHandlingService.LogErrorAsync(
                        "Modelo de reporte de producto inválido",
                        nameof(ReportarProducto),
                        "Advertencia",
                        GetCurrentUserId()
                    );
                    return BadRequest(ModelState);
                }

                var createdReporte = await _reporteService.ReportarProductoAsync(reporteForm);
                return CreatedAtAction(nameof(GetReportes), new { id = createdReporte.IdReporte }, createdReporte);
            }
            catch (KeyNotFoundException ex)
            {
                await _errorHandlingService.LogErrorAsync(
                    ex.Message,
                    nameof(ReportarProducto),
                    "Advertencia",
                    GetCurrentUserId()
                );
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(
                    ex,
                    nameof(ReportarProducto),
                    "Error",
                    GetCurrentUserId()
                );

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error al crear el reporte de producto"
                );
            }
        }
    }
}