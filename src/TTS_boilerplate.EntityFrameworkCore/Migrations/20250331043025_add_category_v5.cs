using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTS_boilerplate.Migrations
{
    /// <inheritdoc />
    public partial class add_category_v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "AppProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCategory = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_CategoryId",
                table: "AppProducts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_AppCategory_CategoryId",
                table: "AppProducts",
                column: "CategoryId",
                principalTable: "AppCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_AppCategory_CategoryId",
                table: "AppProducts");

            migrationBuilder.DropTable(
                name: "AppCategory");

            migrationBuilder.DropIndex(
                name: "IX_AppProducts_CategoryId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "AppProducts");
        }
    }
}
