using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Movie_Booking.Migrations;
using Movie_Booking.Model;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Movie_Booking.Migrations;
using Movie_Booking.Model;
namespace Movie_Booking.Repositry
{
    public class MovieBookingRepositry : IMovieBooking
    {
        public readonly MovieBookingDbContext _context;

        public MovieBookingRepositry( MovieBookingDbContext context)
        {
            _context = context;
        }
        public async Task<User> AddUser(User user)
        {
            if(_context.users.Any(x=>x.emailId==user.emailId))
            {
                 return null;
            }
            else{
             try
            {
                _context.Database.EnsureCreated();
                User user1 = new User()
                {
                    userName=user.userName,
                     emailId = user.emailId,
                     phoneNumer=user.phoneNumer,
                     password=user.password
                };
                await _context.AddAsync(user1);
                _context.SaveChanges();
                return user1;
            }
            catch (Exception ex)
            {
                throw;
            }
            }
        }

      

        public int  CheckUser(string email, string password1)
        {
            try{
                var res=_context.users.Where(x=>x.emailId==email);
                if (res.Count()==1)
                {
                    return res.Count();
                }
                else{
                    return 0;
                }

            }
         catch(Exception ex)
         {
              return -1;
         }
          
        }


        public async Task<List<Admin>> Get()
        {
      
            try
            {
                List<Admin> admin_list = new List<Admin>();
                admin_list    = (from a in _context.admins
                               select a).ToList();
                return admin_list;
            }
            catch(Exception ex) {

                throw;
            }
        }

        public Task<List<Movie>> GetByDate(DateTime selecteddate)
        {
            var movielist=_context.movies.Where(x=>x.EndDate>=selecteddate).OrderByDescending(x=>x.ReleaseDate).ToListAsync();
            if(movielist==null)
            {
                return null;
            }
            else{
                return movielist;
            }
        }


        public async Task<List<Movie>> GetByLanguage(string lang)
        {
            var movie_list=await _context.movies.Where(x=>x.language==lang && x.EndDate>=DateTime.Now.Date ).OrderByDescending(x=>x.ReleaseDate).ToListAsync();
            return movie_list;
        }
            public async Task<List<Movie>> GetByLanguage1(string lang)
        {
            var movie_list=await _context.movies.Where(x=>x.language==lang && x.EndDate>=DateTime.Now.Date ).OrderByDescending(x=>x.ReleaseDate).ToListAsync();
            return movie_list;
        }


        public  Task<TicketBooking> getbyticketid(int id)
        {
            try{
            var ticketDetails= _context.ticketBookings.Where(x=>x.ticketsid==id).FirstOrDefaultAsync();
            return ticketDetails;
            }
            catch(Exception ex)
            {
                return null;
            }
        }


        public async Task<Object> GetTheaters(string moviename,string lang)
        {
              var theatersList = (from movie in _context.movies
                                   join theater in _context.theaters

                                   on movie.movieId equals theater.movieid
                                   where movie.movieName == moviename && movie.language==lang 
                                   select new {
                                       a=movie.movieName,
                                       b=theater.Theater_Name,
                                       c=theater.Theater_Address,
                                       d=theater.Theater_id,
                                       e=movie.ReleaseDate,
                                       f=movie.EndDate
                                   }).ToList();


            return theatersList;
            
        }

        public async Task<List<TicketBooking>> GetTicket(string mailId)
        {
            try{
                var res=await _context.ticketBookings.Where(x=>x.mail==mailId).OrderByDescending(x=>x.date).ToListAsync();
                if(res==null)
                {
                    return null;
                }
                else{
                    return res;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }


        public async Task<List<User>> GetUsers()
        {
          try
            {
                List<User> user_list = new List<User>();
                user_list    = (from a in _context.users
                               select a).ToList();
                return user_list;
            }
            catch(Exception ex) {

                throw;
            }
        }

        public async Task<Theaters> PostTheatr(int id,string name,string address,int mid)
        {
               try
            {
                _context.Database.EnsureCreated();
                Theaters theaters1 = new Theaters()
                {
                  Theater_id=id,
                  Theater_Name=name,
                  Theater_Address=address,
                  movieid=mid
                };
                await _context.AddAsync(theaters1);
                _context.SaveChanges();
                return theaters1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string UpdatePassword(string emailid, string password1)
        {
               try
            {                    
                if(_context.users.Any(x=>x.emailId==emailid))
                {                                            
                var res = _context.users.Where(x => x.emailId == emailid).ExecuteUpdateAsync(setters => setters.SetProperty(x => x.password, password1));
                _context.SaveChangesAsync(true);
                if (res == null)
                {
                    return "Not Updated Sucessfully";
                }
                else{
                return "Updated Sucessfully";
                }
                }
                else{
                    return "not updated";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> VerifyUser(string email, string phonenumber)
        {

            var res=await _context.users.Where(x=>x.emailId==email && x.phoneNumer==phonenumber).ToListAsync();
            if(res.Count==1)
            {
                return "Verified";
            }
            else{
                return "Not Verified";
            }
        }

        public async Task<List<TicketBooking>> ViewTicket()
        {
            var res=await _context.ticketBookings.ToListAsync();
            if(res==null)
            {
                return null;
            }  
            else{
                return res;
            }
        }
        public async Task<Movie> getbymovieid(int id)
        {
            var res=await _context.movies.Where(x=>x.movieId==id).SingleOrDefaultAsync();
            if(res==null)
            {
                return null;
            }
            else{
                return res;
            }
        }

        public async Task<object> GetByNameAndAddress(string searchItem,string moviename,string lang)
        {
          var theatersList = (from theater in _context.theaters
                                   join movie in   _context.movies         
                                   on  theater.movieid equals movie.movieId
                                   where movie.movieName==moviename&& movie.language==lang && (theater.Theater_Name.Contains(searchItem) || theater.Theater_Address.Contains(searchItem))
                                   select new {
                            a=movie.movieName,
                                       b=theater.Theater_Name,
                                       c=theater.Theater_Address,
                                       d=theater.Theater_id,
                                       e=movie.ReleaseDate,
                                       f=movie.EndDate
                                   }).ToList();


            return theatersList;  
        }

  
         public async Task<User> CheckPassword(string email,string pass)
         {
            var res=await _context.users.Where(x=>x.emailId==email && x.password==pass).SingleOrDefaultAsync();
            return res;
         }

       

        public async  Task<Review> PostReview(int rId, int mID, string email, string comments)
        {
                try
            {
                _context.Database.EnsureCreated();
                Review review = new Review()
                {
                  ReviewId=rId,
                  userMail=email,
                  comment=comments,
                  movieId=mID
                };
                await _context.AddAsync(review);
                _context.SaveChanges();
                return review;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Review>> GetReviews(int mID)
        {
             var res=await _context.reviews.Where(x=>x.movieId==mID).ToListAsync();
            return res;
        }

       
    }
}