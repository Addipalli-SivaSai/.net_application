using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Movie_Booking.Model
{
    public class MovieBookingDbContext:DbContext
    {
        public DbSet<Admin> admins{get; set;}

        public DbSet<User> users{get; set;}


        public DbSet<Movie> movies{get; set;}

        public DbSet<Theaters> theaters{get;set;}

        public DbSet<TicketBooking> ticketBookings{get;set;}
        public DbSet<Booking> bookings{get;set;}
        public DbSet<Review> reviews{get; set;}
        public MovieBookingDbContext(DbContextOptions<MovieBookingDbContext> options) : base(options)
        {
         
        }
   
    }
}