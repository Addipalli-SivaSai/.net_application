using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Booking.Model
{
    public class Booking
    {
        [Key]
        public int seatId{get;set;}
         public int seatnumbet {get;set;}

         public bool is_available {get; set;}

          public string timings{get; set;}

         [Column(TypeName = "Date")]
          public DateTime Date{get;set;}
          [ForeignKey("Theaters")]
         public int Theater_id {get ;set;}
       public Theaters Theaters{get;set;}
    }
}