﻿using System;
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
                    usuario_id = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.InsertData(
                table: "categorias",
                columns: new[] { "id_categoria", "nombre_categoria" },
                values: new object[,]
                {
                    { 1, "Electrónica" },
                    { 2, "Hogar" }
                });

            migrationBuilder.InsertData(
                table: "imagenes",
                columns: new[] { "id_imagen", "url_image" },
                values: new object[,]
                {
                    { 1, "smartphone_1.jpg" },
                    { 2, "mesa_cafe_1.jpg" }
                });

            migrationBuilder.InsertData(
                table: "log_errores",
                columns: new[] { "id_log_error", "fecha_ocurrencia", "mensaje_error", "origen_error", "severidad", "stack_trace", "usuario_id" },
                values: new object[] { 2, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Error de autenticación", "AuthService.Authenticate", "Advertencia", "System.UnauthorizedAccessException: Error de autenticación...", null });

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
                columns: new[] { "id_usuario", "apellido_usuario", "contrasenia", "correo_electronico", "fecha_registro", "nombre_usuario", "rol_id", "telefono" },
                values: new object[,]
                {
                    { 1, "Sistema", "AQAAAAEAACcQAAAAEJGuO48K7z1Px5f3VTQE9YiuO0xi4A5WyHIjWc2wNpKPMxl35dyz0gEcdGDwgm9kzA==", "admin@sistema.com", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", 1, "123456789" },
                    { 2, "Pérez", "AQAAAAEAACcQAAAAEDy7P2HKHRAcZpScUPiVTBtLGFpG2XMfqhMHQUYP9l7HNmtfwXwfNnHUMpJ4G7VVAA==", "juan@example.com", new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juan", 2, "987654321" }
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
                    { 1, "Perfil administrador del sistema", "admin_profile.jpg", "Administrador", 1 },
                    { 2, "Me encanta intercambiar productos", "juan_profile.jpg", "JuanP", 2 }
                });

            migrationBuilder.InsertData(
                table: "productos",
                columns: new[] { "id_producto", "descripcion_producto", "fecha_creacion", "intercambio", "nombre_producto", "proceso_negociacion", "usuario_id" },
                values: new object[,]
                {
                    { 1, "Teléfono inteligente en buen estado", new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Smartphone", true, 2 },
                    { 2, "Mesa de madera para sala", new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Mesa de café", false, 2 }
                });

            migrationBuilder.InsertData(
                table: "categorias_producto",
                columns: new[] { "id_categoria_producto", "categoria_id", "producto_id" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "comentarios",
                columns: new[] { "id_comentario", "ComentarioIdComentario", "comentario_padre_id", "contenido_comentario", "fecha_creacion", "producto_id", "usuario_id" },
                values: new object[] { 1, null, null, "¿Está disponible para intercambio?", new DateTime(2025, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 });

            migrationBuilder.InsertData(
                table: "evaluaciones",
                columns: new[] { "id_evaluacion", "comentario", "fecha_creacion", "producto_id", "puntacion", "usuario_id" },
                values: new object[,]
                {
                    { 1, "Producto en excelente estado", new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 5, 1 },
                    { 2, "Buena calidad", new DateTime(2025, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "imagenes_producto",
                columns: new[] { "id_imagen_producto", "imagen_id", "producto_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
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
                name: "IX_productos_usuario_id",
                table: "productos",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_usuario_id",
                table: "refresh_tokens",
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
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "imagenes");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
