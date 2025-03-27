using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.DTOs
{
    public class ProductoCreateDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool ProcesoNegociacion { get; set; }
        public bool Intercambio { get; set; }
        public bool Visible { get; set; }

        public int UsuarioId { get; set; }

        // Lista de URLs de imágenes para crear
        public List<string> ImagenesUrl { get; set; } = new List<string>();

        // Lista de IDs de categorías para relacionar
        public List<int> CategoriasIds { get; set; } = new List<int>();
    }
}
