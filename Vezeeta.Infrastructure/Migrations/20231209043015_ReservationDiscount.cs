using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReservationDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "DiscountCodesUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodesUsers_ReservationId",
                table: "DiscountCodesUsers",
                column: "ReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCodesUsers_Reservations_ReservationId",
                table: "DiscountCodesUsers",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCodesUsers_Reservations_ReservationId",
                table: "DiscountCodesUsers");

            migrationBuilder.DropIndex(
                name: "IX_DiscountCodesUsers_ReservationId",
                table: "DiscountCodesUsers");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "DiscountCodesUsers");
        }
    }
}
