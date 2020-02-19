using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Migrations
{
    public partial class returnedtrue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1,
                column: "Returned",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1,
                column: "Returned",
                value: false);
        }
    }
}
