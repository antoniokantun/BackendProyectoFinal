﻿

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BackendProyectoFinal.Domain.Entities
{
    [Table("productos")]
    public class Producto
    {
        [Key]
        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Required]
        [Column("nombre_producto")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Column("descripcion_producto")]
        [MaxLength(100)]
        public string? Descripcion { get; set; }

        [Required]
        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }

        [Required]
        [Column("proceso_negociacion")]
        public bool ProcesoNegociacion { get; set; }

        [Required]
        [Column("intercambio")]
        public bool Intercambio { get; set; }

        [Required]
        [Column("no_visible")]
        public bool NoVisible { get; set; } = false;

        [Required]
        [Column("reportado")]
        public bool Reportado { get; set; } = false;

        [Required]
        [ForeignKey("Usuario")]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        // Propiedades de navegación para las relaciones
        public virtual ICollection<CategoriaProducto> CategoriaProductos { get; set; } = new List<CategoriaProducto>();
        public virtual ICollection<ImagenProducto> ImagenProductos { get; set; } = new List<ImagenProducto>();
    }
}