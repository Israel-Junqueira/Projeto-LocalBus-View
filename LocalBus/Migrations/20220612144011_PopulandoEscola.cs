using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalBus.Migrations
{
    public partial class PopulandoEscola : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert Into Escola(NomeEscola,TelefoneDaEscola,EmailDaEscola,CodigoDaEtec,MyUserId) " +
" Values ('ETEC Comendador João Rayz','(14) 3641-1310 ','e040adm@cps.sp.gov.br',40,'e5e2f6f9-5337-4e9c-8430-8d17cb6d8cc7')");

            migrationBuilder.Sql("Insert Into Escola(NomeEscola,TelefoneDaEscola,EmailDaEscola,CodigoDaEtec,MyUserId) " +
               " Values ('ETEC  Engenheiro Herval Bellusci','(18) 3521-2494',' e063adm@cps.sp.gov.br',55,'09b6bccf-a56e-4671-b0c0-54d6c23811b1')");

            migrationBuilder.Sql("Insert Into Escola(NomeEscola,TelefoneDaEscola,EmailDaEscola,CodigoDaEtec,MyUserId) " +
               " Values ('ETEC  Arnaldo Pereira Cheregatti','(19) 3652-6204 / 6016','e215adm@cps.sp.gov.br',215,'58ab4f92-e33a-43c9-b826-4c3d7a98ef36')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Escola");
        }
    }
}
