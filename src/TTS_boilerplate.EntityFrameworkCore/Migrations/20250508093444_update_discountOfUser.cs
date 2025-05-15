using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTS_boilerplate.Migrations
{
    /// <inheritdoc />
    public partial class update_discountOfUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountOfUsers_AbpUsers_UserId1",
                table: "DiscountOfUsers");

            migrationBuilder.DropIndex(
                name: "IX_DiscountOfUsers_UserId1",
                table: "DiscountOfUsers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "DiscountOfUsers");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "DiscountOfUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountOfUsers_UserId",
                table: "DiscountOfUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountOfUsers_AbpUsers_UserId",
                table: "DiscountOfUsers",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountOfUsers_AbpUsers_UserId",
                table: "DiscountOfUsers");

            migrationBuilder.DropIndex(
                name: "IX_DiscountOfUsers_UserId",
                table: "DiscountOfUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DiscountOfUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "DiscountOfUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountOfUsers_UserId1",
                table: "DiscountOfUsers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountOfUsers_AbpUsers_UserId1",
                table: "DiscountOfUsers",
                column: "UserId1",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }
    }
}
