using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("Imagen")]
        [Column("imagen_id")]
        public int ImagenId { get; set; } 
        public Imagenes Imagen { get; set; } = null!; 
    }
}