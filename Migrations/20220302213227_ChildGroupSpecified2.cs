using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rollcall.Migrations
{
    public partial class ChildGroupSpecified2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<uint>(
                name: "Mask",
                table: "MealSchemas",
                type: "int unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<uint>(
                name: "Meals",
                table: "Masks",
                type: "int unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<uint>(
                name: "DefaultMeals",
                table: "Children",
                type: "int unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GroupId1",
                table: "Children",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<uint>(
                name: "Meals",
                table: "Attendance",
                type: "int unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 1,
                column: "DefaultMeals",
                value: 3u);

            migrationBuilder.UpdateData(
                table: "MealSchemas",
                keyColumn: "Name",
                keyValue: "breakfast",
                column: "Mask",
                value: 1u);

            migrationBuilder.UpdateData(
                table: "MealSchemas",
                keyColumn: "Name",
                keyValue: "desert",
                column: "Mask",
                value: 4u);

            migrationBuilder.UpdateData(
                table: "MealSchemas",
                keyColumn: "Name",
                keyValue: "dinner",
                column: "Mask",
                value: 2u);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "Mask",
                table: "MealSchemas",
                type: "int",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "int unsigned");

            migrationBuilder.AlterColumn<int>(
                name: "Meals",
                table: "Masks",
                type: "int",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "int unsigned");

            migrationBuilder.AlterColumn<int>(
                name: "DefaultMeals",
                table: "Children",
                type: "int",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "int unsigned");

            migrationBuilder.AlterColumn<int>(
                name: "Meals",
                table: "Attendance",
                type: "int",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "int unsigned");

            migrationBuilder.UpdateData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 1,
                column: "DefaultMeals",
                value: 3);

            migrationBuilder.UpdateData(
                table: "MealSchemas",
                keyColumn: "Name",
                keyValue: "breakfast",
                column: "Mask",
                value: 1);

            migrationBuilder.UpdateData(
                table: "MealSchemas",
                keyColumn: "Name",
                keyValue: "desert",
                column: "Mask",
                value: 4);

            migrationBuilder.UpdateData(
                table: "MealSchemas",
                keyColumn: "Name",
                keyValue: "dinner",
                column: "Mask",
                value: 2);
        }
    }
}
