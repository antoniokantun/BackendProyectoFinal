using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Entities
{
    [Table("refresh_tokens")]
    public class RefreshToken
    {
        [Key]
        [Column("id_token")]
        public int IdToken { get; set; }

        [Required]
        [Column("token")]
        public string Token { get; set; } = string.Empty;

        [Required]
        [Column("fecha_expiracion")]
        public DateTime FechaExpiracion { get; set; }

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }

        [Required]
        [Column("esta_activo")]
        public bool EstaActivo { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
    }
}
