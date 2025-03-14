using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Domain.Entities
{
    [Table("perfiles")]
    public class Perfil
    {
        [Key]
        [Column("id_perfil")]
        public int IdPerfil { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        [Column("imagen_perfil")]
        public string? ImagenPerfil { get; set; }

        [Column("nombre_perfil")]
        [MaxLength(100)]
        public string NombrePerfil { get; set; } = string.Empty;

        [Column("descripcion")]
        [MaxLength(255)]
        public string? Descripcion { get; set; }
    }
}
