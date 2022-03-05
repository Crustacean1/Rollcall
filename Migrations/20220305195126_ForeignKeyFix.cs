using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rollcall.Migrations
{
    public partial class ForeignKeyFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildAttendance_MealSchemas_SchemaId",
                table: "ChildAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupAttendance_MealSchemas_SchemaId",
                table: "GroupAttendance");

            migrationBuilder.DropIndex(
                name: "IX_GroupAttendance_SchemaId",
                table: "GroupAttendance");

            migrationBuilder.DropIndex(
                name: "IX_ChildAttendance_SchemaId",
                table: "ChildAttendance");

            migrationBuilder.DropColumn(
                name: "SchemaId",
                table: "GroupAttendance");

            migrationBuilder.DropColumn(
                name: "SchemaId",
                table: "ChildAttendance");

            migrationBuilder.CreateIndex(
                name: "IX_GroupAttendance_MealId",
                table: "GroupAttendance",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildAttendance_MealId",
                table: "ChildAttendance",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildAttendance_MealSchemas_MealId",
                table: "ChildAttendance",
                column: "MealId",
                principalTable: "MealSchemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupAttendance_MealSchemas_MealId",
                table: "GroupAttendance",
                column: "MealId",
                principalTable: "MealSchemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildAttendance_MealSchemas_MealId",
                table: "ChildAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupAttendance_MealSchemas_MealId",
                table: "GroupAttendance");

            migrationBuilder.DropIndex(
                name: "IX_GroupAttendance_MealId",
                table: "GroupAttendance");

            migrationBuilder.DropIndex(
                name: "IX_ChildAttendance_MealId",
                table: "ChildAttendance");

            migrationBuilder.AddColumn<int>(
                name: "SchemaId",
                table: "GroupAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchemaId",
                table: "ChildAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GroupAttendance_SchemaId",
                table: "GroupAttendance",
                column: "SchemaId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildAttendance_SchemaId",
                table: "ChildAttendance",
                column: "SchemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildAttendance_MealSchemas_SchemaId",
                table: "ChildAttendance",
                column: "SchemaId",
                principalTable: "MealSchemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupAttendance_MealSchemas_SchemaId",
                table: "GroupAttendance",
                column: "SchemaId",
                principalTable: "MealSchemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
