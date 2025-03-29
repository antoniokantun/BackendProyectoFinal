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

        // Endpoints existentes...
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

        // Nuevo endpoint para crear categoría con imagen
        // POST: api/Categoria/con-imagen
        [HttpPost("con-imagen")]
        public async Task<ActionResult<CategoriaDTO>> PostCategoriaConImagen([FromForm] CategoriaCreateForm categoriaForm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await _errorHandlingService.LogErrorAsync(
                        "Modelo de creación de categoría inválido",
                        nameof(PostCategoriaConImagen),
                        "Advertencia",
                        GetCurrentUserId()
                    );
                    return BadRequest(ModelState);
                }

                // Verificar que el archivo sea una imagen válida
                if (categoriaForm.Imagen != null)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(categoriaForm.Imagen.FileName).ToLowerInvariant();

                    if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                    {
                        return BadRequest("El archivo debe ser una imagen (jpg, jpeg, png o gif).");
                    }

                    // Verificar tamaño máximo (por ejemplo, 5MB)
                    if (categoriaForm.Imagen.Length > 5 * 1024 * 1024)
                    {
                        return BadRequest("La imagen no puede exceder los 5MB.");
                    }
                }

                var createdCategoria = await _categoriaService.CreateWithImageAsync(categoriaForm);
                return CreatedAtAction(nameof(GetCategoria), new { id = createdCategoria.IdCategoria }, createdCategoria);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(
                    ex,
                    nameof(PostCategoriaConImagen),
                    "Error",
                    GetCurrentUserId()
                );

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error al crear la categoría con imagen"
                );
            }
        }

        // Nuevo endpoint para actualizar categoría con imagen
        // PUT: api/Categoria/con-imagen/5
        [HttpPut("con-imagen/{id}")]
        public async Task<ActionResult<CategoriaDTO>> PutCategoriaConImagen([FromForm] CategoriaEditForm categoriaForm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await _errorHandlingService.LogErrorAsync(
                        "Modelo de actualización de categoría inválido",
                        nameof(PutCategoriaConImagen),
                        "Advertencia",
                        GetCurrentUserId()
                    );
                    return BadRequest(ModelState);
                }

                // Verificar que el archivo sea una imagen válida (si se proporciona)
                if (categoriaForm.NuevaImagen != null)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(categoriaForm.NuevaImagen.FileName).ToLowerInvariant();

                    if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                    {
                        return BadRequest("El archivo debe ser una imagen (jpg, jpeg, png o gif).");
                    }

                    // Verificar tamaño máximo (por ejemplo, 5MB)
                    if (categoriaForm.NuevaImagen.Length > 5 * 1024 * 1024)
                    {
                        return BadRequest("La imagen no puede exceder los 5MB.");
                    }
                }

                var updatedCategoria = await _categoriaService.UpdateWithImageAsync(categoriaForm);
                return Ok(updatedCategoria);
            }
            catch (KeyNotFoundException ex)
            {
                await _errorHandlingService.LogErrorAsync(
                    ex.Message,
                    nameof(PutCategoriaConImagen),
                    "Advertencia",
                    GetCurrentUserId()
                );
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(
                    ex,
                    nameof(PutCategoriaConImagen),
                    "Error",
                    GetCurrentUserId()
                );

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Error al actualizar la categoría con imagen"
                );
            }
        }
    }
}