﻿// <auto-generated />
using System;
using BackendProyectoFinal.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackendProyectoFinal.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_categoria");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdCategoria"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombre_categoria");

                    b.HasKey("IdCategoria");

                    b.ToTable("categorias");

                    b.HasData(
                        new
                        {
                            IdCategoria = 1,
                            Nombre = "Electrónica"
                        },
                        new
                        {
                            IdCategoria = 2,
                            Nombre = "Hogar"
                        });
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.CategoriaProducto", b =>
                {
                    b.Property<int>("IdCategoriaProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_categoria_producto");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdCategoriaProducto"));

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int")
                        .HasColumnName("categoria_id");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int")
                        .HasColumnName("producto_id");

                    b.HasKey("IdCategoriaProducto");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("ProductoId");

                    b.ToTable("categorias_producto");

                    b.HasData(
                        new
                        {
                            IdCategoriaProducto = 1,
                            CategoriaId = 1,
                            ProductoId = 2
                        },
                        new
                        {
                            IdCategoriaProducto = 2,
                            CategoriaId = 2,
                            ProductoId = 2
                        });
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Comentario", b =>
                {
                    b.Property<int>("IdComentario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_comentario");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdComentario"));

                    b.Property<int?>("ComentarioIdComentario")
                        .HasColumnType("int");

                    b.Property<int?>("ComentarioPadreId")
                        .HasColumnType("int")
                        .HasColumnName("comentario_padre_id");

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("contenido_comentario");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha_creacion");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int")
                        .HasColumnName("producto_id");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnName("usuario_id");

                    b.HasKey("IdComentario");

                    b.HasIndex("ComentarioIdComentario");

                    b.HasIndex("ComentarioPadreId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("comentarios");

                    b.HasData(
                        new
                        {
                            IdComentario = 1,
                            Contenido = "¿Está disponible para intercambio?",
                            FechaCreacion = new DateTime(2025, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductoId = 1,
                            UsuarioId = 1
                        },
                        new
                        {
                            IdComentario = 2,
                            ComentarioPadreId = 1,
                            Contenido = "Sí, disponible para intercambio",
                            FechaCreacion = new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductoId = 1,
                            UsuarioId = 2
                        });
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Evaluacion", b =>
                {
                    b.Property<int>("IdEvaluacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_evaluacion");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdEvaluacion"));

                    b.Property<string>("Comentario")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("comentario");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha_creacion");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int")
                        .HasColumnName("producto_id");

                    b.Property<int>("Puntuacion")
                        .HasColumnType("int")
                        .HasColumnName("puntacion");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnName("usuario_id");

                    b.HasKey("IdEvaluacion");

                    b.HasIndex("ProductoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("evaluaciones");

                    b.HasData(
                        new
                        {
                            IdEvaluacion = 1,
                            Comentario = "Producto en excelente estado",
                            FechaCreacion = new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductoId = 2,
                            Puntuacion = 5,
                            UsuarioId = 1
                        },
                        new
                        {
                            IdEvaluacion = 2,
                            Comentario = "Buena calidad",
                            FechaCreacion = new DateTime(2025, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductoId = 1,
                            Puntuacion = 4,
                            UsuarioId = 2
                        });
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Imagen", b =>
                {
                    b.Property<int>("IdImagen")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_imagen");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdImagen"));

                    b.Property<string>("UrlImagen")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("url_image");

                    b.HasKey("IdImagen");

                    b.ToTable("imagenes");

                    b.HasData(
                        new
                        {
                            IdImagen = 1,
                            UrlImagen = "smartphone_1.jpg"
                        },
                        new
                        {
                            IdImagen = 2,
                            UrlImagen = "mesa_cafe_1.jpg"
                        });
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.ImagenProducto", b =>
                {
                    b.Property<int>("IdImagenProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_imagen_producto");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdImagenProducto"));

                    b.Property<int>("ImagenId")
                        .HasColumnType("int")
                        .HasColumnName("imagen_id");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int")
                        .HasColumnName("producto_id");

                    b.HasKey("IdImagenProducto");

                    b.HasIndex("ImagenId");

                    b.HasIndex("ProductoId");

                    b.ToTable("imagenes_producto");

                    b.HasData(
                        new
                        {
                            IdImagenProducto = 1,
                            ImagenId = 1,
                            ProductoId = 1
                        },
                        new
                        {
                            IdImagenProducto = 2,
                            ImagenId = 2,
                            ProductoId = 2
                        });
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Intercambio", b =>
                {
                    b.Property<int>("IdIntercambio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_intercambio");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdIntercambio"));

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha_registro");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int")
                        .HasColumnName("producto_id");

                    b.Property<int>("UsuarioOfertanteId")
                        .HasColumnType("int")
                        .HasColumnName("usuario_ofertante_id");

                    b.Property<int>("UsuarioSolicitanteId")
                        .HasColumnType("int")
                        .HasColumnName("usuario_solicitante_id");

                    b.HasKey("IdIntercambio");

                    b.HasIndex("ProductoId");

                    b.HasIndex("UsuarioOfertanteId");

                    b.HasIndex("UsuarioSolicitanteId");

                    b.ToTable("intercambios");

                    b.HasData(
                        new
                        {
                            IdIntercambio = 1,
                            FechaRegistro = new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductoId = 1,
                            UsuarioOfertanteId = 2,
                            UsuarioSolicitanteId = 1
                        },
                        new
                        {
                            IdIntercambio = 2,
                            FechaRegistro = new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductoId = 2,
                            UsuarioOfertanteId = 1,
                            UsuarioSolicitanteId = 2
                        });
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.LogError", b =>
                {
                    b.Property<int>("IdLogError")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_log_error");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdLogError"));

                    b.Property<DateTime>("FechaOcurrencia")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha_ocurrencia");

                    b.Property<string>("Mensaje")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("mensaje_error");

                    b.Property<string>("Origen")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("origen_error");

                    b.Property<string>("Severidad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("severidad");

                    b.Property<string>("StackTrace")
                        .HasColumnType("longtext")
                        .HasColumnName("stack_trace");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnName("usuario_id");

                    b.HasKey("IdLogError");

                    b.HasIndex("UsuarioId");

                    b.ToTable("log_errores");

                    b.HasData(
                        new
                        {
                            IdLogError = 1,
                            FechaOcurrencia = new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Mensaje = "Error al procesar imagen",
                            Origen = "ImagenService.ProcessImage",
                            Severidad = "Error",
                            StackTrace = "System.IO.IOException: Error al procesar imagen...",
                            UsuarioId = 2
                        },
                        new
                        {
                            IdLogError = 2,
                            FechaOcurrencia = new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Mensaje = "Error de autenticación",
                            Origen = "AuthService.Authenticate",
                            Severidad = "Advertencia",
                            StackTrace = "System.UnauthorizedAccessException: Error de autenticación..."
                        });
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Perfil", b =>
                {
                    b.Property<int>("IdPerfil")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_perfil");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdPerfil"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("descripcion");

                    b.Property<string>("ImagenPerfil")
                        .HasColumnType("longtext")
                        .HasColumnName("imagen_perfil");

                    b.Property<string>("NombrePerfil")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombre_perfil");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnName("usuario_id");

                    b.HasKey("IdPerfil");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("perfiles");

                    b.HasData(
                        new
                        {
                            IdPerfil = 1,
                            Descripcion = "Perfil administrador del sistema",
                            ImagenPerfil = "admin_profile.jpg",
                            NombrePerfil = "Administrador",
                            UsuarioId = 1
                        },
                        new
                        {
                            IdPerfil = 2,
                            Descripcion = "Me encanta intercambiar productos",
                            ImagenPerfil = "juan_profile.jpg",
                            NombrePerfil = "JuanP",
                            UsuarioId = 2
                        });
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Producto", b =>
                {
                    b.Property<int>("IdProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_producto");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdProducto"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("descripcion_producto");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha_creacion");

                    b.Property<bool>("Intercambio")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("intercambio");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombre_producto");

                    b.Property<bool>("ProcesoNegociacion")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("proceso_negociacion");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnName("usuario_id");

                    b.HasKey("IdProducto");

                    b.HasIndex("UsuarioId");

                    b.ToTable("productos");

                    b.HasData(
                        new
                        {
                            IdProducto = 1,
                            Descripcion = "Teléfono inteligente en buen estado",
                            FechaCreacion = new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Intercambio = true,
                            Nombre = "Smartphone",
                            ProcesoNegociacion = true,
                            UsuarioId = 2
                        },
                        new
                        {
                            IdProducto = 2,
                            Descripcion = "Mesa de madera para sala",
                            FechaCreacion = new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Intercambio = true,
                            Nombre = "Mesa de café",
                            ProcesoNegociacion = false,
                            UsuarioId = 2
                        });
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<int>("IdToken")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_token");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdToken"));

                    b.Property<bool>("EstaActivo")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("esta_activo");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha_creacion");

                    b.Property<DateTime>("FechaExpiracion")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha_expiracion");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("token");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnName("usuario_id");

                    b.HasKey("IdToken");

                    b.HasIndex("UsuarioId");

                    b.ToTable("refresh_tokens");
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Rol", b =>
                {
                    b.Property<int>("IdRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_rol");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdRol"));

                    b.Property<string>("NombreRol")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombre_rol");

                    b.HasKey("IdRol");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            IdRol = 1,
                            NombreRol = "Administrador"
                        },
                        new
                        {
                            IdRol = 2,
                            NombreRol = "Usuario"
                        });
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("apellido_usuario");

                    b.Property<string>("Contrasenia")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("contrasenia");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("correo_electronico");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha_registro");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombre_usuario");

                    b.Property<int>("RolId")
                        .HasColumnType("int")
                        .HasColumnName("rol_id");

                    b.Property<string>("Telefono")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("telefono");

                    b.HasKey("IdUsuario");

                    b.HasIndex("CorreoElectronico")
                        .IsUnique();

                    b.HasIndex("RolId");

                    b.ToTable("usuarios");

                    b.HasData(
                        new
                        {
                            IdUsuario = 1,
                            Apellido = "Sistema",
                            Contrasenia = "AQAAAAEAACcQAAAAEJGuO48K7z1Px5f3VTQE9YiuO0xi4A5WyHIjWc2wNpKPMxl35dyz0gEcdGDwgm9kzA==",
                            CorreoElectronico = "admin@sistema.com",
                            FechaRegistro = new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Admin",
                            RolId = 1,
                            Telefono = "123456789"
                        },
                        new
                        {
                            IdUsuario = 2,
                            Apellido = "Pérez",
                            Contrasenia = "AQAAAAEAACcQAAAAEDy7P2HKHRAcZpScUPiVTBtLGFpG2XMfqhMHQUYP9l7HNmtfwXwfNnHUMpJ4G7VVAA==",
                            CorreoElectronico = "juan@example.com",
                            FechaRegistro = new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Juan",
                            RolId = 2,
                            Telefono = "987654321"
                        });
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.CategoriaProducto", b =>
                {
                    b.HasOne("BackendProyectoFinal.Domain.Entities.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendProyectoFinal.Domain.Entities.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Comentario", b =>
                {
                    b.HasOne("BackendProyectoFinal.Domain.Entities.Comentario", null)
                        .WithMany("ComentariosHijos")
                        .HasForeignKey("ComentarioIdComentario");

                    b.HasOne("BackendProyectoFinal.Domain.Entities.Comentario", "ComentarioPadre")
                        .WithMany()
                        .HasForeignKey("ComentarioPadreId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BackendProyectoFinal.Domain.Entities.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendProyectoFinal.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ComentarioPadre");

                    b.Navigation("Producto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Evaluacion", b =>
                {
                    b.HasOne("BackendProyectoFinal.Domain.Entities.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendProyectoFinal.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.ImagenProducto", b =>
                {
                    b.HasOne("BackendProyectoFinal.Domain.Entities.Imagen", "Imagen")
                        .WithMany()
                        .HasForeignKey("ImagenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendProyectoFinal.Domain.Entities.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Imagen");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Intercambio", b =>
                {
                    b.HasOne("BackendProyectoFinal.Domain.Entities.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendProyectoFinal.Domain.Entities.Usuario", "UsuarioOfertante")
                        .WithMany()
                        .HasForeignKey("UsuarioOfertanteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BackendProyectoFinal.Domain.Entities.Usuario", "UsuarioSolicitante")
                        .WithMany()
                        .HasForeignKey("UsuarioSolicitanteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("UsuarioOfertante");

                    b.Navigation("UsuarioSolicitante");
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.LogError", b =>
                {
                    b.HasOne("BackendProyectoFinal.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Perfil", b =>
                {
                    b.HasOne("BackendProyectoFinal.Domain.Entities.Usuario", "Usuario")
                        .WithOne()
                        .HasForeignKey("BackendProyectoFinal.Domain.Entities.Perfil", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Producto", b =>
                {
                    b.HasOne("BackendProyectoFinal.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("BackendProyectoFinal.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Usuario", b =>
                {
                    b.HasOne("BackendProyectoFinal.Domain.Entities.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Comentario", b =>
                {
                    b.Navigation("ComentariosHijos");
                });
#pragma warning restore 612, 618
        }
    }
}
