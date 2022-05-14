using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rollcall.Migrations
{
    public partial class PolishCollationv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "MealSchemas",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealSchemas", x => x.Name);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci"),
                    PasswordSalt = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci"),
                    Surname = table.Column<string>(type: "longtext", nullable: false, collation: "utf8_general_ci"),
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
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "GroupAttendance",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    MealName = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8_general_ci"),
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
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "ChildAttendance",
                columns: table => new
                {
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    MealName = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8_general_ci"),
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
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "DefaultAttendance",
                columns: table => new
                {
                    MealName = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8_general_ci"),
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
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.InsertData(
                table: "MealSchemas",
                column: "Name",
                value: "breakfast");

            migrationBuilder.InsertData(
                table: "MealSchemas",
                column: "Name",
                value: "desert");

            migrationBuilder.InsertData(
                table: "MealSchemas",
                column: "Name",
                value: "dinner");

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
