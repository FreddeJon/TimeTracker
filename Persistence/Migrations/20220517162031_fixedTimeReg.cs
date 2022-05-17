using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class fixedTimeReg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeRegistration_Projects_ProjectId",
                table: "TimeRegistration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeRegistration",
                table: "TimeRegistration");

            migrationBuilder.RenameTable(
                name: "TimeRegistration",
                newName: "TimeRegistrations");

            migrationBuilder.RenameIndex(
                name: "IX_TimeRegistration_ProjectId",
                table: "TimeRegistrations",
                newName: "IX_TimeRegistrations_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeRegistrations",
                table: "TimeRegistrations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRegistrations_Projects_ProjectId",
                table: "TimeRegistrations",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeRegistrations_Projects_ProjectId",
                table: "TimeRegistrations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeRegistrations",
                table: "TimeRegistrations");

            migrationBuilder.RenameTable(
                name: "TimeRegistrations",
                newName: "TimeRegistration");

            migrationBuilder.RenameIndex(
                name: "IX_TimeRegistrations_ProjectId",
                table: "TimeRegistration",
                newName: "IX_TimeRegistration_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeRegistration",
                table: "TimeRegistration",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeRegistration_Projects_ProjectId",
                table: "TimeRegistration",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
