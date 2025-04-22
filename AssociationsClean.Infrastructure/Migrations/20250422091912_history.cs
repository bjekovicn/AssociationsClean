using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssociationsClean.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class history : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssociationsHistory",
                columns: table => new
                {
                    UserUuid = table.Column<Guid>(type: "uuid", nullable: false),
                    AssociationId = table.Column<int>(type: "integer", nullable: false),
                    AnsweredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociationsHistory", x => new { x.UserUuid, x.AssociationId });
                    table.ForeignKey(
                        name: "FK_AssociationsHistory_Associations_AssociationId",
                        column: x => x.AssociationId,
                        principalTable: "Associations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssociationsHistory_AssociationId",
                table: "AssociationsHistory",
                column: "AssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_AssociationsHistory_UserUuid_AssociationId",
                table: "AssociationsHistory",
                columns: new[] { "UserUuid", "AssociationId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssociationsHistory");
        }
    }
}
