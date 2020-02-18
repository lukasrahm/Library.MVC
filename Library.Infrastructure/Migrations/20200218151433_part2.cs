using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Migrations
{
    public partial class part2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Loans_BookId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LoanId",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "OnLoan",
                table: "Books",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfLoan", "DateOfReturn" },
                values: new object[] { new DateTime(2020, 2, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 3, 3, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookId",
                table: "Loans",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Loans_BookId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "OnLoan",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "LoanId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfLoan", "DateOfReturn" },
                values: new object[] { new DateTime(2020, 2, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 3, 2, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookId",
                table: "Loans",
                column: "BookId",
                unique: true);
        }
    }
}
