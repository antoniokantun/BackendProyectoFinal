using BackendProyectoFinal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendProyectoFinal.Infrastructure.Persistence.Data
{
    public static class ApplicationDbContextSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Seed Roles
            modelBuilder.Entity<Rol>().HasData(
                new Rol { IdRol = 1, NombreRol = "Administrador" },
                new Rol { IdRol = 2, NombreRol = "Usuario" }
            );

            // Seed Usuarios (con contraseñas hasheadas como ejemplo)
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    IdUsuario = 1,
                    Nombre = "Admin",
                    Apellido = "Sistema",
                    CorreoElectronico = "admin@sistema.com",
                    Telefono = "123456789",
                    Contrasenia = "AQAAAAEAACcQAAAAEJGuO48K7z1Px5f3VTQE9YiuO0xi4A5WyHIjWc2wNpKPMxl35dyz0gEcdGDwgm9kzA==", // Contraseña hasheada (ejemplo)
                    FechaRegistro = new DateTime(2025, 1, 1),
                    RolId = 1
                },
                new Usuario
                {
                    IdUsuario = 2,
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    CorreoElectronico = "juan@example.com",
                    Telefono = "987654321",
                    Contrasenia = "AQAAAAEAACcQAAAAEDy7P2HKHRAcZpScUPiVTBtLGFpG2XMfqhMHQUYP9l7HNmtfwXwfNnHUMpJ4G7VVAA==", // Contraseña hasheada (ejemplo)
                    FechaRegistro = new DateTime(2025, 1, 2),
                    RolId = 2
                }
            );

            // Seed Perfiles
            modelBuilder.Entity<Perfil>().HasData(
                new Perfil
                {
                    IdPerfil = 1,
                    UsuarioId = 1,
                    ImagenPerfil = "admin_profile.jpg",
                    NombrePerfil = "Administrador",
                    Descripcion = "Perfil administrador del sistema"
                },
                new Perfil
                {
                    IdPerfil = 2,
                    UsuarioId = 2,
                    ImagenPerfil = "juan_profile.jpg",
                    NombrePerfil = "JuanP",
                    Descripcion = "Me encanta intercambiar productos"
                }
            );

            // Seed Categorias
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { IdCategoria = 1, Nombre = "Electrónica" },
                new Categoria { IdCategoria = 2, Nombre = "Hogar" }
            );

            // Seed Productos
            modelBuilder.Entity<Producto>().HasData(
                new Producto
                {
                    IdProducto = 1,
                    Nombre = "Smartphone",
                    Descripcion = "Teléfono inteligente en buen estado",
                    FechaCreacion = new DateTime(2025, 2, 15),
                    ProcesoNegociacion = true,
                    Intercambio = true,
                    UsuarioId = 2
                },
                new Producto
                {
                    IdProducto = 2,
                    Nombre = "Mesa de café",
                    Descripcion = "Mesa de madera para sala",
                    FechaCreacion = new DateTime(2025, 2, 20),
                    ProcesoNegociacion = false,
                    Intercambio = true,
                    UsuarioId = 2
                }
            );

            // Seed CategoriaProducto
            modelBuilder.Entity<CategoriaProducto>().HasData(
                new CategoriaProducto
                {
                    IdCategoriaProducto = 1,
                    CategoriaId = 1,
                    ProductoId = 2
                },
                new CategoriaProducto
                {
                    IdCategoriaProducto = 2,
                    CategoriaId = 2,
                    ProductoId = 2
                }
            );

            // Seed Imagenes
            modelBuilder.Entity<Imagen>().HasData(
                new Imagen
                {
                    IdImagen = 1,
                    UrlImagen = "smartphone_1.jpg"
                },
                new Imagen
                {
                    IdImagen = 2,
                    UrlImagen = "mesa_cafe_1.jpg"
                }
            );

            // Seed ImagenProducto
            modelBuilder.Entity<ImagenProducto>().HasData(
                new ImagenProducto
                {
                    IdImagenProducto = 1,
                    ProductoId = 1,
                    ImagenId = 1
                },
                new ImagenProducto
                {
                    IdImagenProducto = 2,
                    ProductoId = 2,
                    ImagenId = 2
                }
            );

            // Seed Comentarios
            modelBuilder.Entity<Comentario>().HasData(
                new Comentario
                {
                    IdComentario = 1,
                    Contenido = "¿Está disponible para intercambio?",
                    UsuarioId = 1,
                    ProductoId = 1,
                    FechaCreacion = new DateTime(2025, 2, 16),
                    ComentarioPadreId = null
                },
                new Comentario
                {
                    IdComentario = 2,
                    Contenido = "Sí, disponible para intercambio",
                    UsuarioId = 2,
                    ProductoId = 1,
                    FechaCreacion = new DateTime(2025, 2, 17),
                    ComentarioPadreId = 1
                }
            );

            // Seed Evaluaciones
            modelBuilder.Entity<Evaluacion>().HasData(
                new Evaluacion
                {
                    IdEvaluacion = 1,
                    UsuarioId = 1,
                    ProductoId = 2,
                    FechaCreacion = new DateTime(2025, 2, 25),
                    Comentario = "Producto en excelente estado",
                    Puntuacion = 5
                },
                new Evaluacion
                {
                    IdEvaluacion = 2,
                    UsuarioId = 2,
                    ProductoId = 1,
                    FechaCreacion = new DateTime(2025, 2, 26),
                    Comentario = "Buena calidad",
                    Puntuacion = 4
                }
            );

            // Seed Intercambios
            modelBuilder.Entity<Intercambio>().HasData(
                new Intercambio
                {
                    IdIntercambio = 1,
                    UsuarioSolicitanteId = 1,
                    UsuarioOfertanteId = 2,
                    ProductoId = 1,
                    FechaRegistro = new DateTime(2025, 3, 1)
                },
                new Intercambio
                {
                    IdIntercambio = 2,
                    UsuarioSolicitanteId = 2,
                    UsuarioOfertanteId = 1,
                    ProductoId = 2,
                    FechaRegistro = new DateTime(2025, 3, 5)
                }
            );

            // Seed LogErrores
            modelBuilder.Entity<LogError>().HasData(
                new LogError
                {
                    IdLogError = 1,
                    Mensaje = "Error al procesar imagen",
                    FechaOcurrencia = new DateTime(2025, 3, 10),
                    Origen = "ImagenService.ProcessImage",
                    Severidad = "Error",
                    StackTrace = "System.IO.IOException: Error al procesar imagen...",
                    UsuarioId = 2
                },
                new LogError
                {
                    IdLogError = 2,
                    Mensaje = "Error de autenticación",
                    FechaOcurrencia = new DateTime(2025, 3, 11),
                    Origen = "AuthService.Authenticate",
                    Severidad = "Advertencia",
                    StackTrace = "System.UnauthorizedAccessException: Error de autenticación...",
                    UsuarioId = null
                }
            );
        }
    }
}
