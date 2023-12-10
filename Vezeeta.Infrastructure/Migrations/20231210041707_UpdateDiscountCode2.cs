using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDiscountCode2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxUsage",
                table: "DiscountCodes",
                newName: "RemainingUsage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemainingUsage",
                table: "DiscountCodes",
                newName: "MaxUsage");
        }
    }
}
