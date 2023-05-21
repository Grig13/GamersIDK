using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamersChatAPI.Migrations
{
    /// <inheritdoc />
    public partial class shopmigrationv4 : Migration
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
            //    onDelete: ReferentialAction.SetDefault);

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProductComments_Products_ProductId",
            //    table: "ProductComments");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "ProductIdNullable",
            //    table: "ProductComments",
            //    type: "uniqueidentifier",
            //    nullable: true);

            //migrationBuilder.Sql("UPDATE ProductComments SET ProductIdNullable = ProductId");

            //migrationBuilder.DropColumn(
            //    name: "ProductId",
            //    table: "ProductComments");

            //migrationBuilder.RenameColumn(
            //    name: "ProductIdNullable",
            //    table: "ProductComments",
            //    newName: "ProductId");

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
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProductComments_Products_ProductId",
            //    table: "ProductComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductComments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProductComments_Products_ProductId",
            //    table: "ProductComments",
            //    column: "ProductId",
            //    principalTable: "Products",
            //    principalColumn: "Id");
        }
    }
}
