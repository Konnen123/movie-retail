using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRetail.Migrations
{
    public partial class AddMoviesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ImageContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    VideoContentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movies");
        }
    }
}
