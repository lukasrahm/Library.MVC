using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Migrations
{
    public partial class part4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateReturned",
                table: "Loans");

            migrationBuilder.AddColumn<bool>(
                name: "Returned",
                table: "Loans",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfLoan", "DateOfReturn" },
                values: new object[] { new DateTime(2020, 2, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 3, 4, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Returned",
                table: "Loans");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateReturned",
                table: "Loans",
                type: "Date",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfLoan", "DateOfReturn" },
                values: new object[] { new DateTime(2020, 2, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 3, 3, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
