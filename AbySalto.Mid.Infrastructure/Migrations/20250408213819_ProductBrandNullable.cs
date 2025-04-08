using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbySalto.Mid.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductBrandNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "EmailVerifiedAt", "RefreshTokenExpiryTime", "ResetPasswordExpiry", "UpdatedOn" },
                values: new object[] { new DateTime(2025, 4, 8, 21, 38, 19, 10, DateTimeKind.Utc).AddTicks(1585), new DateTime(2025, 4, 8, 21, 38, 19, 10, DateTimeKind.Utc).AddTicks(3110), new DateTime(2025, 4, 8, 21, 38, 19, 10, DateTimeKind.Utc).AddTicks(3631), new DateTime(2025, 4, 8, 21, 38, 19, 10, DateTimeKind.Utc).AddTicks(4408), new DateTime(2025, 4, 8, 21, 38, 19, 10, DateTimeKind.Utc).AddTicks(1874) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "EmailVerifiedAt", "RefreshTokenExpiryTime", "ResetPasswordExpiry", "UpdatedOn" },
                values: new object[] { new DateTime(2025, 4, 8, 21, 38, 19, 10, DateTimeKind.Utc).AddTicks(5511), new DateTime(2025, 4, 8, 21, 38, 19, 10, DateTimeKind.Utc).AddTicks(5514), new DateTime(2025, 4, 8, 21, 38, 19, 10, DateTimeKind.Utc).AddTicks(5515), new DateTime(2025, 4, 8, 21, 38, 19, 10, DateTimeKind.Utc).AddTicks(5516), new DateTime(2025, 4, 8, 21, 38, 19, 10, DateTimeKind.Utc).AddTicks(5512) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "EmailVerifiedAt", "RefreshTokenExpiryTime", "ResetPasswordExpiry", "UpdatedOn" },
                values: new object[] { new DateTime(2025, 4, 8, 21, 25, 39, 534, DateTimeKind.Utc).AddTicks(1367), new DateTime(2025, 4, 8, 21, 25, 39, 534, DateTimeKind.Utc).AddTicks(3784), new DateTime(2025, 4, 8, 21, 25, 39, 534, DateTimeKind.Utc).AddTicks(4202), new DateTime(2025, 4, 8, 21, 25, 39, 534, DateTimeKind.Utc).AddTicks(4674), new DateTime(2025, 4, 8, 21, 25, 39, 534, DateTimeKind.Utc).AddTicks(1700) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "EmailVerifiedAt", "RefreshTokenExpiryTime", "ResetPasswordExpiry", "UpdatedOn" },
                values: new object[] { new DateTime(2025, 4, 8, 21, 25, 39, 534, DateTimeKind.Utc).AddTicks(5842), new DateTime(2025, 4, 8, 21, 25, 39, 534, DateTimeKind.Utc).AddTicks(5844), new DateTime(2025, 4, 8, 21, 25, 39, 534, DateTimeKind.Utc).AddTicks(5845), new DateTime(2025, 4, 8, 21, 25, 39, 534, DateTimeKind.Utc).AddTicks(5846), new DateTime(2025, 4, 8, 21, 25, 39, 534, DateTimeKind.Utc).AddTicks(5842) });
        }
    }
}
