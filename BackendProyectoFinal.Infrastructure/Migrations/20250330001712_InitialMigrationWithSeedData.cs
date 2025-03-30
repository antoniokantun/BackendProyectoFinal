using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendProyectoFinal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationWithSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    id_categoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_categoria = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    imagen_categoria = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.id_categoria);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "imagenes",
                columns: table => new
                {
                    id_imagen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    url_image = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imagenes", x => x.id_imagen);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "reportes",
                columns: table => new
                {
                    id_reporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    motivo_reporte = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_reporte = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportes", x => x.id_reporte);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_rol = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id_rol);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_usuario = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido_usuario = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    correo_electronico = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contrasenia = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_registro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    baneado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    reportado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    rol_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id_usuario);
                    table.ForeignKey(
                        name: "FK_usuarios_roles_rol_id",
                        column: x => x.rol_id,
                        principalTable: "roles",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "log_errores",
                columns: table => new
                {
                    id_log_error = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    mensaje_error = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_ocurrencia = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    origen_error = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    severidad = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    stack_trace = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    usuario_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_errores", x => x.id_log_error);
                    table.ForeignKey(
                        name: "FK_log_errores_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "perfiles",
                columns: table => new
                {
                    id_perfil = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    usuario_id = table.Column<int>(type: "int", nullable: false),
                    imagen_perfil = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombre_perfil = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perfiles", x => x.id_perfil);
                    table.ForeignKey(
                        name: "FK_perfiles_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    id_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_producto = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion_producto = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_creacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    proceso_negociacion = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    intercambio = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    no_visible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    reportado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    usuario_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.id_producto);
                    table.ForeignKey(
                        name: "FK_productos_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    id_token = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    token = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_expiracion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    esta_activo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    usuario_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.id_token);
                    table.ForeignKey(
                        name: "FK_refresh_tokens_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario_reporte",
                columns: table => new
                {
                    id_usuario_reporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    usuario_id = table.Column<int>(type: "int", nullable: false),
                    reporte_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_reporte", x => x.id_usuario_reporte);
                    table.ForeignKey(
                        name: "FK_usuario_reporte_reportes_reporte_id",
                        column: x => x.reporte_id,
                        principalTable: "reportes",
                        principalColumn: "id_reporte",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuario_reporte_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "categorias_producto",
                columns: table => new
                {
                    id_categoria_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    categoria_id = table.Column<int>(type: "int", nullable: false),
                    producto_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias_producto", x => x.id_categoria_producto);
                    table.ForeignKey(
                        name: "FK_categorias_producto_categorias_categoria_id",
                        column: x => x.categoria_id,
                        principalTable: "categorias",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_categorias_producto_productos_producto_id",
                        column: x => x.producto_id,
                        principalTable: "productos",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "comentarios",
                columns: table => new
                {
                    id_comentario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    contenido_comentario = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    usuario_id = table.Column<int>(type: "int", nullable: false),
                    producto_id = table.Column<int>(type: "int", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    comentario_padre_id = table.Column<int>(type: "int", nullable: true),
                    ComentarioIdComentario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comentarios", x => x.id_comentario);
                    table.ForeignKey(
                        name: "FK_comentarios_comentarios_ComentarioIdComentario",
                        column: x => x.ComentarioIdComentario,
                        principalTable: "comentarios",
                        principalColumn: "id_comentario");
                    table.ForeignKey(
                        name: "FK_comentarios_comentarios_comentario_padre_id",
                        column: x => x.comentario_padre_id,
                        principalTable: "comentarios",
                        principalColumn: "id_comentario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comentarios_productos_producto_id",
                        column: x => x.producto_id,
                        principalTable: "productos",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comentarios_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "evaluaciones",
                columns: table => new
                {
                    id_evaluacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    titulo_evaluacion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    usuario_id = table.Column<int>(type: "int", nullable: false),
                    usuario_evaluador_id = table.Column<int>(type: "int", nullable: false),
                    producto_id = table.Column<int>(type: "int", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    comentario = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    puntacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluaciones", x => x.id_evaluacion);
                    table.ForeignKey(
                        name: "FK_evaluaciones_productos_producto_id",
                        column: x => x.producto_id,
                        principalTable: "productos",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_evaluaciones_usuarios_usuario_evaluador_id",
                        column: x => x.usuario_evaluador_id,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_evaluaciones_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "imagenes_producto",
                columns: table => new
                {
                    id_imagen_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    producto_id = table.Column<int>(type: "int", nullable: false),
                    imagen_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imagenes_producto", x => x.id_imagen_producto);
                    table.ForeignKey(
                        name: "FK_imagenes_producto_imagenes_imagen_id",
                        column: x => x.imagen_id,
                        principalTable: "imagenes",
                        principalColumn: "id_imagen",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_imagenes_producto_productos_producto_id",
                        column: x => x.producto_id,
                        principalTable: "productos",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "intercambios",
                columns: table => new
                {
                    id_intercambio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    usuario_solicitante_id = table.Column<int>(type: "int", nullable: false),
                    usuario_ofertante_id = table.Column<int>(type: "int", nullable: false),
                    producto_id = table.Column<int>(type: "int", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_intercambios", x => x.id_intercambio);
                    table.ForeignKey(
                        name: "FK_intercambios_productos_producto_id",
                        column: x => x.producto_id,
                        principalTable: "productos",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_intercambios_usuarios_usuario_ofertante_id",
                        column: x => x.usuario_ofertante_id,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_intercambios_usuarios_usuario_solicitante_id",
                        column: x => x.usuario_solicitante_id,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "producto_reporte",
                columns: table => new
                {
                    id_producto_reporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    producto_id = table.Column<int>(type: "int", nullable: false),
                    reporte_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producto_reporte", x => x.id_producto_reporte);
                    table.ForeignKey(
                        name: "FK_producto_reporte_productos_producto_id",
                        column: x => x.producto_id,
                        principalTable: "productos",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_producto_reporte_reportes_reporte_id",
                        column: x => x.reporte_id,
                        principalTable: "reportes",
                        principalColumn: "id_reporte",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "categorias",
                columns: new[] { "id_categoria", "imagen_categoria", "nombre_categoria" },
                values: new object[,]
                {
                    { 1, "https://images.unsplash.com/photo-1550009158-9ebf69173e03?q=80&w=1201&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Electrónica" },
                    { 2, "https://images.unsplash.com/photo-1618220179428-22790b461013?q=80&w=1227&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Hogar" },
                    { 3, "https://images.unsplash.com/photo-1523381210434-271e8be1f52b?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Ropa" },
                    { 4, "https://images.unsplash.com/photo-1495446815901-a7297e633e8d?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Libros" },
                    { 5, "https://images.unsplash.com/flagged/photo-1556746834-cbb4a38ee593?q=80&w=1172&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Deportes" },
                    { 6, "https://images.unsplash.com/photo-1587654780291-39c9404d746b?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Juguetes" },
                    { 7, "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Muebles" },
                    { 8, "https://images.unsplash.com/photo-1581783898377-1c85bf937427?q=80&w=1015&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Herramientas" }
                });

            migrationBuilder.InsertData(
                table: "imagenes",
                columns: new[] { "id_imagen", "url_image" },
                values: new object[,]
                {
                    { 1, "https://plus.unsplash.com/premium_photo-1664201890402-6886a989796f?q=80&w=987&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                    { 2, "https://images.unsplash.com/photo-1597072689227-8882273e8f6a?q=80&w=1035&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                    { 3, "https://images.unsplash.com/photo-1495546968767-f0573cca821e?q=80&w=1031&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                    { 4, "https://images.unsplash.com/photo-1485965120184-e220f721d03e?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                    { 5, "https://images.unsplash.com/photo-1610890716171-6b1bb98ffd09?q=80&w=1031&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" }
                });

            migrationBuilder.InsertData(
                table: "log_errores",
                columns: new[] { "id_log_error", "fecha_ocurrencia", "mensaje_error", "origen_error", "severidad", "stack_trace", "usuario_id" },
                values: new object[] { 2, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Error de autenticación", "AuthService.Authenticate", "Advertencia", "System.UnauthorizedAccessException: Error de autenticación...", null });

            migrationBuilder.InsertData(
                table: "reportes",
                columns: new[] { "id_reporte", "fecha_reporte", "motivo_reporte" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 15, 10, 30, 0, 0, DateTimeKind.Utc), "Contenido inapropiado" },
                    { 2, new DateTime(2024, 3, 16, 14, 45, 0, 0, DateTimeKind.Utc), "Información engañosa o fraudulenta" },
                    { 3, new DateTime(2024, 3, 17, 9, 15, 0, 0, DateTimeKind.Utc), "Comportamiento sospechoso" },
                    { 4, new DateTime(2024, 3, 18, 16, 20, 0, 0, DateTimeKind.Utc), "Producto no coincide con la descripción" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id_rol", "nombre_rol" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Usuario" }
                });

            migrationBuilder.InsertData(
                table: "usuarios",
                columns: new[] { "id_usuario", "apellido_usuario", "baneado", "contrasenia", "correo_electronico", "fecha_registro", "nombre_usuario", "reportado", "rol_id", "telefono" },
                values: new object[,]
                {
                    { 1, "Sistema", false, "AQAAAAEAACcQAAAAEJGuO48K7z1Px5f3VTQE9YiuO0xi4A5WyHIjWc2wNpKPMxl35dyz0gEcdGDwgm9kzA==", "admin@sistema.com", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", false, 1, "123456789" },
                    { 2, "Pérez", false, "AQAAAAEAACcQAAAAEDy7P2HKHRAcZpScUPiVTBtLGFpG2XMfqhMHQUYP9l7HNmtfwXwfNnHUMpJ4G7VVAA==", "juan@example.com", new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juan", false, 2, "987654321" },
                    { 3, "Gómez", false, "AQAAAAEAACcQAAAAEFCxR9j/L3Wd7nC0p/W4aLgYg1zMv8zKjEaXbZ9pQoR/sT2uBvC7wYnZqN8u9yFAA==", "maria@example.com", new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "María", false, 2, "5551234567" },
                    { 4, "López", false, "AQAAAAEAACcQAAAAEL+0iJ9pS/RkYvX/Z8bTqU2wN1oPz7uIe/jKdLsMvX9bZcRjWnFvQxUeYvA2b7AAA==", "carlos@example.com", new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carlos", false, 2, "1119876543" }
                });

            migrationBuilder.InsertData(
                table: "log_errores",
                columns: new[] { "id_log_error", "fecha_ocurrencia", "mensaje_error", "origen_error", "severidad", "stack_trace", "usuario_id" },
                values: new object[] { 1, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Error al procesar imagen", "ImagenService.ProcessImage", "Error", "System.IO.IOException: Error al procesar imagen...", 2 });

            migrationBuilder.InsertData(
                table: "perfiles",
                columns: new[] { "id_perfil", "descripcion", "imagen_perfil", "nombre_perfil", "usuario_id" },
                values: new object[,]
                {
                    { 1, "Perfil administrador del sistema", "https://images.unsplash.com/photo-1543610892-0b1f7e6d8ac1?q=80&w=1856&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Administrador", 1 },
                    { 2, "Me encanta intercambiar productos", "https://images.unsplash.com/photo-1489980557514-251d61e3eeb6?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "JuanP", 2 },
                    { 3, "Busco ofertas interesantes para mi hogar", "https://images.unsplash.com/photo-1577565177023-d0f29c354b69?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "MariaG", 3 },
                    { 4, "Apasionado por la tecnología y los gadgets", "https://images.unsplash.com/photo-1558203728-00f45181dd84?q=80&w=2074&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "CarlosL", 4 }
                });

            migrationBuilder.InsertData(
                table: "productos",
                columns: new[] { "id_producto", "descripcion_producto", "fecha_creacion", "intercambio", "no_visible", "nombre_producto", "proceso_negociacion", "reportado", "usuario_id" },
                values: new object[,]
                {
                    { 1, "Teléfono inteligente en buen estado", new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Smartphone", true, false, 2 },
                    { 2, "Mesa de madera para sala", new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Mesa de café", false, false, 2 },
                    { 3, "Recetas fáciles y deliciosas", new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Libro de cocina", false, false, 3 },
                    { 4, "Ideal para aventuras al aire libre", new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Bicicleta de montaña", true, false, 4 },
                    { 5, "Para disfrutar en familia o con amigos", new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Juego de mesa", false, false, 3 }
                });

            migrationBuilder.InsertData(
                table: "usuario_reporte",
                columns: new[] { "id_usuario_reporte", "reporte_id", "usuario_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "categorias_producto",
                columns: new[] { "id_categoria_producto", "categoria_id", "producto_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 2, 3 },
                    { 4, 4, 3 },
                    { 5, 5, 4 },
                    { 6, 6, 5 },
                    { 7, 2, 5 },
                    { 8, 1, 1 },
                    { 9, 7, 2 }
                });

            migrationBuilder.InsertData(
                table: "comentarios",
                columns: new[] { "id_comentario", "ComentarioIdComentario", "comentario_padre_id", "contenido_comentario", "fecha_creacion", "producto_id", "usuario_id" },
                values: new object[] { 1, null, null, "¿Está disponible para intercambio?", new DateTime(2025, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 });

            migrationBuilder.InsertData(
                table: "evaluaciones",
                columns: new[] { "id_evaluacion", "comentario", "fecha_creacion", "producto_id", "puntacion", "titulo_evaluacion", "usuario_evaluador_id", "usuario_id" },
                values: new object[,]
                {
                    { 1, "Buen producto", new DateTime(2025, 3, 29, 19, 17, 12, 43, DateTimeKind.Local).AddTicks(2788), 1, 5, "Evaluación del producto 1", 2, 1 },
                    { 2, "Buena calidad", new DateTime(2025, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, "Evaluación del producto 2", 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "imagenes_producto",
                columns: new[] { "id_imagen_producto", "imagen_id", "producto_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "intercambios",
                columns: new[] { "id_intercambio", "fecha_registro", "producto_id", "usuario_ofertante_id", "usuario_solicitante_id" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 1 },
                    { 2, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "producto_reporte",
                columns: new[] { "id_producto_reporte", "producto_id", "reporte_id" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "comentarios",
                columns: new[] { "id_comentario", "ComentarioIdComentario", "comentario_padre_id", "contenido_comentario", "fecha_creacion", "producto_id", "usuario_id" },
                values: new object[] { 2, null, 1, "Sí, disponible para intercambio", new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_categorias_producto_categoria_id",
                table: "categorias_producto",
                column: "categoria_id");

            migrationBuilder.CreateIndex(
                name: "IX_categorias_producto_producto_id",
                table: "categorias_producto",
                column: "producto_id");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_comentario_padre_id",
                table: "comentarios",
                column: "comentario_padre_id");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_ComentarioIdComentario",
                table: "comentarios",
                column: "ComentarioIdComentario");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_producto_id",
                table: "comentarios",
                column: "producto_id");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_usuario_id",
                table: "comentarios",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_evaluaciones_producto_id",
                table: "evaluaciones",
                column: "producto_id");

            migrationBuilder.CreateIndex(
                name: "IX_evaluaciones_usuario_evaluador_id",
                table: "evaluaciones",
                column: "usuario_evaluador_id");

            migrationBuilder.CreateIndex(
                name: "IX_evaluaciones_usuario_id",
                table: "evaluaciones",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_imagenes_producto_imagen_id",
                table: "imagenes_producto",
                column: "imagen_id");

            migrationBuilder.CreateIndex(
                name: "IX_imagenes_producto_producto_id",
                table: "imagenes_producto",
                column: "producto_id");

            migrationBuilder.CreateIndex(
                name: "IX_intercambios_producto_id",
                table: "intercambios",
                column: "producto_id");

            migrationBuilder.CreateIndex(
                name: "IX_intercambios_usuario_ofertante_id",
                table: "intercambios",
                column: "usuario_ofertante_id");

            migrationBuilder.CreateIndex(
                name: "IX_intercambios_usuario_solicitante_id",
                table: "intercambios",
                column: "usuario_solicitante_id");

            migrationBuilder.CreateIndex(
                name: "IX_log_errores_usuario_id",
                table: "log_errores",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_perfiles_usuario_id",
                table: "perfiles",
                column: "usuario_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_producto_reporte_producto_id",
                table: "producto_reporte",
                column: "producto_id");

            migrationBuilder.CreateIndex(
                name: "IX_producto_reporte_reporte_id",
                table: "producto_reporte",
                column: "reporte_id");

            migrationBuilder.CreateIndex(
                name: "IX_productos_usuario_id",
                table: "productos",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_usuario_id",
                table: "refresh_tokens",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_reporte_reporte_id",
                table: "usuario_reporte",
                column: "reporte_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_reporte_usuario_id",
                table: "usuario_reporte",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_correo_electronico",
                table: "usuarios",
                column: "correo_electronico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_rol_id",
                table: "usuarios",
                column: "rol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categorias_producto");

            migrationBuilder.DropTable(
                name: "comentarios");

            migrationBuilder.DropTable(
                name: "evaluaciones");

            migrationBuilder.DropTable(
                name: "imagenes_producto");

            migrationBuilder.DropTable(
                name: "intercambios");

            migrationBuilder.DropTable(
                name: "log_errores");

            migrationBuilder.DropTable(
                name: "perfiles");

            migrationBuilder.DropTable(
                name: "producto_reporte");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "usuario_reporte");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "imagenes");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "reportes");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
