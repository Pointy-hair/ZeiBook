using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZeiBook.Data.Migrations
{
    public partial class UniqueChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Categories_Name",
                table: "Categories");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Books_Author_Name",
                table: "Books");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Authors_Name",
                table: "Authors");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Author_Name",
                table: "Books",
                columns: new[] { "Author", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Name",
                table: "Authors",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Books_Author_Name",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Authors_Name",
                table: "Authors");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Categories_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Books_Author_Name",
                table: "Books",
                columns: new[] { "Author", "Name" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Authors_Name",
                table: "Authors",
                column: "Name");
        }
    }
}
