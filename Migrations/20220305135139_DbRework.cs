using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rollcall.Migrations
{
    public partial class DbRework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Masks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealSchemas",
                table: "MealSchemas");

            migrationBuilder.DeleteData(
                table: "MealSchemas",
                keyColumn: "Name",
                keyValue: "breakfast");

            migrationBuilder.DeleteData(
                table: "MealSchemas",
                keyColumn: "Name",
                keyValue: "desert");

            migrationBuilder.DeleteData(
                table: "MealSchemas",
                keyColumn: "Name",
                keyValue: "dinner");

            migrationBuilder.DropColumn(
                name: "Mask",
                table: "MealSchemas");

            migrationBuilder.DropColumn(
                name: "DefaultMeals",
                table: "Children");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MealSchemas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MealSchemas",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealSchemas",
                table: "MealSchemas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ChildAttendance",
                columns: table => new
                {
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    SchemaId = table.Column<int>(type: "int", nullable: false),
                    Attendance = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildAttendance", x => new { x.ChildId, x.MealId, x.Date });
                    table.ForeignKey(
                        name: "FK_ChildAttendance_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChildAttendance_MealSchemas_SchemaId",
                        column: x => x.SchemaId,
                        principalTable: "MealSchemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DefaultAttendance",
                columns: table => new
                {
                    MealId = table.Column<int>(type: "int", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    Attendance = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultAttendance", x => new { x.ChildId, x.MealId });
                    table.ForeignKey(
                        name: "FK_DefaultAttendance_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DefaultAttendance_MealSchemas_MealId",
                        column: x => x.MealId,
                        principalTable: "MealSchemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GroupAttendance",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    SchemaId = table.Column<int>(type: "int", nullable: false),
                    Attendance = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupAttendance", x => new { x.GroupId, x.MealId, x.Date });
                    table.ForeignKey(
                        name: "FK_GroupAttendance_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupAttendance_MealSchemas_SchemaId",
                        column: x => x.SchemaId,
                        principalTable: "MealSchemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "MealSchemas",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "breakfast" });

            migrationBuilder.InsertData(
                table: "MealSchemas",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "dinner" });

            migrationBuilder.InsertData(
                table: "MealSchemas",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "desert" });

            migrationBuilder.InsertData(
                table: "DefaultAttendance",
                columns: new[] { "ChildId", "MealId", "Attendance" },
                values: new object[] { 1, 1, true });

            migrationBuilder.InsertData(
                table: "DefaultAttendance",
                columns: new[] { "ChildId", "MealId", "Attendance" },
                values: new object[] { 1, 2, true });

            migrationBuilder.InsertData(
                table: "DefaultAttendance",
                columns: new[] { "ChildId", "MealId", "Attendance" },
                values: new object[] { 1, 3, false });

            migrationBuilder.CreateIndex(
                name: "IX_ChildAttendance_SchemaId",
                table: "ChildAttendance",
                column: "SchemaId");

            migrationBuilder.CreateIndex(
                name: "IX_DefaultAttendance_MealId",
                table: "DefaultAttendance",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupAttendance_SchemaId",
                table: "GroupAttendance",
                column: "SchemaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildAttendance");

            migrationBuilder.DropTable(
                name: "DefaultAttendance");

            migrationBuilder.DropTable(
                name: "GroupAttendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealSchemas",
                table: "MealSchemas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MealSchemas");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MealSchemas",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<uint>(
                name: "Mask",
                table: "MealSchemas",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "DefaultMeals",
                table: "Children",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealSchemas",
                table: "MealSchemas",
                column: "Name");

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Meals = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => new { x.ChildId, x.Date });
                    table.ForeignKey(
                        name: "FK_Attendance_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Masks",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Meals = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masks", x => new { x.GroupId, x.Date });
                    table.ForeignKey(
                        name: "FK_Masks_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
        }
    }
}
