using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamersChatAPI.Migrations
{
    /// <inheritdoc />
    public partial class DoamneAJUTA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProductComments_Products_ProductId",
            //    table: "ProductComments");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "ProductId",
            //    table: "ProductComments",
            //    nullable: false,  // Make the column nullable
            //    oldClrType: typeof(Guid),
            //    oldType: "uniqueidentifier");

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

        }
    }
}
