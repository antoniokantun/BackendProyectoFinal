using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Entities
{
    [Table("evaluaciones")]
    public class Evaluacion
    {
        [Key]
        [Column("id_evaluacion")]
        public int IdEvaluacion { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        [Required]
        [ForeignKey("Producto")]
        [Column("producto_id")]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;

        [Required]
        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }

        [Column("comentario")]
        [MaxLength(150)]
        public string? Comentario { get; set; }

        [Required]
        [Column("puntacion")]
        [Range(1, 5)]
        public int Puntuacion { get; set; }
    }
}
