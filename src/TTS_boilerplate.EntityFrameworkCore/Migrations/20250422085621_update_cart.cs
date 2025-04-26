using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTS_boilerplate.Migrations
{
    /// <inheritdoc />
    public partial class update_cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Tạo bảng CartTemp mới
            migrationBuilder.CreateTable(
                name: "CartsTemp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    // Thêm các cột khác của bảng Cart
                });

            // 2. Tạo bảng CartItemsTemp mới
            migrationBuilder.CreateTable(
                name: "CartItemsTemp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    // Thêm các cột khác của CartItem
                });

            // 3. Copy dữ liệu từ bảng cũ sang bảng mới (nếu cần)
            if (migrationBuilder.IsSqlServer())
            {
                migrationBuilder.Sql(@"
                INSERT INTO CartsTemp (UserId /*, các cột khác */)
                SELECT UserId /*, các cột khác */ FROM Carts;

                DECLARE @CartMapping TABLE (OldId uniqueidentifier, NewId int);
                INSERT INTO @CartMapping (OldId, NewId)
                SELECT c.Id, ct.Id FROM Carts c
                JOIN CartsTemp ct ON c.UserId = ct.UserId;

                INSERT INTO CartItemsTemp (CartId, ProductId, Quantity /*, các cột khác */)
                SELECT cm.NewId, ci.ProductId, ci.Quantity /*, các cột khác */
                FROM CartItems ci
                JOIN @CartMapping cm ON ci.CartId = cm.OldId;
            ");
            }

            // 4. Xóa các bảng cũ
            migrationBuilder.DropTable(name: "CartItems");
            migrationBuilder.DropTable(name: "Carts");

            // 5. Đổi tên bảng tạm thành tên bảng chính
            migrationBuilder.RenameTable(
                name: "CartsTemp",
                newName: "Carts");

            migrationBuilder.RenameTable(
                name: "CartItemsTemp",
                newName: "CartItems");

            // 6. Thêm các ràng buộc và index
            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1. Tạo bảng tạm với kiểu Guid
            migrationBuilder.CreateTable(
                name: "CartsTemp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    // Thêm các cột khác
                });

            migrationBuilder.CreateTable(
                name: "CartItemsTemp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    // Thêm các cột khác
                });

            // 2. Copy dữ liệu (nếu cần)
            // 3. Xóa bảng cũ
            // 4. Đổi tên bảng
            // 5. Thêm các ràng buộc
            // (Tương tự như Up nhưng ngược lại)
        }
    }
}
