using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rollcall.Migrations
{
    public partial class MealsOnWheels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MealSchemas",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealSchemas", x => x.Name);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordSalt = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Children_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GroupAttendance",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    MealName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Attendance = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupAttendance", x => new { x.GroupId, x.MealName, x.Date });
                    table.ForeignKey(
                        name: "FK_GroupAttendance_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupAttendance_MealSchemas_MealName",
                        column: x => x.MealName,
                        principalTable: "MealSchemas",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChildAttendance",
                columns: table => new
                {
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    MealName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Attendance = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildAttendance", x => new { x.ChildId, x.MealName, x.Date });
                    table.ForeignKey(
                        name: "FK_ChildAttendance_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChildAttendance_MealSchemas_MealName",
                        column: x => x.MealName,
                        principalTable: "MealSchemas",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DefaultAttendance",
                columns: table => new
                {
                    MealName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    Attendance = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultAttendance", x => new { x.ChildId, x.MealName });
                    table.ForeignKey(
                        name: "FK_DefaultAttendance_Children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Children",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DefaultAttendance_MealSchemas_MealName",
                        column: x => x.MealName,
                        principalTable: "MealSchemas",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "AEII" });

            migrationBuilder.InsertData(
                table: "MealSchemas",
                column: "Name",
                values: new object[]
                {
                    "breakfast",
                    "desert",
                    "dinner"
                });

            migrationBuilder.InsertData(
                table: "Children",
                columns: new[] { "Id", "GroupId", "Name", "Surname" },
                values: new object[] { 1, 1, "Kamil", "Kowalski" });

            migrationBuilder.InsertData(
                table: "DefaultAttendance",
                columns: new[] { "ChildId", "MealName", "Attendance" },
                values: new object[] { 1, "breakfast", true });

            migrationBuilder.InsertData(
                table: "DefaultAttendance",
                columns: new[] { "ChildId", "MealName", "Attendance" },
                values: new object[] { 1, "desert", false });

            migrationBuilder.InsertData(
                table: "DefaultAttendance",
                columns: new[] { "ChildId", "MealName", "Attendance" },
                values: new object[] { 1, "dinner", true });

            migrationBuilder.CreateIndex(
                name: "IX_ChildAttendance_MealName",
                table: "ChildAttendance",
                column: "MealName");

            migrationBuilder.CreateIndex(
                name: "IX_Children_GroupId",
                table: "Children",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DefaultAttendance_MealName",
                table: "DefaultAttendance",
                column: "MealName");

            migrationBuilder.CreateIndex(
                name: "IX_GroupAttendance_MealName",
                table: "GroupAttendance",
                column: "MealName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildAttendance");

            migrationBuilder.DropTable(
                name: "DefaultAttendance");

            migrationBuilder.DropTable(
                name: "GroupAttendance");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Children");

            migrationBuilder.DropTable(
                name: "MealSchemas");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
