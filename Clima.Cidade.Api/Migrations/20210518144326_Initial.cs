using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clima.Cidade.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClimaCidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Cidade = table.Column<string>(maxLength: 80, nullable: true),
                    TemperaturaAtual = table.Column<double>(nullable: false),
                    TemperaturaMin = table.Column<double>(nullable: false),
                    TemperaturaMax = table.Column<double>(nullable: false),
                    DataRegistro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClimaCidades", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClimaCidades");
        }
    }
}
