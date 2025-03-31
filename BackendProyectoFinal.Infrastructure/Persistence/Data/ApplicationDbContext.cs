using BackendProyectoFinal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Infrastructure.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CategoriaProducto> CategoriasProducto { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<ImagenProducto> ImagenesProducto { get; set; }
        public DbSet<Intercambio> Intercambios { get; set; }
        public DbSet<LogError> LogErrores { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
        public DbSet<UsuarioReporte> UsuarioReportes { get; set; }
        public DbSet<ProductoReporte> ProductoReportes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relaciones y restricciones
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.CorreoElectronico)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany()
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.Usuario)
                .WithMany()
                .HasForeignKey(rt => rt.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);


            // Configurar relaciones para intercambios
            modelBuilder.Entity<Intercambio>()
                .HasOne(i => i.UsuarioSolicitante)
                .WithMany()
                .HasForeignKey(i => i.UsuarioSolicitanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Intercambio>()
                .HasOne(i => i.UsuarioOfertante)
                .WithMany()
                .HasForeignKey(i => i.UsuarioOfertanteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Intercambio>()
                .HasOne(i => i.Estado)
                .WithMany(e => e.Intercambios)
                .HasForeignKey(i => i.EstadoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar relación para productos
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // CORRECCIÓN: Configurar relación para categorías de producto con navegación bidireccional
            modelBuilder.Entity<CategoriaProducto>()
                .HasOne(cp => cp.Categoria)
                .WithMany()
                .HasForeignKey(cp => cp.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoriaProducto>()
                .HasOne(cp => cp.Producto)
                .WithMany(p => p.CategoriaProductos) // Especificar la colección en Producto
                .HasForeignKey(cp => cp.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);

            // CORRECCIÓN: Configurar relación para imágenes de producto con navegación bidireccional
            modelBuilder.Entity<ImagenProducto>()
                .HasOne(ip => ip.Imagen)
                .WithMany()
                .HasForeignKey(ip => ip.ImagenId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ImagenProducto>()
                .HasOne(ip => ip.Producto)
                .WithMany(p => p.ImagenProductos) // Especificar la colección en Producto
                .HasForeignKey(ip => ip.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relación para evaluaciones
            modelBuilder.Entity<Evaluacion>()
                .HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Evaluacion>()
                .HasOne(e => e.Producto)
                .WithMany()
                .HasForeignKey(e => e.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);


            // Configurar relación para perfiles
            modelBuilder.Entity<Perfil>()
                .HasOne(p => p.Usuario)
                .WithOne()
                .HasForeignKey<Perfil>(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de Reporte
            modelBuilder.Entity<Reporte>()
                .HasMany(r => r.UsuarioReportes)
                .WithOne(ur => ur.Reporte)
                .HasForeignKey(ur => ur.ReporteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reporte>()
                .HasMany(r => r.ProductoReportes)
                .WithOne(pr => pr.Reporte)
                .HasForeignKey(pr => pr.ReporteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Agregar datos semilla (seed data)
            ApplicationDbContextSeeder.Seed(modelBuilder);
        }
    }
}