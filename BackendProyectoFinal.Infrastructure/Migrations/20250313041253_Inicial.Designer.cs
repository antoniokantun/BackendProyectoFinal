﻿// <auto-generated />
using System;
using BackendProyectoFinal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackendProyectoFinal.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250313041253_Inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnName("usuario_id");

                    b.HasKey("IdCategoriaProducto");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("categorias_producto");
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Comentario", b =>
                {
                    b.Property<int>("IdComentario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_comentario");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdComentario"));

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

                    b.HasIndex("ComentarioPadreId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("comentarios");
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
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.ImagenProducto", b =>
                {
                    b.Property<int>("IdImagenProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_imagen_producto");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdImagenProducto"));

                    b.Property<int>("ProductoId")
                        .HasColumnType("int")
                        .HasColumnName("producto_id");

                    b.Property<string>("UrlImagen")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("url_image");

                    b.HasKey("IdImagenProducto");

                    b.HasIndex("ProductoId");

                    b.ToTable("imagenes_producto");
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

                    b.HasIndex("UsuarioId");

                    b.ToTable("perfiles");
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
                        .HasColumnType("longtext")
                        .HasColumnName("correo_electronico");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("fecha_registro");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nombre_usuario");

                    b.Property<string>("Telefono")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("telefono");

                    b.HasKey("IdUsuario");

                    b.ToTable("usuarios");
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.CategoriaProducto", b =>
                {
                    b.HasOne("BackendProyectoFinal.Domain.Entities.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendProyectoFinal.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.Comentario", b =>
                {
                    b.HasOne("BackendProyectoFinal.Domain.Entities.Comentario", "ComentarioPadre")
                        .WithMany()
                        .HasForeignKey("ComentarioPadreId");

                    b.HasOne("BackendProyectoFinal.Domain.Entities.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendProyectoFinal.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BackendProyectoFinal.Domain.Entities.ImagenProducto", b =>
                {
                    b.HasOne("BackendProyectoFinal.Domain.Entities.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendProyectoFinal.Domain.Entities.Usuario", "UsuarioSolicitante")
                        .WithMany()
                        .HasForeignKey("UsuarioSolicitanteId")
                        .OnDelete(DeleteBehavior.Cascade)
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
                        .WithMany()
                        .HasForeignKey("UsuarioId")
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
#pragma warning restore 612, 618
        }
    }
}
