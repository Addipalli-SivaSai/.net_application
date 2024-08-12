using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie_Booking.Migrations
{
    /// <inheritdoc />
    public partial class three : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_theaters_movieid",
                table: "theaters",
                column: "movieid");

            migrationBuilder.AddForeignKey(
                name: "FK_theaters_movies_movieid",
                table: "theaters",
                column: "movieid",
                principalTable: "movies",
                principalColumn: "movieId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_theaters_movies_movieid",
                table: "theaters");

            migrationBuilder.DropIndex(
                name: "IX_theaters_movieid",
                table: "theaters");
        }
    }
}
