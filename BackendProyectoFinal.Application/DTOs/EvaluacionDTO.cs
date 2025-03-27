using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Application.DTOs
{
    public class EvaluacionDTO
    {
        public int IdEvaluacion { get; set; }
        public string TituloEvaluacion { get; set; } = null!;
        public int UsuarioId { get; set; }
        public int UsuarioEvaluadorId { get; set; }
        public int ProductoId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? Comentario { get; set; }
        public int Puntuacion { get; set; }

        // Propiedades adicionales para mostrar información relacionada
        public string? NombreUsuario { get; set; }
        public string? NombreUsuarioEvaluador { get; set; }
        public string? NombreProducto { get; set; }
    }

    public class CreateEvaluacionDTO
    {
        [Required]
        [MaxLength(100)]
        public string TituloEvaluacion { get; set; } = null!;

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int UsuarioEvaluadorId { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [MaxLength(150)]
        public string? Comentario { get; set; }

        [Required]
        [Range(1, 5)]
        public int Puntuacion { get; set; }
    }

    public class UpdateEvaluacionDTO
    {
        [Required]
        [MaxLength(100)]
        public string TituloEvaluacion { get; set; } = null!;

        [MaxLength(150)]
        public string? Comentario { get; set; }

        [Required]
        [Range(1, 5)]
        public int Puntuacion { get; set; }
    }
}