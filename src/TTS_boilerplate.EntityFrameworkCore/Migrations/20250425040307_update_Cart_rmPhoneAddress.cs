using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTS_boilerplate.Migrations
{
    /// <inheritdoc />
    public partial class update_Cart_rmPhoneAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressCustomer",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "NameCustomer",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "PhoneCustomer",
                table: "CartItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressCustomer",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameCustomer",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneCustomer",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
