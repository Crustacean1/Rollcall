using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rollcall.Migrations
{
    public partial class SchemaChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MealSchemas",
                table: "MealSchemas");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MealSchemas",
                newName: "Mask");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MealSchemas",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Mask",
                table: "MealSchemas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealSchemas",
                table: "MealSchemas",
                column: "Name");

            migrationBuilder.UpdateData(
                table: "MealSchemas",
                keyColumn: "Name",
                keyValue: "desert",
                column: "Mask",
                value: 4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MealSchemas",
                table: "MealSchemas");

            migrationBuilder.DeleteData(
                table: "MealSchemas",
                keyColumn: "Name",
                keyValue: "desert");

            migrationBuilder.RenameColumn(
                name: "Mask",
                table: "MealSchemas",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MealSchemas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MealSchemas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealSchemas",
                table: "MealSchemas",
                column: "Id");

            migrationBuilder.InsertData(
                table: "MealSchemas",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "desert" });
        }
    }
}
