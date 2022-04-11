using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalBus.Migrations
{
    public partial class PopularEscola : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert Into Escola(NomeEscola,TelefoneDaEscola,EmailDaEscola,CodigoDaEtec) " +
                " Values ('ETEC Comendador João Rayz','(14) 3641-1310 ','e040adm@cps.sp.gov.br',40)");

            migrationBuilder.Sql("Insert Into Escola(NomeEscola,TelefoneDaEscola,EmailDaEscola,CodigoDaEtec) " +
               " Values ('ETEC  Engenheiro Herval Bellusci','(18) 3521-2494',' e063adm@cps.sp.gov.br',55)");

            migrationBuilder.Sql("Insert Into Escola(NomeEscola,TelefoneDaEscola,EmailDaEscola,CodigoDaEtec) " +
               " Values ('ETEC  Arnaldo Pereira Cheregatti','(19) 3652-6204 / 6016','e215adm@cps.sp.gov.br',215)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Escola");
        }
    }
}
