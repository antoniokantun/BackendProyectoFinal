using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.DTOs
{
    public class EstadoDTO
    {
        public int IdEstado { get; set; }
        public string Nombre { get; set; } = null!;
    }

    public class CreateEstadoDTO
    {
        public string Nombre { get; set; } = null!;
    }

    public class UpdateEstadoDTO
    {
        public string Nombre { get; set; } = null!;
    }
}
