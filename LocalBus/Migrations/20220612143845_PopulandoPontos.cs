using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalBus.Migrations
{
    public partial class PopulandoPontos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Pontos (latitudePonto,LongitudePonto,AtivoPonto,DescriçãoPonto,Nome) " +
         " VALUES (-22.521124, -48.565579,1,'Ponto em frente a jarrier presentes','Ponto: em frente a Jarrier Presentes')");

            migrationBuilder.Sql("INSERT INTO Pontos (latitudePonto,LongitudePonto,AtivoPonto,DescriçãoPonto,Nome) " +
               " VALUES (-22.524385, -48.566264,1,'Ponto em frente o Supermercado J Serve Bem','Ponto: Supermercado J serve Bem')");

            migrationBuilder.Sql("INSERT INTO Pontos (latitudePonto,LongitudePonto,AtivoPonto,DescriçãoPonto,Nome) " +
               " VALUES (-22.513651,-48.552940,1,'Ponto proximo as Escadas do Bairro Nossa gente','Ponto: Na escadinha do Bairro nossa gente')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Pontos");
        }
    }
}
