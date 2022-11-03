using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobsAPI.Migrations
{
    public partial class jobs3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Categories_categoryTypeId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Cities_cityTypeId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_States_stateTypeId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Users_userTypeUserID",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_categoryTypeId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_cityTypeId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_stateTypeId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_userTypeUserID",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "categoryTypeId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "cityTypeId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "stateTypeId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "userTypeUserID",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "userType",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userType",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "categoryTypeId",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cityTypeId",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "stateTypeId",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "userTypeUserID",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_categoryTypeId",
                table: "Jobs",
                column: "categoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_cityTypeId",
                table: "Jobs",
                column: "cityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_stateTypeId",
                table: "Jobs",
                column: "stateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_userTypeUserID",
                table: "Jobs",
                column: "userTypeUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Categories_categoryTypeId",
                table: "Jobs",
                column: "categoryTypeId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Cities_cityTypeId",
                table: "Jobs",
                column: "cityTypeId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_States_stateTypeId",
                table: "Jobs",
                column: "stateTypeId",
                principalTable: "States",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Users_userTypeUserID",
                table: "Jobs",
                column: "userTypeUserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
