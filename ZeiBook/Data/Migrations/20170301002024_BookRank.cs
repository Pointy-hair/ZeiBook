using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZeiBook.Data.Migrations
{
    public partial class BookRank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CornValue",
                table: "BookRanks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnableTask",
                table: "BookRanks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CornValue",
                table: "BookRanks");

            migrationBuilder.DropColumn(
                name: "EnableTask",
                table: "BookRanks");
        }
    }
}
