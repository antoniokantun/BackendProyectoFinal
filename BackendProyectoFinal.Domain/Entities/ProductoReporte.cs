
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BackendProyectoFinal.Domain.Entities
{
    [Table("producto_reporte")]
    public class ProductoReporte
    {
        [Key]
        [Column("id_producto_reporte")]
        public int IdProductoReporte { get; set; }

        [Required]
        [ForeignKey("Producto")]
        [Column("producto_id")]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;

        [Required]
        [ForeignKey("Reporte")]
        [Column("reporte_id")]
        public int ReporteId { get; set; }
        public Reporte Reporte { get; set; } = null!;
    }
}
