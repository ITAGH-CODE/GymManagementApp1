using Microsoft.EntityFrameworkCore.Migrations;

namespace GymApp.Infrastructure.Migrations
{
    public partial class updatetablesclientsandpayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdClient",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Payments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "IdClient",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
