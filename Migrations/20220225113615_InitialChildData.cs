using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rollcall.Migrations
{
    public partial class InitialChildData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "AEII" });

            migrationBuilder.InsertData(
                table: "Children",
                columns: new[] { "Id", "DefaultMeals", "GroupId", "Name", "Surname" },
                values: new object[] { 1, 3, 1, "Kamil", "Kowalski" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Children",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
