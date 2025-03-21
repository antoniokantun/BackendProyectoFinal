﻿using BackendProyectoFinal.Domain.Entities;
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
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Intercambio> Intercambios { get; set; }
        public DbSet<LogError> LogErrores { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

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

            // Configurar relación autoreferencial para comentarios
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.ComentarioPadre)
                .WithMany()
                .HasForeignKey(c => c.ComentarioPadreId)
                .OnDelete(DeleteBehavior.Restrict); // Evita borrados en cascada circular

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

            // Configurar relación para productos
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relación para categorías de producto
            modelBuilder.Entity<CategoriaProducto>()
                .HasOne(cp => cp.Categoria)
                .WithMany()
                .HasForeignKey(cp => cp.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoriaProducto>()
                .HasOne(cp => cp.Producto)
                .WithMany()
                .HasForeignKey(cp => cp.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relación para imágenes de producto
            modelBuilder.Entity<ImagenProducto>()
                .HasOne(ip => ip.Producto)
                .WithMany()
                .HasForeignKey(ip => ip.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ImagenProducto>()
                .HasOne(ip => ip.Imagen)
                .WithMany()
                .HasForeignKey(ip => ip.ImagenId)
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

            // Configurar LogError para que la relación sea opcional
            modelBuilder.Entity<LogError>()
                .HasOne(l => l.Usuario)
                .WithMany()
                .HasForeignKey(l => l.UsuarioId)
                .IsRequired(false);

            // Configurar relación para perfiles
            modelBuilder.Entity<Perfil>()
                .HasOne(p => p.Usuario)
                .WithOne()
                .HasForeignKey<Perfil>(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relación para comentarios
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Producto)
                .WithMany()
                .HasForeignKey(c => c.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Agregar datos semilla (seed data)
            ApplicationDbContextSeeder.Seed(modelBuilder);
        }
    }
}
