using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.DTOs
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool ProcesoNegociacion { get; set; }
        public bool Intercambio { get; set; }
        public int UsuarioId { get; set; }
        // Puedes incluir información del usuario si es necesario
        // public string NombreUsuario { get; set; }
    }

    public class CreateProductoDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool ProcesoNegociacion { get; set; }
        public bool Intercambio { get; set; }
        public int UsuarioId { get; set; }
    }

    public class UpdateProductoDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool ProcesoNegociacion { get; set; }
        public bool Intercambio { get; set; }
    }
}
