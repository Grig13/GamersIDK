using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamersChatAPI.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProductComments_Products_ProductId",
            //    table: "ProductComments");

            //migrationBuilder.DropColumn(
            //    name: "ProductId",
            //    table: "ProductComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductComments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalPrice",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProductComments_Products_ProductId",
            //    table: "ProductComments",
            //    column: "ProductId",
            //    principalTable: "Products",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_Products_ProductId",
                table: "ProductComments");

            migrationBuilder.DropIndex(
                name: "IX_ProductComments_ProductId",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Carts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductComments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ProductComments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductComments_ProductId",
            //    table: "ProductComments",
            //    column: "ProductId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProductComments_Products_ProductId",
            //    table: "ProductComments",
            //    column: "ProductId",
            //    principalTable: "Products",
            //    principalColumn: "Id");
        }

    }
}
