using System.ComponentModel.DataAnnotations;

namespace Movie_Booking.Model
{
    public class Admin
    {
        public int adminId{get;set;}
         [Required]
        public string? Name{get; set;}

 [Required]
         public string? emailId{get; set;}
 [Required]
        public string? password{get;set;}
    }
}