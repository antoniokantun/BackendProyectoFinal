using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.DTOs
{
    public class ProductoEditForm
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool ProcesoNegociacion { get; set; }
        public bool Intercambio { get; set; }
        public bool NoVisible { get; set; }

        public int UsuarioId { get; set; }

        // IDs de las categorías seleccionadas
        public List<int> CategoriasIds { get; set; } = new List<int>();

        // IDs de las imágenes que se desean mantener
        public List<int> ImagenesExistentesIds { get; set; } = new List<int>();

        // Nuevas imágenes a agregar
        public List<IFormFile>? NuevasImagenes { get; set; }
    }
}
