using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.API.Migrations
{
    /// <inheritdoc />
    public partial class bookshelfC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookshelf_c",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    book_id = table.Column<Guid>(type: "TEXT", nullable: true),
                    book_name = table.Column<string>(type: "TEXT", nullable: true),
                    created_by = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_by = table.Column<string>(type: "TEXT", nullable: true),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookshelf_c", x => x.id);
                    table.ForeignKey(
                        name: "FK_Bookshelf_c_Library_book_id",
                        column: x => x.book_id,
                        principalTable: "Library",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookshelf_c_book_id",
                table: "Bookshelf_c",
                column: "book_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookshelf_c");
        }
    }
}
