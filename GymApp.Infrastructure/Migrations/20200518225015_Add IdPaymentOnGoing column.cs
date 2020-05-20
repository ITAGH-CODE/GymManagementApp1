using Microsoft.EntityFrameworkCore.Migrations;

namespace GymApp.Infrastructure.Migrations
{
    public partial class AddIdPaymentOnGoingcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdPaymentOnGoing",
                table: "Clients",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPaymentOnGoing",
                table: "Clients");
        }
    }
}
