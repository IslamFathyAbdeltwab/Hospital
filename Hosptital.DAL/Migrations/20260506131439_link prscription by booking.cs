using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hosptital.DAL.Migrations
{
    /// <inheritdoc />
    public partial class linkprscriptionbybooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Prescriptions");
        }
    }
}
