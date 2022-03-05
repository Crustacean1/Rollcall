using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rollcall.Migrations
{
    public partial class ChildGroupSpecified3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Groups_GroupId1",
                table: "Children");

            migrationBuilder.DropIndex(
                name: "IX_Children_GroupId1",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "GroupId1",
                table: "Children");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId1",
                table: "Children",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Children_GroupId1",
                table: "Children",
                column: "GroupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Groups_GroupId1",
                table: "Children",
                column: "GroupId1",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
