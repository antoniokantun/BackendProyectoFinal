using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;
        private readonly IErrorHandlingService _errorHandlingService;
        private readonly ILogger<RolController> _logger;

        public RolController(IRolService rolService, IErrorHandlingService errorHandlingService, ILogger<RolController> logger)
        {
            _rolService = rolService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
        }

        // GET: api/Rol
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RolDTO>>> GetRoles()
        {
            try
            {
                var roles = await _rolService.GetAllAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetRoles));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los roles");
            }
        }

        // GET: api/Rol/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RolDTO>> GetRol(int id)
        {
            try
            {
                var rol = await _rolService.GetByIdAsync(id);
                return Ok(rol);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetRol));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el rol");
            }
        }

        // POST: api/Rol
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RolDTO>> PostRol(RolDTO rolDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdRol = await _rolService.CreateAsync(rolDto);
                return CreatedAtAction(nameof(GetRol), new { id = createdRol.IdRol }, createdRol);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PostRol));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el rol");
            }
        }

        // PUT: api/Rol/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutRol(int id, RolDTO rolDto)
        {
            try
            {
                if (id != rolDto.IdRol)
                    return BadRequest("El ID no coincide con el ID del rol proporcionado");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _rolService.UpdateAsync(rolDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(PutRol));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el rol");
            }
        }

        // DELETE: api/Rol/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRol(int id)
        {
            try
            {
                await _rolService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(DeleteRol));
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el rol");
            }
        }
    }
}
