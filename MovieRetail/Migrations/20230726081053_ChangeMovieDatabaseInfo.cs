using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRetail.Migrations
{
    public partial class ChangeMovieDatabaseInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoContentType",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "VideoData",
                table: "movies");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "movies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "movies",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "VideoLink",
                table: "movies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "VideoLink",
                table: "movies");

            migrationBuilder.AddColumn<string>(
                name: "VideoContentType",
                table: "movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "VideoData",
                table: "movies",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
