using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbySalto.Mid.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_Carts_CartId",
                table: "CartProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_Products_ProductId",
                table: "CartProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartProduct",
                table: "CartProduct");

            migrationBuilder.RenameTable(
                name: "CartProduct",
                newName: "CartProducts");

            migrationBuilder.RenameIndex(
                name: "IX_CartProduct_ProductId",
                table: "CartProducts",
                newName: "IX_CartProducts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartProducts",
                table: "CartProducts",
                columns: new[] { "CartId", "ProductId" });

            migrationBuilder.CreateTable(
                name: "UserFavorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFavorites_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ApiId");
                    table.ForeignKey(
                        name: "FK_UserFavorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "EmailVerifiedAt", "RefreshTokenExpiryTime", "ResetPasswordExpiry", "UpdatedOn" },
                values: new object[] { new DateTime(2025, 4, 9, 4, 44, 20, 184, DateTimeKind.Utc).AddTicks(5989), new DateTime(2025, 4, 9, 4, 44, 20, 184, DateTimeKind.Utc).AddTicks(7472), new DateTime(2025, 4, 9, 4, 44, 20, 184, DateTimeKind.Utc).AddTicks(7860), new DateTime(2025, 4, 9, 4, 44, 20, 184, DateTimeKind.Utc).AddTicks(8239), new DateTime(2025, 4, 9, 4, 44, 20, 184, DateTimeKind.Utc).AddTicks(6284) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "EmailVerifiedAt", "RefreshTokenExpiryTime", "ResetPasswordExpiry", "UpdatedOn" },
                values: new object[] { new DateTime(2025, 4, 9, 4, 44, 20, 184, DateTimeKind.Utc).AddTicks(9258), new DateTime(2025, 4, 9, 4, 44, 20, 184, DateTimeKind.Utc).AddTicks(9261), new DateTime(2025, 4, 9, 4, 44, 20, 184, DateTimeKind.Utc).AddTicks(9262), new DateTime(2025, 4, 9, 4, 44, 20, 184, DateTimeKind.Utc).AddTicks(9263), new DateTime(2025, 4, 9, 4, 44, 20, 184, DateTimeKind.Utc).AddTicks(9259) });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_ProductId",
                table: "UserFavorites",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_UserId",
                table: "UserFavorites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_Carts_CartId",
                table: "CartProducts",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_Products_ProductId",
                table: "CartProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ApiId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Carts_CartId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Products_ProductId",
                table: "CartProducts");

            migrationBuilder.DropTable(
                name: "UserFavorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartProducts",
                table: "CartProducts");

            migrationBuilder.RenameTable(
                name: "CartProducts",
                newName: "CartProduct");

            migrationBuilder.RenameIndex(
                name: "IX_CartProducts_ProductId",
                table: "CartProduct",
                newName: "IX_CartProduct_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartProduct",
                table: "CartProduct",
                columns: new[] { "CartId", "ProductId" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "EmailVerifiedAt", "RefreshTokenExpiryTime", "ResetPasswordExpiry", "UpdatedOn" },
                values: new object[] { new DateTime(2025, 4, 9, 4, 28, 26, 838, DateTimeKind.Utc).AddTicks(7529), new DateTime(2025, 4, 9, 4, 28, 26, 838, DateTimeKind.Utc).AddTicks(9176), new DateTime(2025, 4, 9, 4, 28, 26, 838, DateTimeKind.Utc).AddTicks(9602), new DateTime(2025, 4, 9, 4, 28, 26, 839, DateTimeKind.Utc).AddTicks(18), new DateTime(2025, 4, 9, 4, 28, 26, 838, DateTimeKind.Utc).AddTicks(7863) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "EmailVerifiedAt", "RefreshTokenExpiryTime", "ResetPasswordExpiry", "UpdatedOn" },
                values: new object[] { new DateTime(2025, 4, 9, 4, 28, 26, 839, DateTimeKind.Utc).AddTicks(1185), new DateTime(2025, 4, 9, 4, 28, 26, 839, DateTimeKind.Utc).AddTicks(1188), new DateTime(2025, 4, 9, 4, 28, 26, 839, DateTimeKind.Utc).AddTicks(1189), new DateTime(2025, 4, 9, 4, 28, 26, 839, DateTimeKind.Utc).AddTicks(1190), new DateTime(2025, 4, 9, 4, 28, 26, 839, DateTimeKind.Utc).AddTicks(1186) });

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_Carts_CartId",
                table: "CartProduct",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_Products_ProductId",
                table: "CartProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ApiId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
