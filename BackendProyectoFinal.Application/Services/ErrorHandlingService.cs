using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.Services
{
    public class ErrorHandlingService : IErrorHandlingService
    {
        private readonly ILogErrorRepository _logErrorRepository;

        public ErrorHandlingService(ILogErrorRepository logErrorRepository)
        {
            _logErrorRepository = logErrorRepository;
        }

        public async Task LogErrorAsync(Exception ex, string origen, string severidad = "Error", int? usuarioId = null)
        {
            var logError = new LogError
            {
                Mensaje = ex.Message,
                FechaOcurrencia = DateTime.UtcNow,
                Origen = origen,
                Severidad = severidad,
                StackTrace = ex.StackTrace,
                UsuarioId = usuarioId
            };

            await _logErrorRepository.CreateAsync(logError);
        }

        public async Task LogErrorAsync(string mensaje, string origen, string severidad = "Error", int? usuarioId = null)
        {
            var logError = new LogError
            {
                Mensaje = mensaje,
                FechaOcurrencia = DateTime.UtcNow,
                Origen = origen,
                Severidad = severidad,
                UsuarioId = usuarioId
            };

            await _logErrorRepository.CreateAsync(logError);
        }
    }
}
