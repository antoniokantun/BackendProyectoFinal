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
        [Column("origen")]
        [MaxLength(50)]
        public string Origen { get; set; } = string.Empty; // "Backend" o "Frontend"
    }
}
