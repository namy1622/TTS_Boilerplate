using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTS_boilerplate.Migrations
{
    /// <inheritdoc />
    public partial class fk_cartItem_customerInfomation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerInformationId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CustomerInformationId",
                table: "CartItems",
                column: "CustomerInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_CustomerInformation_CustomerInformationId",
                table: "CartItems",
                column: "CustomerInformationId",
                principalTable: "CustomerInformation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_CustomerInformation_CustomerInformationId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CustomerInformationId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CustomerInformationId",
                table: "CartItems");
        }
    }
}
