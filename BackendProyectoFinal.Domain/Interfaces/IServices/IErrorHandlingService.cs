using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Interfaces.IServices
{
    public interface IErrorHandlingService
    {
        Task LogErrorAsync(Exception ex, string origen, string severidad = "Error", int? usuarioId = null);
        Task LogErrorAsync(string mensaje, string origen, string severidad = "Error", int? usuarioId = null);
    }
}
