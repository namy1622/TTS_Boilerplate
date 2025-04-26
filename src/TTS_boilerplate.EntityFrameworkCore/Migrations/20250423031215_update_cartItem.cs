using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTS_boilerplate.Migrations
{
    /// <inheritdoc />
    public partial class update_cartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId1",
                table: "CartItems",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_AppProducts_ProductId1",
                table: "CartItems",
                column: "ProductId1",
                principalTable: "AppProducts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_AppProducts_ProductId1",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductId1",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "CartItems");

           
        }
    }
}
