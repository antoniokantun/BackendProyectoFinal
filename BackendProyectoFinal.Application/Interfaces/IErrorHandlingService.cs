using BackendProyectoFinal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces.IServices
{
    public interface IErrorHandlingService
    {
        Task<LogErrorDTO> LogErrorAsync(Exception ex, string origen);
        Task<LogErrorDTO> LogCustomErrorAsync(string mensaje, string origen);
        Task<IEnumerable<LogErrorDTO>> GetAllLogsAsync();
    }
}
