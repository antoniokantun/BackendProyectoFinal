using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private readonly IErrorHandlingService _errorHandlingService;
        private readonly ILogger<ProductoController> _logger;
        private readonly IFileStorageService _fileStorageService; // Agregar esto

        public ProductoController(IProductoService productoService, IErrorHandlingService errorHandlingService, ILogger<ProductoController> logger, IFileStorageService fileStorageService)
        {
            _productoService = productoService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
            _fileStorageService = fileStorageService;
        }

        // GET: api/Producto
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductos()
        {
            try
            {
                var productos = await _productoService.GetAllAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetProductos));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los productos");
            }
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductoDTO>> GetProducto(int id)
        {
            try
            {
                var producto = await _productoService.GetByIdAsync(id);
                return Ok(producto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetProducto));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el producto");
            }
        }

        // GET: api/Producto/detail/5
        [HttpGet("detail/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductoDetailDTO>> GetProductoDetail(int id)
        {
            try
            {
                var producto = await _productoService.GetDetailByIdAsync(id);
                return Ok(producto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetProductoDetail));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el detalle del producto");
            }
        }

        // GET: api/Producto/usuario/5
        [HttpGet("usuario/{usuarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductosByUsuarioId(int usuarioId)
        {
            try
            {
                var productos = await _productoService.GetByUsuarioIdAsync(usuarioId);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetProductosByUsuarioId));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los productos del usuario");
            }
        }

        // POST: api/Producto
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductoDTO>> PostProducto(ProductoDTO productoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Establecer la fecha de creación al momento actual si no se proporciona
                if (productoDto.FechaCreacion == default)
                {
                    productoDto.FechaCreacion = DateTime.Now;
                }

                var createdProducto = await _productoService.CreateAsync(productoDto);
                return CreatedAtAction(nameof(GetProducto), new { id = createdProducto.IdProducto }, createdProducto);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PostProducto));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el producto");
            }
        }

        // POST: api/Producto/completo-con-imagenes
        [HttpPost("completo-con-imagenes")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductoDTO>> PostProductoCompletoConImagenes([FromForm] ProductoCreateForm productoForm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var productoCreateDto = new ProductoCreateDTO
                {
                    Nombre = productoForm.Nombre,
                    Descripcion = productoForm.Descripcion,
                    ProcesoNegociacion = productoForm.ProcesoNegociacion,
                    Intercambio = productoForm.Intercambio,
                    NoVisible=false,
                    UsuarioId = productoForm.UsuarioId,
                    CategoriasIds = productoForm.CategoriasIds
                };

                // Procesar imágenes
                var imagenesUrl = new List<string>();
                if (productoForm.Imagenes != null)
                {
                    foreach (var imagen in productoForm.Imagenes)
                    {
                        var url = await _fileStorageService.SaveFileAsync(imagen, "productos");
                        imagenesUrl.Add(url);
                    }
                }
                productoCreateDto.ImagenesUrl = imagenesUrl;

                var createdProducto = await _productoService.CreateProductoCompletoAsync(productoCreateDto);
                return CreatedAtAction(nameof(GetProducto), new { id = createdProducto.IdProducto }, createdProducto);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PostProductoCompletoConImagenes));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el producto completo con imágenes");
            }
        }

        // BackendProyectoFinal.Presentation/Controllers/ProductoController.cs
        // Añadir este método al controlador existente

        // PUT: api/Producto/edicion-completa/{id}
        [HttpPut("edicion-completa/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductoDTO>> PutProductoEdicionCompleta(int id, [FromForm] ProductoEditForm productoForm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Obtener el producto actual para verificar que existe
                var productoActual = await _productoService.GetByIdAsync(id);

                // Crear el DTO de actualización
                var productoUpdateDto = new ProductoCreateDTO
                {
                    Nombre = productoForm.Nombre,
                    Descripcion = productoForm.Descripcion,
                    ProcesoNegociacion = productoForm.ProcesoNegociacion,
                    Intercambio = productoForm.Intercambio,
                    UsuarioId = productoForm.UsuarioId,
                    CategoriasIds = productoForm.CategoriasIds
                };

                // Procesar nuevas imágenes (si hay)
                var nuevasImagenesUrls = new List<string>();
                if (productoForm.NuevasImagenes != null && productoForm.NuevasImagenes.Any())
                {
                    foreach (var imagen in productoForm.NuevasImagenes)
                    {
                        // Guardar la imagen físicamente y obtener su URL
                        var url = await _fileStorageService.SaveFileAsync(imagen, "productos");
                        nuevasImagenesUrls.Add(url);
                    }
                }

                // Actualizar el producto con sus imágenes y categorías
                var productoActualizado = await _productoService.UpdateProductoCompletoConImagenesAsync(
                    id,
                    productoForm.ImagenesExistentesIds,
                    nuevasImagenesUrls,
                    productoUpdateDto);

                return Ok(productoActualizado);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PutProductoEdicionCompleta));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el producto completo");
            }
        }

        // PUT: api/Producto/completo/5
        [HttpPut("completo/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutProductoCompleto(int id, ProductoCreateDTO productoUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _productoService.UpdateProductoCompletoAsync(id, productoUpdateDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PutProductoCompleto));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el producto completo");
            }
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                await _productoService.DeleteProductoCompletoAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(DeleteProducto));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el producto");
            }
        }

        [HttpPatch("patch-producto-visible")] 
        public async Task<IActionResult> UpdateVisibility([FromBody] ProductoUpdateVisibilityDTO productoUpdateVisibilityDTO)
        {
            try
            {
                await _productoService.UpdateProductVisibility(productoUpdateVisibilityDTO);
                return NoContent(); 
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, "Error interno del servidor."); 
            }
        }
        [HttpPatch("patch-producto-report")]
        public async Task<IActionResult> UpdateProductReport([FromBody] ProductReportDTO productReportDTO)
        {
            try
            {
                await _productoService.UpdateProductReport(productReportDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, "Error interno del servidor.");
            }
        }

    }
}
