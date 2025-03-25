using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IErrorHandlingService _errorHandlingService;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(
            ICategoriaService categoriaService,
            IErrorHandlingService errorHandlingService,
            ILogger<CategoriaController> logger)
        {
            _categoriaService = categoriaService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
        }

        // Método privado para obtener el ID del usuario actual
        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim != null ? int.Parse(userIdClaim) : null;
        }

        // GET: api/Categoria
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias()
        {
            try
            {
                var categorias = await _categoriaService.GetAllAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                // Registrar el error en la base de datos
                await _errorHandlingService.LogErrorAsync(
                    ex,
                    nameof(GetCategorias),
                    "Error",
                    GetCurrentUserId()
                );

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error al obtener las categorías"
                );
            }
        }

        // GET: api/Categoria/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoriaDTO>> GetCategoria(int id)
        {
            try
            {
                var categoria = await _categoriaService.GetByIdAsync(id);
                return Ok(categoria);
            }
            catch (KeyNotFoundException ex)
            {
                // Registrar error de no encontrado con severidad de Advertencia
                await _errorHandlingService.LogErrorAsync(
                    ex.Message,
                    nameof(GetCategoria),
                    "Advertencia",
                    GetCurrentUserId()
                );
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Registrar otros errores en la base de datos
                await _errorHandlingService.LogErrorAsync(
                    ex,
                    nameof(GetCategoria),
                    "Error",
                    GetCurrentUserId()
                );

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error al obtener la categoría"
                );
            }
        }

        // POST: api/Categoria
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoriaDTO>> PostCategoria(CategoriaDTO categoriaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Registrar modelo inválido con severidad de Advertencia
                    await _errorHandlingService.LogErrorAsync(
                        "Modelo de creación de categoría inválido",
                        nameof(PostCategoria),
                        "Advertencia",
                        GetCurrentUserId()
                    );
                    return BadRequest(ModelState);
                }

                var createdCategoria = await _categoriaService.CreateAsync(categoriaDto);
                return CreatedAtAction(nameof(GetCategoria), new { id = createdCategoria.IdCategoria }, createdCategoria);
            }
            catch (Exception ex)
            {
                // Registrar otros errores en la base de datos
                await _errorHandlingService.LogErrorAsync(
                    ex,
                    nameof(PostCategoria),
                    "Error",
                    GetCurrentUserId()
                );

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error al crear la categoría"
                );
            }
        }

        // PUT: api/Categoria/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutCategoria(int id, CategoriaDTO categoriaDto)
        {
            try
            {
                if (id != categoriaDto.IdCategoria)
                {
                    // Registrar error de ID no coincidente con severidad de Advertencia
                    await _errorHandlingService.LogErrorAsync(
                        "El ID no coincide con el ID de la categoría proporcionada",
                        nameof(PutCategoria),
                        "Advertencia",
                        GetCurrentUserId()
                    );
                    return BadRequest("El ID no coincide con el ID de la categoría proporcionada");
                }

                if (!ModelState.IsValid)
                {
                    // Registrar modelo inválido con severidad de Advertencia
                    await _errorHandlingService.LogErrorAsync(
                        "Modelo de actualización de categoría inválido",
                        nameof(PutCategoria),
                        "Advertencia",
                        GetCurrentUserId()
                    );
                    return BadRequest(ModelState);
                }

                await _categoriaService.UpdateAsync(categoriaDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Registrar error de no encontrado con severidad de Advertencia
                await _errorHandlingService.LogErrorAsync(
                    ex.Message,
                    nameof(PutCategoria),
                    "Advertencia",
                    GetCurrentUserId()
                );
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Registrar otros errores en la base de datos
                await _errorHandlingService.LogErrorAsync(
                    ex,
                    nameof(PutCategoria),
                    "Error",
                    GetCurrentUserId()
                );

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error al actualizar la categoría"
                );
            }
        }

        // DELETE: api/Categoria/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            try
            {
                await _categoriaService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Registrar error de no encontrado con severidad de Advertencia
                await _errorHandlingService.LogErrorAsync(
                    ex.Message,
                    nameof(DeleteCategoria),
                    "Advertencia",
                    GetCurrentUserId()
                );
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Registrar otros errores en la base de datos
                await _errorHandlingService.LogErrorAsync(
                    ex,
                    nameof(DeleteCategoria),
                    "Error",
                    GetCurrentUserId()
                );

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error al eliminar la categoría"
                );
            }
        }
    }
}