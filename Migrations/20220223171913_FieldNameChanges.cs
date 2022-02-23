using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace attendance.Migrations
{
    public partial class FieldNameChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_Children_ChildId",
                table: "Days");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DaySchemas",
                table: "DaySchemas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Days",
                table: "Days");

            migrationBuilder.RenameTable(
                name: "DaySchemas",
                newName: "MealSchemas");

            migrationBuilder.RenameTable(
                name: "Days",
                newName: "Attendance");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealSchemas",
                table: "MealSchemas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance",
                columns: new[] { "ChildId", "Date" });

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Children_ChildId",
                table: "Attendance",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Children_ChildId",
                table: "Attendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealSchemas",
                table: "MealSchemas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance");

            migrationBuilder.RenameTable(
                name: "MealSchemas",
                newName: "DaySchemas");

            migrationBuilder.RenameTable(
                name: "Attendance",
                newName: "Days");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DaySchemas",
                table: "DaySchemas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Days",
                table: "Days",
                columns: new[] { "ChildId", "Date" });

            migrationBuilder.AddForeignKey(
                name: "FK_Days_Children_ChildId",
                table: "Days",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
