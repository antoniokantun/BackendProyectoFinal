using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Application.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using BackendProyectoFinal.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.Services
{
    public class LogErrorService : ILogErrorService
    {
        private readonly ILogErrorRepository _logErrorRepository;
        private readonly IErrorHandlingService _errorHandlingService;

        public LogErrorService(ILogErrorRepository logErrorRepository, IErrorHandlingService errorHandlingService)
        {
            _logErrorRepository = logErrorRepository;
            _errorHandlingService = errorHandlingService;
        }

        public async Task<IEnumerable<LogErrorDTO>> GetAllAsync()
        {
            try
            {
                var logs = await _logErrorRepository.GetAllAsync();
                return logs.Select(l => new LogErrorDTO
                {
                    IdLogError = l.IdLogError,
                    Mensaje = l.Mensaje,
                    FechaOcurrencia = l.FechaOcurrencia,
                    Origen = l.Origen,
                    Severidad = l.Severidad,
                    StackTrace = l.StackTrace,
                    UsuarioId = l.UsuarioId
                });
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetAllAsync));
                throw;
            }
        }

        public async Task<LogErrorDTO> GetByIdAsync(int id)
        {
            try
            {
                var log = await _logErrorRepository.GetByIdAsync(id);
                if (log == null)
                {
                    await _errorHandlingService.LogErrorAsync($"Log de error con ID {id} no encontrado", nameof(GetByIdAsync), "Advertencia");
                    throw new KeyNotFoundException($"Log de error con ID {id} no encontrado");
                }

                return new LogErrorDTO
                {
                    IdLogError = log.IdLogError,
                    Mensaje = log.Mensaje,
                    FechaOcurrencia = log.FechaOcurrencia,
                    Origen = log.Origen,
                    Severidad = log.Severidad,
                    StackTrace = log.StackTrace,
                    UsuarioId = log.UsuarioId
                };
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetByIdAsync));
                throw;
            }
        }

        public async Task<IEnumerable<LogErrorDTO>> GetByUserIdAsync(int userId)
        {
            try
            {
                var logs = await _logErrorRepository.GetByUserIdAsync(userId);
                return logs.Select(l => new LogErrorDTO
                {
                    IdLogError = l.IdLogError,
                    Mensaje = l.Mensaje,
                    FechaOcurrencia = l.FechaOcurrencia,
                    Origen = l.Origen,
                    Severidad = l.Severidad,
                    StackTrace = l.StackTrace,
                    UsuarioId = l.UsuarioId
                });
            }
            catch (Exception ex)
            {
                await _errorHandlingService.LogErrorAsync(ex, nameof(GetByUserIdAsync));
                throw;
            }
        }
    }
}
