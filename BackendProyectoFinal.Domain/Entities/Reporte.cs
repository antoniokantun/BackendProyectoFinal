
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Domain.Entities
{
    [Table("reportes")]
    public class Reporte
    {
        [Key]
        [Column("id_reporte")]
        public int IdReporte { get; set; }

        [Required]
        [Column("motivo_reporte")]
        [MaxLength(250)]
        public string MotivoReporte { get; set; }

        [Required]
        [Column("fecha_reporte")]
        public DateTime FechaReporte { get; set; } = DateTime.UtcNow;


        // Propiedades de navegación para relaciones
        public virtual ICollection<UsuarioReporte> UsuarioReportes { get; set; } = new List<UsuarioReporte>();
        public virtual ICollection<ProductoReporte> ProductoReportes { get; set; } = new List<ProductoReporte>();
    }
}
