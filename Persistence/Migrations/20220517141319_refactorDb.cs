using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class refactorDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeRegistration_Customers_CustomerId",
                table: "TimeRegistration");

            migrationBuilder.DropIndex(
                name: "IX_TimeRegistration_CustomerId",
                table: "TimeRegistration");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "TimeRegistration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "TimeRegistration",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TimeRegistration_CustomerId",
                table: "TimeRegistration",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRegistration_Customers_CustomerId",
                table: "TimeRegistration",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
