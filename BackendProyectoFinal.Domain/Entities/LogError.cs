 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Entities
{
    [Table("log_errores")]
    public class LogError
    {
        [Key]
        [Column("id_log_error")]
        public int IdLogError { get; set; }

        [Required]
        [Column("mensaje_error")]
        [MaxLength(500)]
        public string Mensaje { get; set; } = string.Empty;

        [Required]
        [Column("fecha_ocurrencia")]
        public DateTime FechaOcurrencia { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("origen_error")]
        [MaxLength(200)]
        public string Origen { get; set; } = string.Empty; // Puede ser el nombre de la clase o método donde ocurrió el error

        [Required]
        [Column("severidad")]
        [MaxLength(50)]
        public string Severidad { get; set; } = "Error"; // Puede ser Info, Advertencia, Error, Crítico

        [Column("stack_trace")]
        public string? StackTrace { get; set; } // Traza del error para depuración

        [Column("usuario_id")]
        public int? UsuarioId { get; set; } // Si el error está relacionado con un usuario

        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; } // Relación con la entidad Usuario
    }
}
