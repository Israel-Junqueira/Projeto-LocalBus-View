using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalBus.Migrations
{
    public partial class Populando_Ponto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Pontos(latitudePonto,LongitudePonto,AtivoPonto,DescriçãoPonto)" +
                "VALUES(-22.521134,-48565617,1,'Ponto em Frente a Jarrier Presentes')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Ponto");
        }
    }
}
