using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.DTOs
{
    public class ProductoDetailDTO
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public bool ProcesoNegociacion { get; set; }
        public bool Intercambio { get; set; }
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;

        public List<ImagenDTO> Imagenes { get; set; } = new List<ImagenDTO>();
        public List<CategoriaDTO> Categorias { get; set; } = new List<CategoriaDTO>();
    }
}
