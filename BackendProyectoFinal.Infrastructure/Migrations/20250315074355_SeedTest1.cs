using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendProyectoFinal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedTest1 : Migration
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
                name: "usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_usuario = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido_usuario = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    correo_electronico = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contrasenia = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_registro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id_usuario);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "categorias_producto",
                columns: table => new
                {
                    id_categoria_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    categoria_id = table.Column<int>(type: "int", nullable: false),
                    usuario_id = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_categorias_producto_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
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
                    comentario_padre_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comentarios", x => x.id_comentario);
                    table.ForeignKey(
                        name: "FK_comentarios_comentarios_comentario_padre_id",
                        column: x => x.comentario_padre_id,
                        principalTable: "comentarios",
                        principalColumn: "id_comentario");
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_intercambios_usuarios_usuario_solicitante_id",
                        column: x => x.usuario_solicitante_id,
                        principalTable: "usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "categorias",
                columns: new[] { "id_categoria", "nombre_categoria" },
                values: new object[,]
                {
                    { 1, "Electrónica" },
                    { 2, "Ropa" },
                    { 3, "Hogar" }
                });

            migrationBuilder.InsertData(
                table: "imagenes",
                columns: new[] { "id_imagen", "url_image" },
                values: new object[,]
                {
                    { 1, "https://example.com/imagen1.jpg" },
                    { 2, "https://example.com/imagen2.jpg" },
                    { 3, "https://example.com/imagen3.jpg" }
                });

            migrationBuilder.InsertData(
                table: "usuarios",
                columns: new[] { "id_usuario", "apellido_usuario", "contrasenia", "correo_electronico", "fecha_registro", "nombre_usuario", "telefono" },
                values: new object[,]
                {
                    { 1, "Pérez", "juan123456", "juan.perez@example.com", new DateTime(2025, 3, 15, 7, 43, 55, 421, DateTimeKind.Utc).AddTicks(249), "Juan", "123456789" },
                    { 2, "Gómez", "maria123456", "maria.gomez@example.com", new DateTime(2025, 3, 15, 7, 43, 55, 421, DateTimeKind.Utc).AddTicks(251), "María", "987654321" }
                });

            migrationBuilder.InsertData(
                table: "categorias_producto",
                columns: new[] { "id_categoria_producto", "categoria_id", "usuario_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "perfiles",
                columns: new[] { "id_perfil", "descripcion", "imagen_perfil", "nombre_perfil", "usuario_id" },
                values: new object[,]
                {
                    { 1, "Amante de la tecnología.", "https://images.unsplash.com/flagged/photo-1595514191830-3e96a518989b?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "JuanP", 1 },
                    { 2, "Fashionista y amante de la moda.", "https://images.unsplash.com/photo-1579591919791-0e3737ae3808?q=80&w=2030&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "MariaG", 2 }
                });

            migrationBuilder.InsertData(
                table: "productos",
                columns: new[] { "id_producto", "descripcion_producto", "fecha_creacion", "intercambio", "nombre_producto", "proceso_negociacion", "usuario_id" },
                values: new object[,]
                {
                    { 1, "Un smartphone de última generación.", new DateTime(2025, 3, 15, 7, 43, 55, 421, DateTimeKind.Utc).AddTicks(197), true, "Smartphone XYZ", false, 1 },
                    { 2, "Una laptop potente para trabajo y juegos.", new DateTime(2025, 3, 15, 7, 43, 55, 421, DateTimeKind.Utc).AddTicks(201), false, "Laptop ABC", true, 2 }
                });

            migrationBuilder.InsertData(
                table: "comentarios",
                columns: new[] { "id_comentario", "comentario_padre_id", "contenido_comentario", "fecha_creacion", "producto_id", "usuario_id" },
                values: new object[] { 1, null, "¡Me encanta este producto!", new DateTime(2025, 3, 15, 7, 43, 55, 420, DateTimeKind.Utc).AddTicks(9974), 2, 1 });

            migrationBuilder.InsertData(
                table: "evaluaciones",
                columns: new[] { "id_evaluacion", "comentario", "fecha_creacion", "producto_id", "puntacion", "usuario_id" },
                values: new object[,]
                {
                    { 1, "Excelente producto, muy recomendado.", new DateTime(2025, 3, 15, 7, 43, 55, 421, DateTimeKind.Utc).AddTicks(25), 2, 5, 1 },
                    { 2, "Buen producto, pero podría mejorar.", new DateTime(2025, 3, 15, 7, 43, 55, 421, DateTimeKind.Utc).AddTicks(29), 1, 4, 2 }
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
                values: new object[] { 1, new DateTime(2025, 3, 15, 7, 43, 55, 421, DateTimeKind.Utc).AddTicks(111), 1, 2, 1 });

            migrationBuilder.InsertData(
                table: "comentarios",
                columns: new[] { "id_comentario", "comentario_padre_id", "contenido_comentario", "fecha_creacion", "producto_id", "usuario_id" },
                values: new object[] { 2, 1, "¿Todavía está disponible?", new DateTime(2025, 3, 15, 7, 43, 55, 420, DateTimeKind.Utc).AddTicks(9983), 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_categorias_producto_categoria_id",
                table: "categorias_producto",
                column: "categoria_id");

            migrationBuilder.CreateIndex(
                name: "IX_categorias_producto_usuario_id",
                table: "categorias_producto",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_comentario_padre_id",
                table: "comentarios",
                column: "comentario_padre_id");

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
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_productos_usuario_id",
                table: "productos",
                column: "usuario_id");
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
                name: "categorias");

            migrationBuilder.DropTable(
                name: "imagenes");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
