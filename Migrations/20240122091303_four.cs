using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie_Booking.Migrations
{
    /// <inheritdoc />
    public partial class four : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_bookings_Theater_id",
                table: "bookings",
                column: "Theater_id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_theaters_Theater_id",
                table: "bookings",
                column: "Theater_id",
                principalTable: "theaters",
                principalColumn: "Theater_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookings_theaters_Theater_id",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_bookings_Theater_id",
                table: "bookings");
        }
    }
}
