using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalBus.Migrations
{
    public partial class Populando_Escola : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Escola(NomeEscola,TelefoneDaEscola,EmailDaEscola,CodigoDaEtec,ImagemUrl,ImageThumbnailUrl)" +
                "VALUES('Etec Comendador João Rays','3641-5600', 'e040adm@cps.sp.gov.br',040,'https://www.cps.sp.gov.br/wp-content/uploads/sites/1/2020/10/Etec-Comendador-Joa%CC%83o-Rays-400x300.jpg','https://www.cps.sp.gov.br/wp-content/uploads/sites/1/2020/10/Etec-Comendador-Joa%CC%83o-Rays-400x300.jpg')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Escola");
        }
    }
}
