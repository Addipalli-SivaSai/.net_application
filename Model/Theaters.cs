using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Booking.Model
{
    public class Theaters
    {
    [Key]
        public int Theater_id{get; set;}

        public string? Theater_Name{get; set;}
        public string? Theater_Address{get; set;}
     [ForeignKey("Movie")]
       public int movieid{get;set;}
      public Movie Movie{get;set;}
  

    }
}