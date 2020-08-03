using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class totalVotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalVotos",
                table: "Votacoes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalVotos",
                table: "Votacoes");
        }
    }
}
