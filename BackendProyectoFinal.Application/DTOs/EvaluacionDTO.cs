﻿using System;
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
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public string? Comentario { get; set; }
        public int Puntuacion { get; set; }
    }

    public class CreateEvaluacionDTO
    {
        [Required]
        public int UsuarioId { get; set; }

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
        [MaxLength(150)]
        public string? Comentario { get; set; }

        [Required]
        [Range(1, 5)]
        public int Puntuacion { get; set; }
    }
}
