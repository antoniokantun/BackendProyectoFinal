using BackendProyectoFinal.Application.DTOs;
using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Domain.Interfaces;
using BackendProyectoFinal.Domain.Interfaces.IServices;


namespace BackendProyectoFinal.Application.Services
{
    public class ErrorHandlingService : IErrorHandlingService
    {
        private readonly ILogErrorRepository _logErrorRepository;

        public ErrorHandlingService(ILogErrorRepository logErrorRepository)
        {
            _logErrorRepository = logErrorRepository;
        }

        public async Task<LogErrorDTO> LogErrorAsync(Exception ex, string origen)
        {
            var logError = new LogError
            {
                Mensaje = ex.Message,
                FechaOcurrencia = DateTime.UtcNow,
                Origen = origen
            };

            var createdLog = await _logErrorRepository.AddAsync(logError);
            return MapToDto(createdLog);
        }

        public async Task<LogErrorDTO> LogCustomErrorAsync(string mensaje, string origen)
        {
            var logError = new LogError
            {
                Mensaje = mensaje,
                FechaOcurrencia = DateTime.UtcNow,
                Origen = origen
            };

            var createdLog = await _logErrorRepository.AddAsync(logError);
            return MapToDto(createdLog);
        }

        public async Task<IEnumerable<LogErrorDTO>> GetAllLogsAsync()
        {
            var logs = await _logErrorRepository.GetAllAsync();
            return logs.Select(MapToDto);
        }

        private LogErrorDTO MapToDto(LogError logError)
        {
            return new LogErrorDTO
            {
                IdLogError = logError.IdLogError,
                Mensaje = logError.Mensaje,
                FechaOcurrencia = logError.FechaOcurrencia,
                Origen = logError.Origen
            };
        }
    }
}
