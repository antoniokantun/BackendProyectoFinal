using BackendProyectoFinal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CategoriaProducto> CategoriasProducto { get; set; }
        public DbSet<ImagenProducto> ImagenesProducto { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Intercambio> Intercambios { get; set; }
        public DbSet<LogError> LogErrores { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
    }
}
