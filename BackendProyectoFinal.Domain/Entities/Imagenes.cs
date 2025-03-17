using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProyectoFinal.Domain.Entities
{
    [Table("imagenes")]
    public class Imagenes
    {
        [Key]
        [Column("id_imagen")]
        public int IdImagen { get; set; } 

        [Required]
        [Column("url_image")]
        [MaxLength(500)] 
        public string UrlImagen { get; set; } = string.Empty; 
    }
}