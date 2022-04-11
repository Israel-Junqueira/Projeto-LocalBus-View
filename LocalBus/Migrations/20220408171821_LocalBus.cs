using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalBus.Migrations
{
    public partial class LocalBus : Migration
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
                    CodigoDaEtec = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escola", x => x.EscolaId);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    TipoImagemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extencao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Length = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.TipoImagemId);
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
                    DescriçãoPonto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type:"nvarchar(100)",nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pontos", x => x.PontoId);
                });

            migrationBuilder.CreateTable(
                name: "ImagemEscola",
                columns: table => new
                {
                    ImagemEscolaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EscolaId = table.Column<int>(type: "int", nullable: false),
                    ImagemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagemEscola", x => x.ImagemEscolaId);
                    table.ForeignKey(
                        name: "FK_ImagemEscola_Escola_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escola",
                        principalColumn: "EscolaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImagemEscola_Image_ImagemId",
                        column: x => x.ImagemId,
                        principalTable: "Image",
                        principalColumn: "TipoImagemId",
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
                name: "IX_EscolasPontos_EscolaId",
                table: "EscolasPontos",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_EscolasPontos_PontoId",
                table: "EscolasPontos",
                column: "PontoId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagemEscola_EscolaId",
                table: "ImagemEscola",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagemEscola_ImagemId",
                table: "ImagemEscola",
                column: "ImagemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EscolasPontos");

            migrationBuilder.DropTable(
                name: "ImagemEscola");

            migrationBuilder.DropTable(
                name: "Pontos");

            migrationBuilder.DropTable(
                name: "Escola");

            migrationBuilder.DropTable(
                name: "Image");
        }
    }
}
