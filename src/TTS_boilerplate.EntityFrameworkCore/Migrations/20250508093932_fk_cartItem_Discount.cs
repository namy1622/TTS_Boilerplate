using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTS_boilerplate.Migrations
{
    /// <inheritdoc />
    public partial class fk_cartItem_Discount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_DiscountId",
                table: "CartItems",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Discounts_DiscountId",
                table: "CartItems",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Discounts_DiscountId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_DiscountId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "CartItems");
        }
    }
}
