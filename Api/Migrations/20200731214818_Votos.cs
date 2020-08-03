using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class Votos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalVotos",
                table: "Votacoes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalVotos",
                table: "Votacoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
