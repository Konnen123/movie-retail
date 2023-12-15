using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRetail.Migrations
{
    public partial class ChangeGenreType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Movies SET Genre = 10 WHERE Genre = 'Crime'");
            migrationBuilder.Sql("UPDATE Movies SET Genre = 1 WHERE Genre = 'Comedy'");
            migrationBuilder.Sql("UPDATE Movies SET Genre = 2 WHERE Genre = 'Drama'");
            migrationBuilder.AlterColumn<int>(
                name: "Genre",
                table: "movies",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

           
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "movies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
