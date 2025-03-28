
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BackendProyectoFinal.Domain.Entities
{
    [Table("categorias")]
    public class Categoria
    {
        [Key]
        [Column("id_categoria")]
        public int IdCategoria { get; set; }

        [Required]
        [Column("nombre_categoria")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column("imagen_categoria")]
        [MaxLength(300)]
        public string ImagenCategoria { get; set; }
    }
}
