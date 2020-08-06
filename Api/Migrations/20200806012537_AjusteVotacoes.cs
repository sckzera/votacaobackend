using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class AjusteVotacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TituloTrabalho",
                table: "Votacoes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TituloTrabalho",
                table: "Votacoes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
