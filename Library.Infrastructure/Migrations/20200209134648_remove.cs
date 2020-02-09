using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Migrations
{
    public partial class remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookDetails_Authors_AuthorID",
                table: "BookDetails");

            migrationBuilder.DropTable(
                name: "BookCopy");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Members",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "BookDetails",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "BookDetails",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_BookDetails_AuthorID",
                table: "BookDetails",
                newName: "IX_BookDetails_AuthorId");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetailsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_BookDetails_DetailsId",
                        column: x => x.DetailsId,
                        principalTable: "BookDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "DetailsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 2 },
                    { 5, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_DetailsId",
                table: "Books",
                column: "DetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookDetails_Authors_AuthorId",
                table: "BookDetails",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookDetails_Authors_AuthorId",
                table: "BookDetails");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Members",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "BookDetails",
                newName: "AuthorID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BookDetails",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_BookDetails_AuthorId",
                table: "BookDetails",
                newName: "IX_BookDetails_AuthorID");

            migrationBuilder.CreateTable(
                name: "BookCopy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetailsID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCopy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookCopy_BookDetails_DetailsID",
                        column: x => x.DetailsID,
                        principalTable: "BookDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookCopy_DetailsID",
                table: "BookCopy",
                column: "DetailsID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookDetails_Authors_AuthorID",
                table: "BookDetails",
                column: "AuthorID",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
