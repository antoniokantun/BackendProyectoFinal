
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackendProyectoFinal.Domain.Entities
{
    [Table("intercambios")]
    public class Intercambio
    {
        [Key]
        [Column("id_intercambio")]
        public int IdIntercambio { get; set; }

        [Required]
        [ForeignKey("UsuarioSolicitante")]
        [Column("usuario_solicitante_id")]
        public int UsuarioSolicitanteId { get; set; }
        public Usuario UsuarioSolicitante { get; set; } = null!;

        [Required]
        [ForeignKey("UsuarioOfertante")]
        [Column("usuario_ofertante_id")]
        public int UsuarioOfertanteId { get; set; }
        public Usuario UsuarioOfertante { get; set; } = null!;

        [Required]
        [ForeignKey("Producto")]
        [Column("producto_id")]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;

        [Required]
        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; }

        [ForeignKey("Estado")]
        [Column("estado_id")]
        public int? EstadoId { get; set; }
        public Estado Estado { get; set; } = null!;
    }
}
