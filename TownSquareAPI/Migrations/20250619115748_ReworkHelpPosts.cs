using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TownSquareAPI.Migrations
{
    /// <inheritdoc />
    public partial class ReworkHelpPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "HelpPost",
                newName: "Content");

            migrationBuilder.AddColumn<int>(
                name: "CommunityId",
                table: "HelpPost",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HelpPost_CommunityId",
                table: "HelpPost",
                column: "CommunityId");

            migrationBuilder.AddForeignKey(
                name: "FK_HelpPost_Community_CommunityId",
                table: "HelpPost",
                column: "CommunityId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HelpPost_Community_CommunityId",
                table: "HelpPost");

            migrationBuilder.DropIndex(
                name: "IX_HelpPost_CommunityId",
                table: "HelpPost");

            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "HelpPost");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "HelpPost",
                newName: "Description");
        }
    }
}
