using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTS_boilerplate.Migrations
{
    /// <inheritdoc />
    public partial class update_order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_AppProducts_ProductId1",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId1",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_AppProducts_ProductId1",
                table: "CartItems",
                column: "ProductId1",
                principalTable: "AppProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_AppProducts_ProductId1",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId1",
                table: "CartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_AppProducts_ProductId1",
                table: "CartItems",
                column: "ProductId1",
                principalTable: "AppProducts",
                principalColumn: "Id");
        }
    }
}
