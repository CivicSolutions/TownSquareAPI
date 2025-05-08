using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TownSquareAPI.Migrations;

/// <inheritdoc />
public partial class MakeSomeAttributesNullable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "User",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text");

        migrationBuilder.AlterColumn<int>(
            name: "CommunityId",
            table: "Post",
            type: "integer",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "integer",
            oldNullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "User",
            type: "text",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<int>(
            name: "CommunityId",
            table: "Post",
            type: "integer",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "integer");
    }
}
