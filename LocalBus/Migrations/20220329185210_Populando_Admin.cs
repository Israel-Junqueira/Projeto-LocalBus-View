using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalBus.Migrations
{
    public partial class Populando_Admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Administrador(EscolaId,NomeAdministrador,TelefoneAdmnistrador,EmailAdministrador,CPFAdministrador,Login,Senha)" +
                "VALUES(1,'Israel Ribeiro','14996983584','israelribeiro313@gmail.com','999.999.999-99','admin','admin123')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Administrador");
        }
    }
}
