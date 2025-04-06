using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.API.Migrations
{
    /// <inheritdoc />
    public partial class AddLibraryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Library",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    book_name = table.Column<string>(type: "TEXT", nullable: false),
                    author = table.Column<string>(type: "TEXT", nullable: false),
                    type = table.Column<string>(type: "TEXT", nullable: false),
                    volume = table.Column<int>(type: "INTEGER", nullable: false),
                    publisher = table.Column<string>(type: "TEXT", nullable: false),
                    publication_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    created_by = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_by = table.Column<string>(type: "TEXT", nullable: true),
                    updated_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Library", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Library");
        }
    }
}
