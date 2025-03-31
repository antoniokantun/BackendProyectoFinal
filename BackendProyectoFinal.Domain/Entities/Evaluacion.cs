
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BackendProyectoFinal.Domain.Entities
{
    [Table("evaluaciones")]
    public class Evaluacion
    {
        [Key]
        [Column("id_evaluacion")]
        public int IdEvaluacion { get; set; }

        [Required]
        [Column("titulo_evaluacion")]
        [MaxLength(100)]
        public string TituloEvaluacion { get; set; } = null!;

        [Required]
        [ForeignKey("Usuario")]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        [Required]
        [ForeignKey("UsuarioEvaluador")]
        [Column("usuario_evaluador_id")]
        public int UsuarioEvaluadorId { get; set; }
        public Usuario UsuarioEvaluador { get; set; } = null!;

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

        [Column("completado")]
        public bool Completado { get; set; } = false;
    }
}