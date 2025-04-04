﻿using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace BackendProyectoFinal.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;
        private readonly IErrorHandlingService _errorHandlingService;
        private readonly ILogger<RolController> _logger;

        public RolController(
            IRolService rolService,
            IErrorHandlingService errorHandlingService,
            ILogger<RolController> logger)
        {
            _rolService = rolService;
            _errorHandlingService = errorHandlingService;
            _logger = logger;
        }

        // Método privado para formatear errores de validación
        private string FormatValidationErrors()
        {
            var errors = new StringBuilder();
            errors.AppendLine("Errores de validación:");

            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errors.AppendLine(error.ErrorMessage);
                }
            }

            return errors.ToString();
        }

        // GET: api/Rol
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolDTO>>> GetRoles()
        {
            try
            {
                var roles = await _rolService.GetAllAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los roles");
            }
        }

        // GET: api/Rol/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RolDTO>> GetRol(int id)
        {
            try
            {
                // Validación manual para demostrar registro de errores
                if (id <= 0)
                {
                    await _errorHandlingService.LogCustomErrorAsync("ID de rol inválido. Debe ser mayor que cero.", "Backend");
                    return BadRequest("El ID del rol debe ser mayor que cero.");
                }

                var rol = await _rolService.GetByIdAsync(id);
                return Ok(rol);
            }
            catch (KeyNotFoundException ex)
            {
                await _errorHandlingService.LogCustomErrorAsync(ex.Message, "Backend");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el rol");
            }
        }

        // POST: api/Rol
        [HttpPost]
        public async Task<ActionResult<RolDTO>> PostRol(RolDTO rolDto)
        {
            try
            {
                // Validación manual antes de verificar ModelState
                if (rolDto == null)
                {
                    await _errorHandlingService.LogCustomErrorAsync("RolDTO es nulo", "Backend");
                    return BadRequest("El rol no puede ser nulo");
                }

                // Verificar ModelState y registrar errores de validación
                if (!ModelState.IsValid)
                {
                    // Formatear y registrar errores de validación
                    var validationErrorMessage = FormatValidationErrors();
                    await _errorHandlingService.LogCustomErrorAsync(validationErrorMessage, "Backend");
                    return BadRequest(ModelState);
                }

                var createdRol = await _rolService.CreateAsync(rolDto);
                return CreatedAtAction(nameof(GetRol), new { id = createdRol.IdRol }, createdRol);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el rol");
            }
        }

        // PUT: api/Rol/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRol(int id, RolDTO rolDto)
        {
            try
            {
                // Validación de ID
                if (id <= 0)
                {
                    await _errorHandlingService.LogCustomErrorAsync("ID de rol inválido. Debe ser mayor que cero.", "Backend");
                    return BadRequest("El ID del rol debe ser mayor que cero");
                }

                // Validación de coincidencia de ID
                if (id != rolDto.IdRol)
                {
                    await _errorHandlingService.LogCustomErrorAsync("El ID no coincide con el ID del rol proporcionado", "Backend");
                    return BadRequest("El ID no coincide con el ID del rol proporcionado");
                }

                // Verificar ModelState y registrar errores de validación
                if (!ModelState.IsValid)
                {
                    // Formatear y registrar errores de validación
                    var validationErrorMessage = FormatValidationErrors();
                    await _errorHandlingService.LogCustomErrorAsync(validationErrorMessage, "Backend");
                    return BadRequest(ModelState);
                }

                await _rolService.UpdateAsync(rolDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                await _errorHandlingService.LogCustomErrorAsync(ex.Message, "Backend");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el rol");
            }
        }

        // DELETE: api/Rol/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            try
            {
                // Validación manual de ID
                if (id <= 0)
                {
                    await _errorHandlingService.LogCustomErrorAsync("ID de rol inválido. Debe ser mayor que cero.", "Backend");
                    return BadRequest("El ID del rol debe ser mayor que cero");
                }

                await _rolService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                await _errorHandlingService.LogCustomErrorAsync(ex.Message, "Backend");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, "Backend");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el rol");
            }
        }
    }
}