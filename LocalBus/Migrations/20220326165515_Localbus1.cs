using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalBus.Migrations
{
    public partial class Localbus1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Escola",
                columns: table => new
                {
                    EscolaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeEscola = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TelefoneDaEscola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailDaEscola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoDaEtec = table.Column<int>(type: "int", nullable: false),
                    ImagemUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escola", x => x.EscolaId);
                });

            migrationBuilder.CreateTable(
                name: "Pontos",
                columns: table => new
                {
                    PontoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    latitudePonto = table.Column<double>(type: "float", nullable: false),
                    LongitudePonto = table.Column<double>(type: "float", nullable: false),
                    AtivoPonto = table.Column<bool>(type: "bit", nullable: false),
                    DescriçãoPonto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pontos", x => x.PontoId);
                });

            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    AdministradorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeAdministrador = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TelefoneAdmnistrador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAdministrador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPFAdministrador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EscolaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.AdministradorId);
                    table.ForeignKey(
                        name: "FK_Administrador_Escola_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escola",
                        principalColumn: "EscolaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EscolasPontos",
                columns: table => new
                {
                    EscolaPontoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EscolaId = table.Column<int>(type: "int", nullable: false),
                    PontoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscolasPontos", x => x.EscolaPontoId);
                    table.ForeignKey(
                        name: "FK_EscolasPontos_Escola_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escola",
                        principalColumn: "EscolaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EscolasPontos_Pontos_PontoId",
                        column: x => x.PontoId,
                        principalTable: "Pontos",
                        principalColumn: "PontoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrador_EscolaId",
                table: "Administrador",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_EscolasPontos_EscolaId",
                table: "EscolasPontos",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_EscolasPontos_PontoId",
                table: "EscolasPontos",
                column: "PontoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "EscolasPontos");

            migrationBuilder.DropTable(
                name: "Escola");

            migrationBuilder.DropTable(
                name: "Pontos");
        }
    }
}
