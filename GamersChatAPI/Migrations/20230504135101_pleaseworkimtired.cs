using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamersChatAPI.Migrations
{
    /// <inheritdoc />
    public partial class pleaseworkimtired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProductComments_Products_ProductId",
            //    table: "ProductComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductComments",
                type: "uniqueidentifier",
                nullable: true,  // Set to nullable
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProductComments_Products_ProductId",
            //    table: "ProductComments",
            //    column: "ProductId",
            //    principalTable: "Products",
            //    principalColumn: "Id");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_Products_ProductId",
                table: "ProductComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductComments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProductComments_Products_ProductId",
            //    table: "ProductComments",
            //    column: "ProductId",
            //    principalTable: "Products",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
