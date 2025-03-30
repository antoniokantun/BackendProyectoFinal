
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BackendProyectoFinal.Domain.Entities
{
    [Table("estados")]
    public class Estado
    {
        [Key]
        [Column("id_estado")]
        public int IdEstado { get; set; }

        [Required]
        [Column("nombre")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        // Relación con Intercambio (uno a muchos)
        public ICollection<Intercambio> Intercambios { get; set; } = new List<Intercambio>();
    }
}
