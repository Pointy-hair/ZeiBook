using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZeiBook.Data.Migrations
{
    public partial class RankInterval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interval",
                table: "BookRanks");

            migrationBuilder.AddColumn<long>(
                name: "DurationTicks",
                table: "BookRanks",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationTicks",
                table: "BookRanks");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Interval",
                table: "BookRanks",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
