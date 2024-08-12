using System.ComponentModel.DataAnnotations;

namespace Movie_Booking.Model
{
    public class Review
    {
        public int ReviewId{get;set;}

        public int movieId{get;set;}
         [Required]
        public string? userMail{get; set;}
 [Required]
        public string? comment{get;set;}
    }
}