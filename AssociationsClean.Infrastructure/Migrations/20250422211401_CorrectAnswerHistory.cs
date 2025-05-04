using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssociationsClean.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectAnswerHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AnsweredCorrectly",
                table: "AssociationsHistory",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnsweredCorrectly",
                table: "AssociationsHistory");
        }
    }
}
