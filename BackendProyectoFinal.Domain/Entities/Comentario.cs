using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Entities
{
    [Table("comentarios")]
    public class Comentario
    {
        [Key]
        [Column("id_comentario")]
        public int IdComentario { get; set; }

        [Required]
        [Column("contenido_comentario")]
        [MaxLength(100)]
        public string Contenido { get; set; } = string.Empty;

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

        [ForeignKey("ComentarioPadre")]
        [Column("comentario_padre_id")]
        public int? ComentarioPadreId { get; set; }
        public Comentario? ComentarioPadre { get; set; }
    }
}
