using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ClinicDayHours_ClinicDayHourId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "ClinicDayHours");

            migrationBuilder.DropTable(
                name: "ClinicWeekDays");

            migrationBuilder.RenameColumn(
                name: "ClinicDayHourId",
                table: "Reservations",
                newName: "AppointmentsHourId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ClinicDayHourId",
                table: "Reservations",
                newName: "IX_Reservations_AppointmentsHourId");

            migrationBuilder.CreateTable(
                name: "AppointmentsDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentsDays_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentsHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hour = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppointmentsDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentsHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentsHours_AppointmentsDays_AppointmentsDayId",
                        column: x => x.AppointmentsDayId,
                        principalTable: "AppointmentsDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsDays_DoctorId",
                table: "AppointmentsDays",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentsHours_AppointmentsDayId",
                table: "AppointmentsHours",
                column: "AppointmentsDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AppointmentsHours_AppointmentsHourId",
                table: "Reservations",
                column: "AppointmentsHourId",
                principalTable: "AppointmentsHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AppointmentsHours_AppointmentsHourId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "AppointmentsHours");

            migrationBuilder.DropTable(
                name: "AppointmentsDays");

            migrationBuilder.RenameColumn(
                name: "AppointmentsHourId",
                table: "Reservations",
                newName: "ClinicDayHourId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_AppointmentsHourId",
                table: "Reservations",
                newName: "IX_Reservations_ClinicDayHourId");

            migrationBuilder.CreateTable(
                name: "ClinicWeekDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicWeekDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicWeekDays_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClinicDayHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicWeekDayId = table.Column<int>(type: "int", nullable: false),
                    Hour = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicDayHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicDayHours_ClinicWeekDays_ClinicWeekDayId",
                        column: x => x.ClinicWeekDayId,
                        principalTable: "ClinicWeekDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicDayHours_ClinicWeekDayId",
                table: "ClinicDayHours",
                column: "ClinicWeekDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicWeekDays_DoctorId",
                table: "ClinicWeekDays",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ClinicDayHours_ClinicDayHourId",
                table: "Reservations",
                column: "ClinicDayHourId",
                principalTable: "ClinicDayHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
