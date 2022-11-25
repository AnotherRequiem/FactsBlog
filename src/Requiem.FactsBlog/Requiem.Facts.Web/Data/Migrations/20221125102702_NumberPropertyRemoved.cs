using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Requiem.Facts.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class NumberPropertyRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Facts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Facts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
