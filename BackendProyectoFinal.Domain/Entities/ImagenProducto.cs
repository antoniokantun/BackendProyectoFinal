using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Entities
{
    [Table("imagenes_producto")]
    public class ImagenProducto
    {
        [Key]
        [Column("id_imagen_producto")]
        public int IdImagenProducto { get; set; }

        [Required]
        [ForeignKey("Producto")]
        [Column("producto_id")]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;

        [Required]
        [Column("url_image")]
        public string UrlImagen { get; set; } = string.Empty;
    }
}
