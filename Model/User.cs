using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Movie_Booking.Model
{
    public class User
    {
        [Key]
        public int userId{get;set;}
          
      
         public string? userName{get;set;}
         
       
         public string? emailId{get; set;}

         public string? phoneNumer{get; set;}
 
         public string? password{get;set;}

    }
}