using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TituloTrabalho",
                table: "Votacoes",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TituloTrabalho",
                table: "Votacoes");
        }
    }
}
