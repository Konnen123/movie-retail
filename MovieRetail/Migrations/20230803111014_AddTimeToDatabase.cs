using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRetail.Migrations
{
    public partial class AddTimeToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "movieComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "movieComments");
        }
    }
}
