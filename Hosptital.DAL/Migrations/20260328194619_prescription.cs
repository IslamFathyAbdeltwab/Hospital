using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hosptital.DAL.Migrations
{
    /// <inheritdoc />
    public partial class prescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrescriptionId1",
                table: "Treatment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_PrescriptionId1",
                table: "Treatment",
                column: "PrescriptionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatment_Prescriptions_PrescriptionId1",
                table: "Treatment",
                column: "PrescriptionId1",
                principalTable: "Prescriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treatment_Prescriptions_PrescriptionId1",
                table: "Treatment");

            migrationBuilder.DropIndex(
                name: "IX_Treatment_PrescriptionId1",
                table: "Treatment");

            migrationBuilder.DropColumn(
                name: "PrescriptionId1",
                table: "Treatment");
        }
    }
}
