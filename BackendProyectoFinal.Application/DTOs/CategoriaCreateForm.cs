using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.DTOs
{
    public class CategoriaCreateForm
    {
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La imagen de la categoría es obligatoria")]
        public IFormFile Imagen { get; set; }
    }

    // DTO para la edición de categorías con opción de cambiar la imagen
    public class CategoriaEditForm
    {
        [Required(ErrorMessage = "El ID de la categoría es obligatorio")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        // Imagen opcional para la edición (si no se proporciona, se mantiene la actual)
        public IFormFile? NuevaImagen { get; set; }
    }

}
