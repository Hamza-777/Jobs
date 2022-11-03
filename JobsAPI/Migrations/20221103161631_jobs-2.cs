using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobsAPI.Migrations
{
    public partial class jobs2 : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_Jobs_categoryTypeId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "category",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "categoryTypeId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "stateTypeId",
                table: "Jobs",
                newName: "stateid");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "Jobs",
                newName: "cityid");

            migrationBuilder.RenameColumn(
                name: "cityTypeId",
                table: "Jobs",
                newName: "categoryid");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_stateTypeId",
                table: "Jobs",
                newName: "IX_Jobs_stateid");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_cityTypeId",
                table: "Jobs",
                newName: "IX_Jobs_categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_cityid",
                table: "Jobs",
                column: "cityid");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Categories_categoryid",
                table: "Jobs",
                column: "categoryid",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Cities_cityid",
                table: "Jobs",
                column: "cityid",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_States_stateid",
                table: "Jobs",
                column: "stateid",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Categories_categoryid",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Cities_cityid",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_States_stateid",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_cityid",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "stateid",
                table: "Jobs",
                newName: "stateTypeId");

            migrationBuilder.RenameColumn(
                name: "cityid",
                table: "Jobs",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "categoryid",
                table: "Jobs",
                newName: "cityTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_stateid",
                table: "Jobs",
                newName: "IX_Jobs_stateTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Jobs_categoryid",
                table: "Jobs",
                newName: "IX_Jobs_cityTypeId");

            migrationBuilder.AddColumn<int>(
                name: "category",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "categoryTypeId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "city",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_categoryTypeId",
                table: "Jobs",
                column: "categoryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Categories_categoryTypeId",
                table: "Jobs",
                column: "categoryTypeId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Cities_cityTypeId",
                table: "Jobs",
                column: "cityTypeId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_States_stateTypeId",
                table: "Jobs",
                column: "stateTypeId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
