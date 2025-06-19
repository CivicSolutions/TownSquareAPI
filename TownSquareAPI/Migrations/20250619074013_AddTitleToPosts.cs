using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TownSquareAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleToPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Post",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Post");
        }
    }
}
