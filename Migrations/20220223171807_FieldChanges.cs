using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace attendance.Migrations
{
    public partial class FieldChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Breakfast",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "Desert",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "Dinner",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "Breakfast",
                table: "DayMasks");

            migrationBuilder.DropColumn(
                name: "Desert",
                table: "DayMasks");

            migrationBuilder.DropColumn(
                name: "Dinner",
                table: "DayMasks");

            migrationBuilder.AddColumn<int>(
                name: "Meals",
                table: "Days",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Meals",
                table: "DayMasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefaultMeals",
                table: "Children",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DaySchemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaySchemas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Children_GroupId",
                table: "Children",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Groups_GroupId",
                table: "Children",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Groups_GroupId",
                table: "Children");

            migrationBuilder.DropTable(
                name: "DaySchemas");

            migrationBuilder.DropIndex(
                name: "IX_Children_GroupId",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "Meals",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "Meals",
                table: "DayMasks");

            migrationBuilder.DropColumn(
                name: "DefaultMeals",
                table: "Children");

            migrationBuilder.AddColumn<bool>(
                name: "Breakfast",
                table: "Days",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Desert",
                table: "Days",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Dinner",
                table: "Days",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Breakfast",
                table: "DayMasks",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Desert",
                table: "DayMasks",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Dinner",
                table: "DayMasks",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
