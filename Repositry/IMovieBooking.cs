using System.Reflection.Metadata;
using Movie_Booking.Model;

namespace Movie_Booking.Repositry
{
    public interface IMovieBooking
    {
         public Task<List<Admin>> Get();
         public Task<List<User>> GetUsers();
        
        public Task<User> AddUser(User user);

        public int CheckUser(string email,string password);
        public string UpdatePassword(string emailId,string password);

        public Task<List<Movie>> GetByLanguage(string language);


        public Task<List<Movie>> GetByDate(DateTime releaseddate);


        public Task<string> VerifyUser(string email,string phonenumber);


        public Task<Object> GetTheaters(string moviename,string lang);



        public Task<Theaters> PostTheatr(int id,string tname,string taddress,int mid);


        public Task<List<TicketBooking>> GetTicket(string mailId);
       
         public Task<List<TicketBooking>> ViewTicket();

         public Task<TicketBooking> getbyticketid(int id);


        public Task<Movie> getbymovieid(int id);

        public Task<object> GetByNameAndAddress(string searchItem, string moviename,string language);


        public Task<User> CheckPassword(string email,string password);

      public Task<Review> PostReview(int rId,int mID,string email,string comment);
       public Task<List<Review>> GetReviews(int mID);

    }
}