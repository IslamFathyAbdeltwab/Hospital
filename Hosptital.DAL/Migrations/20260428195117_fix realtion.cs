using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hosptital.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixrealtion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DoctorAvailabilityId",
                table: "Bookings",
                column: "DoctorAvailabilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_DoctorAvailabilityId",
                table: "Bookings");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DoctorAvailabilityId",
                table: "Bookings",
                column: "DoctorAvailabilityId",
                unique: true);
        }
    }
}
