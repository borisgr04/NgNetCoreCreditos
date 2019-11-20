using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NgNetCore.Data.Migrations
{
    public partial class DomainModelInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Identidad = table.Column<string>(nullable: false),
                    NombreCompleto = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Identidad);
                });

            migrationBuilder.CreateTable(
                name: "Creditos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    NumeroCuotas = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    ValorCredito = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creditos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Creditos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Identidad",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cuota",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroCuota = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    ValorCuota = table.Column<decimal>(nullable: false),
                    SaldoCuota = table.Column<decimal>(nullable: false),
                    CreditoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuota", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuota_Creditos_CreditoId",
                        column: x => x.CreditoId,
                        principalTable: "Creditos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Creditos_ClienteId",
                table: "Creditos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_CreditoId",
                table: "Cuota",
                column: "CreditoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cuota");

            migrationBuilder.DropTable(
                name: "Creditos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
