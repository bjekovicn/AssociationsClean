using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssociationsClean.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AssociationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Association_Categories_CategoryId",
                table: "Association");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Association",
                table: "Association");

            migrationBuilder.RenameTable(
                name: "Association",
                newName: "Associations");

            migrationBuilder.RenameIndex(
                name: "IX_Association_Name_CategoryId",
                table: "Associations",
                newName: "IX_Associations_Name_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Association_CategoryId",
                table: "Associations",
                newName: "IX_Associations_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Associations",
                table: "Associations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_Categories_CategoryId",
                table: "Associations",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associations_Categories_CategoryId",
                table: "Associations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Associations",
                table: "Associations");

            migrationBuilder.RenameTable(
                name: "Associations",
                newName: "Association");

            migrationBuilder.RenameIndex(
                name: "IX_Associations_Name_CategoryId",
                table: "Association",
                newName: "IX_Association_Name_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Associations_CategoryId",
                table: "Association",
                newName: "IX_Association_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Association",
                table: "Association",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Association_Categories_CategoryId",
                table: "Association",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
