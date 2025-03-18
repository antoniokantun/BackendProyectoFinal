using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.DTOs
{
    public class PerfilDTO
    {
        public int IdPerfil { get; set; }
        public int UsuarioId { get; set; }
        public string? ImagenPerfil { get; set; }
        public string NombrePerfil { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }

    public class CreatePerfilDTO
    {
        [Required]
        public int UsuarioId { get; set; }
        public string? ImagenPerfil { get; set; }
        [Required]
        [MaxLength(100)]
        public string NombrePerfil { get; set; } = string.Empty;
        [MaxLength(255)]
        public string? Descripcion { get; set; }
    }

    public class UpdatePerfilDTO
    {
        public string? ImagenPerfil { get; set; }
        [MaxLength(100)]
        public string NombrePerfil { get; set; } = string.Empty;
        [MaxLength(255)]
        public string? Descripcion { get; set; }
    }
}
