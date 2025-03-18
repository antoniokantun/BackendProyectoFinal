using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IErrorHandlingService _errorHandlingService;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(ICategoriaService categoriaService, IErrorHandlingService errorHandlingService, ILogger<CategoriaController> logger)
        {
            _categoriaService = categoriaService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
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
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetCategorias));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener las categorías");
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetCategoria));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener la categoría");
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
                    return BadRequest(ModelState);

                var createdCategoria = await _categoriaService.CreateAsync(categoriaDto);
                return CreatedAtAction(nameof(GetCategoria), new { id = createdCategoria.IdCategoria }, createdCategoria);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PostCategoria));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la categoría");
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
                    return BadRequest("El ID no coincide con el ID de la categoría proporcionada");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _categoriaService.UpdateAsync(categoriaDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PutCategoria));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar la categoría");
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
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(DeleteCategoria));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar la categoría");
            }
        }
    }
}
