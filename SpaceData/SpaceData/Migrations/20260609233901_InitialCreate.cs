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
                    DT_NASCIMENTO = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
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
                    IdMissao = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    NomeMissao = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    DtInicio = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    DuracaoEstimada = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MISSAO", x => x.IdMissao);
                });

            migrationBuilder.CreateTable(
                name: "T_AGENTE_MISSAO",
                columns: table => new
                {
                    IdAgenteMissao = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    IdAgente = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    IdMissao = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    RelatorioMissao = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AGENTE_MISSAO", x => x.IdAgenteMissao);
                    table.ForeignKey(
                        name: "FK_T_AGENTE_MISSAO_T_AGENTE_IdAgente",
                        column: x => x.IdAgente,
                        principalTable: "T_AGENTE",
                        principalColumn: "ID_AGENTE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_AGENTE_MISSAO_T_MISSAO_IdMissao",
                        column: x => x.IdMissao,
                        principalTable: "T_MISSAO",
                        principalColumn: "IdMissao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_AGENTE_MISSAO_IdAgente",
                table: "T_AGENTE_MISSAO",
                column: "IdAgente");

            migrationBuilder.CreateIndex(
                name: "IX_T_AGENTE_MISSAO_IdMissao",
                table: "T_AGENTE_MISSAO",
                column: "IdMissao");
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
