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

        // POST: api/Producto/completo
        [HttpPost("completo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductoDTO>> PostProductoCompleto(ProductoCreateDTO productoCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdProducto = await _productoService.CreateProductoCompletoAsync(productoCreateDto);
                return CreatedAtAction(nameof(GetProducto), new { id = createdProducto.IdProducto }, createdProducto);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PostProductoCompleto));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el producto completo");
            }
        }

        // PUT: api/Producto/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutProducto(int id, ProductoDTO productoDto)
        {
            try
            {
                if (id != productoDto.IdProducto)
                    return BadRequest("El ID no coincide con el ID del producto proporcionado");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _productoService.UpdateAsync(productoDto);
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
    }
}
