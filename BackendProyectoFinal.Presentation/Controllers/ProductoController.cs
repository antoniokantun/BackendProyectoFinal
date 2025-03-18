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

        public ProductoController(IProductoService productoService, IErrorHandlingService errorHandlingService, ILogger<ProductoController> logger)
        {
            _productoService = productoService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
        }

        // GET: api/Productos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductos()
        {
            _logger.LogInformation("Obteniendo productos");
            try
            {
                var productos = await _productoService.GetAllAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos");
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetProductos));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los productos");
            }
        }

        // GET: api/Productos/5
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

        // GET: api/Productos/usuario/5
        [HttpGet("usuario/{usuarioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductosByUsuario(int usuarioId)
        {
            try
            {
                var productos = await _productoService.GetByUsuarioIdAsync(usuarioId);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetProductosByUsuario));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los productos del usuario");
            }
        }

        // POST: api/Productos
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductoDTO>> PostProducto(CreateProductoDTO productoDto)
        {
            try
            {
                var producto = await _productoService.CreateAsync(productoDto);
                return CreatedAtAction(nameof(GetProducto), new { id = producto.IdProducto }, producto);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PostProducto));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el producto");
            }
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutProducto(int id, UpdateProductoDTO productoDto)
        {
            try
            {
                await _productoService.UpdateAsync(id, productoDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PutProducto));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el producto");
            }
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                await _productoService.DeleteAsync(id);
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
    }
}
