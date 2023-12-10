using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointmentsTablesConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsHours_Hour_AppointmentsDayId",
                table: "AppointmentsHours",
                columns: new[] { "Hour", "AppointmentsDayId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsDays_Day_DoctorId",
                table: "AppointmentsDays",
                columns: new[] { "Day", "DoctorId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppointmentsHours_Hour_AppointmentsDayId",
                table: "AppointmentsHours");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentsDays_Day_DoctorId",
                table: "AppointmentsDays");
        }
    }
}
