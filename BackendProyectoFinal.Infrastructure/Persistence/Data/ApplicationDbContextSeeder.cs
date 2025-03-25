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
                    Contrasenia = "AQAAAAEAACcQAAAAEJGuO48K7z1Px5f3VTQE9YiuO0xi4A5WyHIjWc2wNpKPMxl35dyz0gEcdGDwgm9kzA==",
                    FechaRegistro = new DateTime(2025, 1, 1),
                    RolId = 1,
                    Baneado = false // Valor por defecto
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
                    RolId = 2,
                    Baneado = false // Valor por defecto
                },
                new Usuario
                {
                    IdUsuario = 3,
                    Nombre = "María",
                    Apellido = "Gómez",
                    CorreoElectronico = "maria@example.com",
                    Telefono = "5551234567",
                    Contrasenia = "AQAAAAEAACcQAAAAEFCxR9j/L3Wd7nC0p/W4aLgYg1zMv8zKjEaXbZ9pQoR/sT2uBvC7wYnZqN8u9yFAA==", // Contraseña hasheada (ejemplo)
                    FechaRegistro = new DateTime(2025, 1, 3),
                    RolId = 2,
                    Baneado = false // Valor por defecto
                },
                new Usuario
                {
                    IdUsuario = 4,
                    Nombre = "Carlos",
                    Apellido = "López",
                    CorreoElectronico = "carlos@example.com",
                    Telefono = "1119876543",
                    Contrasenia = "AQAAAAEAACcQAAAAEL+0iJ9pS/RkYvX/Z8bTqU2wN1oPz7uIe/jKdLsMvX9bZcRjWnFvQxUeYvA2b7AAA==", // Contraseña hasheada (ejemplo)
                    FechaRegistro = new DateTime(2025, 1, 4),
                    RolId = 2,
                    Baneado = false // Valor por defecto
                }
            );

            // Seed Perfiles
            modelBuilder.Entity<Perfil>().HasData(
                new Perfil
                {
                    IdPerfil = 1,
                    UsuarioId = 1,
                    ImagenPerfil = "https://images.unsplash.com/photo-1543610892-0b1f7e6d8ac1?q=80&w=1856&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    NombrePerfil = "Administrador",
                    Descripcion = "Perfil administrador del sistema"
                },
                new Perfil
                {
                    IdPerfil = 2,
                    UsuarioId = 2,
                    ImagenPerfil = "https://images.unsplash.com/photo-1489980557514-251d61e3eeb6?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    NombrePerfil = "JuanP",
                    Descripcion = "Me encanta intercambiar productos"
                },
                new Perfil
                {
                    IdPerfil = 3,
                    UsuarioId = 3,
                    ImagenPerfil = "https://images.unsplash.com/photo-1577565177023-d0f29c354b69?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    NombrePerfil = "MariaG",
                    Descripcion = "Busco ofertas interesantes para mi hogar"
                },
                new Perfil
                {
                    IdPerfil = 4,
                    UsuarioId = 4,
                    ImagenPerfil = "https://images.unsplash.com/photo-1558203728-00f45181dd84?q=80&w=2074&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    NombrePerfil = "CarlosL",
                    Descripcion = "Apasionado por la tecnología y los gadgets"
                }
            );

            // Seed Categorias
            modelBuilder.Entity<Categoria>().HasData(
            new Categoria { IdCategoria = 1, Nombre = "Electrónica", ImagenCategoria = "https://images.unsplash.com/photo-1550009158-9ebf69173e03?q=80&w=1201&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
            new Categoria { IdCategoria = 2, Nombre = "Hogar", ImagenCategoria = "https://images.unsplash.com/photo-1618220179428-22790b461013?q=80&w=1227&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
            new Categoria { IdCategoria = 3, Nombre = "Ropa", ImagenCategoria = "https://images.unsplash.com/photo-1523381210434-271e8be1f52b?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
            new Categoria { IdCategoria = 4, Nombre = "Libros", ImagenCategoria = "https://images.unsplash.com/photo-1495446815901-a7297e633e8d?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
            new Categoria { IdCategoria = 5, Nombre = "Deportes", ImagenCategoria = "https://images.unsplash.com/flagged/photo-1556746834-cbb4a38ee593?q=80&w=1172&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
            new Categoria { IdCategoria = 6, Nombre = "Juguetes", ImagenCategoria = "https://images.unsplash.com/photo-1587654780291-39c9404d746b?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
            new Categoria { IdCategoria = 7, Nombre = "Muebles", ImagenCategoria = "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
            new Categoria { IdCategoria = 8, Nombre = "Herramientas", ImagenCategoria = "https://images.unsplash.com/photo-1581783898377-1c85bf937427?q=80&w=1015&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" }
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
                },
                new Producto
                {
                    IdProducto = 3,
                    Nombre = "Libro de cocina",
                    Descripcion = "Recetas fáciles y deliciosas",
                    FechaCreacion = new DateTime(2025, 2, 25),
                    ProcesoNegociacion = false,
                    Intercambio = true,
                    UsuarioId = 3
                },
                new Producto
                {
                    IdProducto = 4,
                    Nombre = "Bicicleta de montaña",
                    Descripcion = "Ideal para aventuras al aire libre",
                    FechaCreacion = new DateTime(2025, 3, 1),
                    ProcesoNegociacion = true,
                    Intercambio = true,
                    UsuarioId = 4
                },
                new Producto
                {
                    IdProducto = 5,
                    Nombre = "Juego de mesa",
                    Descripcion = "Para disfrutar en familia o con amigos",
                    FechaCreacion = new DateTime(2025, 3, 5),
                    ProcesoNegociacion = false,
                    Intercambio = true,
                    UsuarioId = 3
                }
            );

            // Seed CategoriaProducto
            modelBuilder.Entity<CategoriaProducto>().HasData(
                new CategoriaProducto
                {
                    IdCategoriaProducto = 1,
                    CategoriaId = 1, // Electrónica
                    ProductoId = 1  // Smartphone
                },
                new CategoriaProducto
                {
                    IdCategoriaProducto = 2,
                    CategoriaId = 2, // Hogar
                    ProductoId = 2  // Mesa de café
                },
                new CategoriaProducto
                {
                    IdCategoriaProducto = 3,
                    CategoriaId = 2, // Hogar
                    ProductoId = 3  // Libro de cocina
                },
                new CategoriaProducto
                {
                    IdCategoriaProducto = 4,
                    CategoriaId = 4, // Libros
                    ProductoId = 3  // Libro de cocina
                },
                new CategoriaProducto
                {
                    IdCategoriaProducto = 5,
                    CategoriaId = 5, // Deportes
                    ProductoId = 4  // Bicicleta de montaña
                },
                new CategoriaProducto
                {
                    IdCategoriaProducto = 6,
                    CategoriaId = 6, // Juguetes
                    ProductoId = 5  // Juego de mesa
                },
                new CategoriaProducto
                {
                    IdCategoriaProducto = 7,
                    CategoriaId = 2, // Hogar
                    ProductoId = 5  
                },
                new CategoriaProducto
                {
                    IdCategoriaProducto = 8,
                    CategoriaId = 1, // Electrónica
                    ProductoId = 1  // Smartphone
                },
                new CategoriaProducto
                {
                    IdCategoriaProducto = 9,
                    CategoriaId = 7, // Muebles
                    ProductoId = 2  
                }
            );

            // Seed Imagenes
            modelBuilder.Entity<Imagen>().HasData(
                new Imagen
                {
                    IdImagen = 1,
                    UrlImagen = "https://plus.unsplash.com/premium_photo-1664201890402-6886a989796f?q=80&w=987&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Imagen
                {
                    IdImagen = 2,
                    UrlImagen = "https://images.unsplash.com/photo-1597072689227-8882273e8f6a?q=80&w=1035&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Imagen
                {
                    IdImagen = 3,
                    UrlImagen = "https://images.unsplash.com/photo-1495546968767-f0573cca821e?q=80&w=1031&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Imagen
                {
                    IdImagen = 4,
                    UrlImagen = "https://images.unsplash.com/photo-1485965120184-e220f721d03e?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new Imagen
                {
                    IdImagen = 5,
                    UrlImagen = "https://images.unsplash.com/photo-1610890716171-6b1bb98ffd09?q=80&w=1031&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
            );

            // Seed ImagenProducto
            modelBuilder.Entity<ImagenProducto>().HasData(
                new ImagenProducto
                {
                    IdImagenProducto = 1,
                    ProductoId = 1, // Smartphone
                    ImagenId = 1   // smartphone_1.jpg
                },
                new ImagenProducto
                {
                    IdImagenProducto = 2,
                    ProductoId = 2, // Mesa de café
                    ImagenId = 2   // mesa_cafe_1.jpg
                },
                new ImagenProducto
                {
                    IdImagenProducto = 3,
                    ProductoId = 3, // Libro de cocina
                    ImagenId = 3   // libro_cocina_1.jpg
                },
                new ImagenProducto
                {
                    IdImagenProducto = 4,
                    ProductoId = 4, // Bicicleta de montaña
                    ImagenId = 4   // bicicleta_montana_1.jpg
                },
                new ImagenProducto
                {
                    IdImagenProducto = 5,
                    ProductoId = 5, // Juego de mesa
                    ImagenId = 5   // juego_mesa_1.jpg
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
