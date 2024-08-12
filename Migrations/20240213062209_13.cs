using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie_Booking.Migrations
{
    /// <inheritdoc />
    public partial class _13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ticketBookings_seatid",
                table: "ticketBookings",
                column: "seatid");

            migrationBuilder.AddForeignKey(
                name: "FK_ticketBookings_bookings_seatid",
                table: "ticketBookings",
                column: "seatid",
                principalTable: "bookings",
                principalColumn: "seatId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ticketBookings_bookings_seatid",
                table: "ticketBookings");

            migrationBuilder.DropIndex(
                name: "IX_ticketBookings_seatid",
                table: "ticketBookings");
        }
    }
}
