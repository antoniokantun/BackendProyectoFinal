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
        private readonly IFileStorageService _fileStorageService;

        public CategoriaController(
            ICategoriaService categoriaService,
            IErrorHandlingService errorHandlingService,
            ILogger<CategoriaController> logger,
            IFileStorageService fileStorageService)
        {
            _categoriaService = categoriaService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
            _fileStorageService = fileStorageService;
        }

        // Método privado para obtener el ID del usuario actual
        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim != null ? int.Parse(userIdClaim) : null;
        }

        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias()
        {
            try
            {
                var categorias = await _categoriaService.GetAllAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                // Usar el nuevo servicio simplificado de logs
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener las categorías");
            }
        }

        // GET: api/Categoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoria(int id)
        {
            try
            {
                var categoria = await _categoriaService.GetByIdAsync(id);
                return Ok(categoria);
            }
            catch (KeyNotFoundException ex)
            {
                // Registrar error de no encontrado
                await _errorHandlingService.LogCustomErrorAsync(ex.Message, "Backend");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Registrar otros errores
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener la categoría");
            }
        }

        // POST: api/Categoria/con-imagen
        [HttpPost("con-imagen")]
        public async Task<ActionResult<CategoriaDTO>> PostCategoriaConImagen([FromForm] CategoriaCreateForm categoriaForm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await _errorHandlingService.LogCustomErrorAsync("Modelo de creación de categoría inválido", "Backend");
                    return BadRequest(ModelState);
                }

                // Verificar que el archivo sea una imagen válida
                if (categoriaForm.Imagen != null)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(categoriaForm.Imagen.FileName).ToLowerInvariant();

                    if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                    {
                        await _errorHandlingService.LogCustomErrorAsync("Formato de imagen no válido", "Backend");
                        return BadRequest("El archivo debe ser una imagen (jpg, jpeg, png o gif).");
                    }

                    // Verificar tamaño máximo (por ejemplo, 5MB)
                    if (categoriaForm.Imagen.Length > 5 * 1024 * 1024)
                    {
                        await _errorHandlingService.LogCustomErrorAsync("Imagen demasiado grande", "Backend");
                        return BadRequest("La imagen no puede exceder los 5MB.");
                    }
                }

                var createdCategoria = await _categoriaService.CreateWithImageAsync(categoriaForm);
                return CreatedAtAction(nameof(GetCategoria), new { id = createdCategoria.IdCategoria }, createdCategoria);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la categoría con imagen");
            }
        }

        // PUT: api/Categoria/con-imagen/5
        [HttpPut("con-imagen/{id}")]
        public async Task<ActionResult<CategoriaDTO>> PutCategoriaConImagen([FromForm] CategoriaEditForm categoriaForm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await _errorHandlingService.LogCustomErrorAsync("Modelo de actualización de categoría inválido", "Backend");
                    return BadRequest(ModelState);
                }

                // Verificar que el archivo sea una imagen válida (si se proporciona)
                if (categoriaForm.NuevaImagen != null)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(categoriaForm.NuevaImagen.FileName).ToLowerInvariant();

                    if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                    {
                        await _errorHandlingService.LogCustomErrorAsync("Formato de imagen no válido", "Backend");
                        return BadRequest("El archivo debe ser una imagen (jpg, jpeg, png o gif).");
                    }

                    // Verificar tamaño máximo (por ejemplo, 5MB)
                    if (categoriaForm.NuevaImagen.Length > 5 * 1024 * 1024)
                    {
                        await _errorHandlingService.LogCustomErrorAsync("Imagen demasiado grande", "Backend");
                        return BadRequest("La imagen no puede exceder los 5MB.");
                    }
                }

                var updatedCategoria = await _categoriaService.UpdateWithImageAsync(categoriaForm);
                return Ok(updatedCategoria);
            }
            catch (KeyNotFoundException ex)
            {
                await _errorHandlingService.LogCustomErrorAsync(ex.Message, "Backend");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar la categoría con imagen");
            }
        }
    }
}