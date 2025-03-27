using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.DTOs
{
    public class ProductoCreateForm
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool ProcesoNegociacion { get; set; }
        public bool Intercambio { get; set; }
        public bool Visible { get; set; }

        public int UsuarioId { get; set; }
        public List<int> CategoriasIds { get; set; } = new List<int>();
        public List<IFormFile>? Imagenes { get; set; }
    }
}
