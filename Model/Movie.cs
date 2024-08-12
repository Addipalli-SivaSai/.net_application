using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_Booking.Model
{
    public class Movie
    {
        [Key]
        public  int movieId{get; set;}

        public  byte[]? movie_poster{get; set;}

        public  string? movieName{get; set;}
        public  string? RunTime{get; set;}
     
       public  string? HeroName{get;set;}

       public  string? HeroineName{get; set;}

       public  string? Director{get; set;}

       public  string? Producer{get; set;}


       public  string? musicDirector{get; set;}

       public required string? language{get;set;}

       public  string? movieDescription{get; set;}

[Column(TypeName = "Date")]
       public  DateTime ReleaseDate{get;set;}
    

      [Column(TypeName = "Date")]
       public  DateTime EndDate{get;set;}

   
    

    }
}