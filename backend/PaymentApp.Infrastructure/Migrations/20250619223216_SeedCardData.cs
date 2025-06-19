using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCardData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Balance", "CardHolder", "CardNumber", "ExpiryDate" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), 1000m, "Mohamed Al Keblawy", "1234567890123456", new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));
        }
    }
}
