
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BackendProyectoFinal.Domain.Entities
{
    [Table("roles")]
    public class Rol
    {
        [Key]
        [Column("id_rol")]
        public int IdRol { get; set; }

        [Required]
        [Column("nombre_rol")]
        [MaxLength(100)]
        public string NombreRol { get; set; } = string.Empty;

    }
}
