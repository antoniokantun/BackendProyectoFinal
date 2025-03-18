using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.DTOs
{
    public class ComentarioDTO
    {
        public int IdComentario { get; set; }
        public string Contenido { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty; // Nombre del usuario para mostrar
        public int ProductoId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? ComentarioPadreId { get; set; }
        public List<ComentarioDTO> ComentariosHijos { get; set; } = new List<ComentarioDTO>();
    }

    public class CreateComentarioDTO
    {
        [Required]
        [MaxLength(100)]
        public string Contenido { get; set; } = string.Empty;

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int ProductoId { get; set; }

        public int? ComentarioPadreId { get; set; }
    }

    public class UpdateComentarioDTO
    {
        [Required]
        [MaxLength(100)]
        public string Contenido { get; set; } = string.Empty;
    }
}
