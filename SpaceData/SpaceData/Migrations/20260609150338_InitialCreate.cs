using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceData.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_AGENTE",
                columns: table => new
                {
                    ID_AGENTE = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    NM_AGENTE = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    DtNascimento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ST_AGENTE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ESPECIALIDADE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AGENTE", x => x.ID_AGENTE);
                });

            migrationBuilder.CreateTable(
                name: "T_MISSAO",
                columns: table => new
                {
                    ID_MISSAO = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    NM_MISSAO = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    DT_INICIO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DURACAO_ESTIMADA = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Descrição = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    ST_MISSAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MISSAO", x => x.ID_MISSAO);
                });

            migrationBuilder.CreateTable(
                name: "T_AGENTE_MISSAO",
                columns: table => new
                {
                    ID_AGENTE_MISSAO = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ID_AGENTE = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ID_MISSAO = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AGENTE_MISSAO", x => x.ID_AGENTE_MISSAO);
                    table.ForeignKey(
                        name: "FK_T_AGENTE_MISSAO_T_AGENTE_ID_AGENTE",
                        column: x => x.ID_AGENTE,
                        principalTable: "T_AGENTE",
                        principalColumn: "ID_AGENTE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_AGENTE_MISSAO_T_MISSAO_ID_MISSAO",
                        column: x => x.ID_MISSAO,
                        principalTable: "T_MISSAO",
                        principalColumn: "ID_MISSAO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_AGENTE_MISSAO_ID_AGENTE",
                table: "T_AGENTE_MISSAO",
                column: "ID_AGENTE");

            migrationBuilder.CreateIndex(
                name: "IX_T_AGENTE_MISSAO_ID_MISSAO",
                table: "T_AGENTE_MISSAO",
                column: "ID_MISSAO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_AGENTE_MISSAO");

            migrationBuilder.DropTable(
                name: "T_AGENTE");

            migrationBuilder.DropTable(
                name: "T_MISSAO");
        }
    }
}
