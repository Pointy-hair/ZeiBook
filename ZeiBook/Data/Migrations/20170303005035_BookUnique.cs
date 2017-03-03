using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZeiBook.Data.Migrations
{
    public partial class BookUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Books",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Books",
                nullable: false);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Books_Author_Name",
                table: "Books",
                columns: new[] { "Author", "Name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Books_Author_Name",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Books",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Books",
                nullable: false);
        }
    }
}
