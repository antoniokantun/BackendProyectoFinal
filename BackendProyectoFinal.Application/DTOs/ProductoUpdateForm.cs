using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BackendProyectoFinal.Application.DTOs
{
    public class ProductoUpdateForm
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool? Intercambio { get; set; }
        public List<int> CategoriasIds { get; set; } = new List<int>();
        public List<IFormFile>? Imagenes { get; set; }
    }
}