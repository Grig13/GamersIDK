using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamersChatAPI.Migrations
{
    public partial class COMEON : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProductComments_Users_UserId",
            //    table: "ProductComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ProductComments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_Users_UserId",
                table: "ProductComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProductComments_Users_UserId",
            //    table: "ProductComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ProductComments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_Users_UserId",
                table: "ProductComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
