using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Entities
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("nombre_usuario")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column("apellido_usuario")]
        [MaxLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [Column("correo_electronico")]
        [EmailAddress]
        public string CorreoElectronico { get; set; } = string.Empty;

        [Column("telefono")]
        [MaxLength(50)]
        public string? Telefono { get; set; }

        [Required]
        [Column("contrasenia")]
        [MaxLength(255)]
        public string Contrasenia { get; set; } = string.Empty;

        [Required]
        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; }

        [Required]
        [Column("baneado")]

        public bool Baneado {  get; set; }

        [Required]
        [ForeignKey("Rol")]
        [Column("rol_id")]
        public int RolId { get; set; } 
        public Rol Rol { get; set; } = null!; 
    }
}
