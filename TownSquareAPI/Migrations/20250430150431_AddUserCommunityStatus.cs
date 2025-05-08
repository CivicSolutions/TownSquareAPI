using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TownSquareAPI.Migrations;

/// <inheritdoc />
public partial class AddUserCommunityStatus : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "Status",
            table: "UserCommunity",
            type: "integer",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Status",
            table: "UserCommunity");
    }
}
