using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamersChatAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserToProductComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProductComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "ProductComments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_UserId1",
                table: "ProductComments",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_Users_UserId1",
                table: "ProductComments",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_Users_UserId1",
                table: "ProductComments");

            migrationBuilder.DropIndex(
                name: "IX_ProductComments_UserId1",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ProductComments");
        }
    }
}
