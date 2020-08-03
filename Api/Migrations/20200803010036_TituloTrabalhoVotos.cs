using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class TituloTrabalhoVotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TituloTrabalho",
                table: "Votos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TituloTrabalho",
                table: "Votos");
        }
    }
}
