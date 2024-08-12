using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Booking.Model
{
    public class TicketBooking
    {
        [Key]
        public int ticketsid{get;set;}
        public string mail{get;set;}
       
         public string movieName{get;set;}

         public string language{get;set;}
         public int seatnumber{get;set;}
        public string theatrename{get;set;}

        public string theateraddress{get;set;}
     
        public string time{get;set;}

        [Column(TypeName = "Date")]

        public DateTime date{get;set;}
[ForeignKey("Booking")]
       public int seatid{get; set;}

       public Booking Booking{get; set;}
  
  public bool is_deleted{get;set;}

  public byte[] image{get; set;}


    }
}