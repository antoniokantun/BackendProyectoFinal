using Microsoft.EntityFrameworkCore;
using BackendProyectoFinal.Domain.Entities;

namespace BackendProyectoFinal.Infrastructure.Seeders
{
    public static class DatabaseSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedCategorias(modelBuilder);
            SeedCategoriasProducto(modelBuilder);
            SeedComentarios(modelBuilder);
            SeedEvaluaciones(modelBuilder);
            SeedImagenesProducto(modelBuilder);
            SeedIntercambios(modelBuilder);
            SeedPerfiles(modelBuilder);
            SeedProductos(modelBuilder);
            SeedUsuarios(modelBuilder);
        }

        private static void SeedCategorias(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasData(
            new Categoria { IdCategoria = 1, Nombre = "Electrónica" },
            new Categoria { IdCategoria = 2, Nombre = "Ropa" },
            new Categoria { IdCategoria = 3, Nombre = "Hogar" }
            );
        }

        private static void SeedCategoriasProducto(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<CategoriaProducto>().HasData(
           new CategoriaProducto
            {
            IdCategoriaProducto = 1,
            CategoriaId = 1, // Relación con la categoría "Electrónica"
            UsuarioId = 1 // Relación con el usuario Juan Pérez
            },
          new CategoriaProducto
        {
            IdCategoriaProducto = 2,
            CategoriaId = 2, // Relación con la categoría "Ropa"
            UsuarioId = 2 // Relación con el usuario María Gómez
        }
        );
        }

        private static void SeedComentarios(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comentario>().HasData(
            new Comentario
            {
            IdComentario = 1,
            Contenido = "¡Me encanta este producto!",
            UsuarioId = 1, // Relación con el usuario Juan Pérez
            ProductoId = 2, // Relación con el producto "Laptop ABC"
            FechaCreacion = DateTime.UtcNow,
            ComentarioPadreId = null // Sin comentario padre
            },
            new Comentario
            {
            IdComentario = 2,
            Contenido = "¿Todavía está disponible?",
            UsuarioId = 2, // Relación con el usuario María Gómez
            ProductoId = 1, // Relación con el producto "Smartphone XYZ"
            FechaCreacion = DateTime.UtcNow,
            ComentarioPadreId = 1 // Relación con el comentario anterior
            }
        );
        }

        private static void SeedEvaluaciones(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evaluacion>().HasData(
            new Evaluacion
        {
            IdEvaluacion = 1,
            UsuarioId = 1, // Relación con el usuario Juan Pérez
            ProductoId = 2, // Relación con el producto "Laptop ABC"
            FechaCreacion = DateTime.UtcNow,
            Comentario = "Excelente producto, muy recomendado.",
            Puntuacion = 5
        },
            new Evaluacion
        {
            IdEvaluacion = 2,
            UsuarioId = 2, // Relación con el usuario María Gómez
            ProductoId = 1, // Relación con el producto "Smartphone XYZ"
            FechaCreacion = DateTime.UtcNow,
            Comentario = "Buen producto, pero podría mejorar.",
            Puntuacion = 4
        }
        );
        }

        private static void SeedImagenesProducto(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImagenProducto>().HasData(
            new ImagenProducto
        {
            IdImagenProducto = 1,
            ProductoId = 1, // Relación con el producto "Smartphone XYZ"
            UrlImagen = "https://example.com/smartphone.jpg"
        },
            new ImagenProducto
        {
            IdImagenProducto = 2,
            ProductoId = 2, // Relación con el producto "Laptop ABC"
            UrlImagen = "https://example.com/laptop.jpg"
        }
        );
        }

        private static void SeedIntercambios(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Intercambio>().HasData(
            new Intercambio
            {
                IdIntercambio = 1,
                UsuarioSolicitanteId = 1, // Relación con el usuario Juan Pérez
                UsuarioOfertanteId = 2, // Relación con el usuario María Gómez
                ProductoId = 1, // Relación con el producto "Smartphone XYZ"
                FechaRegistro = DateTime.UtcNow
            }
        );
        }

        private static void SeedPerfiles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Perfil>().HasData(
            new Perfil
            {
                IdPerfil = 1,
                UsuarioId = 1, // Relación con el usuario Juan Pérez
                ImagenPerfil = "https://example.com/juanperez.jpg",
                NombrePerfil = "JuanP",
                Descripcion = "Amante de la tecnología."
            },
            new Perfil
            {
                IdPerfil = 2,
                UsuarioId = 2, // Relación con el usuario María Gómez
                ImagenPerfil = "https://example.com/mariagomez.jpg",
                NombrePerfil = "MariaG",
                Descripcion = "Fashionista y amante de la moda."
            }
        );
        }

        private static void SeedProductos(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasData(
            new Producto
            {
                IdProducto = 1,
                Nombre = "Smartphone XYZ",
                Descripcion = "Un smartphone de última generación.",
                FechaCreacion = DateTime.UtcNow,
                ProcesoNegociacion = false,
                Intercambio = true,
                UsuarioId = 1 // Relación con el usuario Juan Pérez
            },
            new Producto
            {
                IdProducto = 2,
                Nombre = "Laptop ABC",
                Descripcion = "Una laptop potente para trabajo y juegos.",
                FechaCreacion = DateTime.UtcNow,
                ProcesoNegociacion = true,
                Intercambio = false,
                UsuarioId = 2 // Relación con el usuario María Gómez
            }
        );
        }

        private static void SeedUsuarios(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                IdUsuario = 1,
                Nombre = "Juan",
                Apellido = "Pérez",
                CorreoElectronico = "juan.perez@example.com",
                Telefono = "123456789",
                Contrasenia = "hashedpassword1", // Asegúrate de hashear las contraseñas
                FechaRegistro = DateTime.UtcNow
            },
            new Usuario
            {
                IdUsuario = 2,
                Nombre = "María",
                Apellido = "Gómez",
                CorreoElectronico = "maria.gomez@example.com",
                Telefono = "987654321",
                Contrasenia = "hashedpassword2", // Asegúrate de hashear las contraseñas
                FechaRegistro = DateTime.UtcNow
            }
            );
        }

    }
}