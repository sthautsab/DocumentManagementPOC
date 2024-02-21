using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentsPOC.Migrations
{
    /// <inheritdoc />
    public partial class document_parentId_and_selection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelectable",
                table: "Documents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Documents",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelectable",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Documents");
        }
    }
}
