using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Entities
{
    [Table("categorias_producto")]
    public class CategoriaProducto
    {
        [Key]
        [Column("id_categoria_producto")]
        public int IdCategoriaProducto { get; set; } // Se puede omitir si solo usas la clave compuesta

        [Required]
        [ForeignKey("Categoria")]
        [Column("categoria_id")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;

        [Required]
        [ForeignKey("Producto")]
        [Column("producto_id")]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;
    }
}
