using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class InitialPlanesEstudio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CEF");

            migrationBuilder.CreateTable(
                name: "Carreras",
                schema: "CEF",
                columns: table => new
                {
                    IdCarrera = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaveCarrera = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    NombreCarrera = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AliasCarrera = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdCoordinador = table.Column<int>(type: "int", nullable: true),
                    EstadoCarrera = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.IdCarrera);
                });

            migrationBuilder.CreateTable(
                name: "PlanesEstudio",
                schema: "CEF",
                columns: table => new
                {
                    IdPlanEstudio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanEstudio = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    TotalCreditos = table.Column<int>(type: "int", nullable: false),
                    CreditosOptativos = table.Column<int>(type: "int", nullable: false),
                    CreditosObligatorios = table.Column<int>(type: "int", nullable: false),
                    PerfilDeIngreso = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    PerfelDeEgreso = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CampoOcupacional = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    EstadoPlanEstudio = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IdCarrera = table.Column<int>(type: "int", nullable: false),
                    IdNivelAcademico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanesEstudio", x => x.IdPlanEstudio);
                    table.ForeignKey(
                        name: "FK_PlanesEstudio_Carreras_IdCarrera",
                        column: x => x.IdCarrera,
                        principalSchema: "CEF",
                        principalTable: "Carreras",
                        principalColumn: "IdCarrera",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UK_ClaveCarrera",
                schema: "CEF",
                table: "Carreras",
                column: "ClaveCarrera",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanesEstudio_IdCarrera",
                schema: "CEF",
                table: "PlanesEstudio",
                column: "IdCarrera");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanesEstudio",
                schema: "CEF");

            migrationBuilder.DropTable(
                name: "Carreras",
                schema: "CEF");
        }
    }
}
