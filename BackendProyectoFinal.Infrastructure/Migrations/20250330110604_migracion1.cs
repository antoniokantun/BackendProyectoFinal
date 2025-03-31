using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProyectoFinal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migracion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "evaluaciones",
                keyColumn: "id_evaluacion",
                keyValue: 1,
                column: "fecha_creacion",
                value: new DateTime(2025, 3, 30, 6, 6, 2, 976, DateTimeKind.Local).AddTicks(9583));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "evaluaciones",
                keyColumn: "id_evaluacion",
                keyValue: 1,
                column: "fecha_creacion",
                value: new DateTime(2025, 3, 30, 0, 2, 52, 148, DateTimeKind.Local).AddTicks(3330));
        }
    }
}
