using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.DTOs
{
    public class ProductPatchDTO
    {
        public int IdProducto { get; set; } 
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool? Intercambio { get; set; }
        public List<ImagenDTO>? Imagenes { get; set; }
        public List<int> CategoriasIds { get; set; } = new List<int>();

    }
}
