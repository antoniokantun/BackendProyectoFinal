using BackendProyectoFinal.Domain.Entities;
using BackendProyectoFinal.Infrastructure.Seeders;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DatabaseSeeder.Seed(modelBuilder);

            modelBuilder.Entity<ImagenProducto>()
                .HasOne(ip => ip.Imagen)
                .WithMany()
                .HasForeignKey(ip => ip.ImagenId)
                .OnDelete(DeleteBehavior.Cascade); 

        }
    }
}
