using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio_Website_Core.Migrations
{
    public partial class Adding_SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Email", "Name", "PhotoPath" },
                values: new object[] { 8888, 3, "SeedMail@LOL.com", "SeedData = BROKE", null });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Email", "Name", "PhotoPath" },
                values: new object[] { 9999, 3, "SeedMail@LOL.com", "SeedData = BACK", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8888);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9999);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Email", "Name", "PhotoPath" },
                values: new object[] { 1, 3, "SeedMail@LOL.com", "SeedData = BROKE", null });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Email", "Name", "PhotoPath" },
                values: new object[] { 2, 3, "SeedMail@LOL.com", "SeedData = BACK", null });
        }
    }
}
