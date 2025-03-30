
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BackendProyectoFinal.Domain.Entities
{
    [Table("usuario_reporte")]
    public class UsuarioReporte
    {
        [Key]
        [Column("id_usuario_reporte")]
        public int IdUsuarioReporte { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        [Required]
        [ForeignKey("Reporte")]
        [Column("reporte_id")]
        public int ReporteId { get; set; }
        public Reporte Reporte { get; set; } = null!;
    }
}
