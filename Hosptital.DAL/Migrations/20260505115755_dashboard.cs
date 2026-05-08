using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hosptital.DAL.Migrations
{
    /// <inheritdoc />
    public partial class dashboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorDashboard",
                columns: table => new
                {
                    TotalPatients = table.Column<int>(type: "int", nullable: false),
                    TotalPrescriptions = table.Column<int>(type: "int", nullable: false),
                    TotalBookings = table.Column<int>(type: "int", nullable: false),
                    ConfirmedBookings = table.Column<int>(type: "int", nullable: false),
                    PendingBookings = table.Column<int>(type: "int", nullable: false),
                    CancelledBookings = table.Column<int>(type: "int", nullable: false),
                    TotalRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PendingRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThisMonthRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvgBookingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorDashboard");
        }
    }
}
