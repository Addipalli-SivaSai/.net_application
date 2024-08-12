
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Movie_Booking.Model;
using Movie_Booking.Repositry;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Claims;

using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movie_Booking.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieBookingController : ControllerBase
{


    public readonly IMovieBooking _db;
    public readonly MovieBookingDbContext _context1;
    public MovieBookingController(IMovieBooking _context, MovieBookingDbContext _movieContext)
    {
        _db = _context;
        _context1 = _movieContext;
    }
    [HttpGet("/admin")]
    public async Task<IActionResult> Get_Admin()
    {
        var res = await _db.Get();
        if (res.Count == 0)
        {
            return BadRequest();
        }
        else
        {
            return Ok(res);
        }
    }
    [HttpGet("/user")]
    public async Task<IActionResult> Get_Users()
    {
        var res = await _db.GetUsers();
        if (res.Count == 0)
        {
            return BadRequest();
        }
        else
        {
            return Ok(res);
        }
    }
    [HttpPut]
    public IActionResult Update_password(string email, string password)
    {

        var res = _db.UpdatePassword(email, password);
        if (res == "Updated Sucessfully")
        {
            return Ok(res);
        }
        else
        {
            return BadRequest();
        }
    }
    [HttpGet("/check")]
    public IActionResult Checking(string mailid, string pass)
    {
        var res = _db.CheckUser(mailid, pass);
        if (res == 1)
        {
            return Ok();
        }
        else if (res == -1)
        {
            return BadRequest();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("/verify")]
    public async Task<IActionResult> Verify(string mail, string phonenumber)
    {
        var res = await _db.VerifyUser(mail, phonenumber);
        if (res == "Verified")
        {
            return Ok(res);
        }
        else
        {
            return NotFound(res);
        }
    }
    [HttpPost("/addmovie")]
    public async Task<IActionResult> Post([FromForm] MovieDetails model)
    {
        try
        {

            var movie = new Movie
            {
                movieName = model.movieName,
                RunTime = model.RunTime,
                HeroName = model.HeroName,
                HeroineName = model.HeroineName,
                Director = model.Director,
                Producer = model.Producer,
                musicDirector = model.musicDirector,
                language = model.language,
                movieDescription = model.movieDescription,
                ReleaseDate = model.ReleaseDate,
                EndDate = model.EndDate
            };

            using (var memoryStream = new MemoryStream())
            {
                await model.poster.CopyToAsync(memoryStream);
                movie.movie_poster = memoryStream.ToArray();
            }
            

            _context1.movies.Add(movie);
            await _context1.SaveChangesAsync();

            return Ok("Upload successful");
        }

        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpGet("/movies")]
    public async Task<IActionResult> GetPho()
    {
        try
        {
            var photos = await _context1.movies.Where(x => x.EndDate >= DateTime.Now.Date).OrderByDescending(x=>x.ReleaseDate).ToListAsync();
            return Ok(photos);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
    [HttpGet("/search")]
    public async Task<IActionResult> SearchMovie(string moviename)
    {
        try
        {
            var searchItem = await _context1.movies.Where(x => x.movieName.Contains(moviename) && x.EndDate>=DateTime.Now.Date).ToListAsync();

          
                return Ok(searchItem);
            

        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
    [HttpPost]
    public async Task<IActionResult> Post_user([FromBody] User user)
    {
        User user1 = new User();
        try
        {
            user1 = await _db.AddUser(user);
            if (user1 == null)
            {
                return BadRequest();
            }
            return Ok(user1);

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    [HttpPost("/addtheatres")]
    public async Task<IActionResult> PostTheatres(int tid, string tname, string taddress, int mid)
    {
        Theaters theaters1 = new Theaters();
        try
        {
            theaters1 = await _db.PostTheatr(tid, tname, taddress, mid);
            if (theaters1 == null)
            {
                return BadRequest();
            }
            return Ok(theaters1);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [HttpGet("/language")]
    public async Task<IActionResult> GetMovieLanguage(string language)
    {
        var res = await _db.GetByLanguage(language);
        if (res == null)
        {
            return BadRequest(res);
        }
        else
        {
            return Ok(res);
        }
    }
    [HttpGet("/theatreList")]
    public async Task<IActionResult> GetTheatre(string movie_name,string lang)
    {
        try
        {
            var res = await _db.GetTheaters(movie_name,lang);
            if (res == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(res);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("/dates")]
    public async Task<IActionResult> GetByDate(DateTime dateTime)
    {
        try
        {
            var res = await _db.GetByDate(dateTime);
            if (res == null)
            {
                return NotFound(res);
            }
            else
            {
                return Ok(res);
            }
        }
        catch (Exception ex)
        {
            return BadRequest();
        }

    }
    [HttpGet("/bookings")]
    public async Task<IActionResult> GetSeats()
    {
        var res = await _context1.bookings.ToListAsync();
        if (res == null)
        {
            return BadRequest(res);
        }
        else
        {
            return Ok(res);
        }
    }
    [HttpGet("/tickets")]
    public async Task<IActionResult> GetTickets(string time, int id, DateTime date)
    {

        var res = await _context1.bookings.Where(x => x.Theater_id == id && x.timings == time && x.Date == date && date >= DateTime.Now.Date).ToListAsync();
        if (res == null)
        {
            return BadRequest(res);
        }
        else
        {
            return Ok(res);
        }
    }

    [HttpPut("/updateseats")]

    public async Task<IActionResult> updateseats(int id)
    {
        var res = await _context1.bookings.Where(x => x.seatId == id).ExecuteUpdateAsync(setters => setters.SetProperty(x => x.is_available, false));
        _context1.SaveChangesAsync(true);
        if (res == null)
        {
            return BadRequest(res);
        }
        else
        {
            return Ok(res);
        }

    }
    [HttpPost("/ticket")]
    public async Task<IActionResult> PostTickets([FromForm] TicketBooking tickets)
    {

        try
        {

            var ticket1 = new TicketBooking
            {
                mail = tickets.mail,
                movieName = tickets.movieName,
                language=tickets.language,
                seatnumber = tickets.seatnumber,
                theatrename = tickets.theatrename,
                theateraddress = tickets.theateraddress,
                time = tickets.time,
                date = tickets.date,
                seatid = tickets.seatid,
                is_deleted=tickets.is_deleted,
                image=tickets.image
            };


            _context1.ticketBookings.Add(ticket1);
            await _context1.SaveChangesAsync();

            return Ok("Upload successful");
        }

        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
    [HttpGet("/viewticket")]
    public async Task<IActionResult> Ticket(string mailId)
    {
        var res = await _db.GetTicket(mailId);
        if (res == null)
        {
            return BadRequest(res);
        }
        else
        {
            return Ok(res);
        }

    }
    [HttpGet("/viewticketbyadmin")]
    public async Task<IActionResult> ViewTicket()
    {
        var res = await _db.ViewTicket();
        if (res == null)
        {
            return BadRequest(res);
        }
        else
        {
            return Ok(res);
        }
    }
    [HttpPost("/AddSeating")]
    public async Task<IActionResult> AddSeating(int seatid, int seatnumber, bool isAvailable, string timing, int theaterId, DateTime date)
    {
        try
        {

            var booking = new Booking();
            booking.seatId = seatid;
            booking.seatnumbet = seatnumber;
            booking.is_available = isAvailable;
            booking.timings = timing;
            booking.Theater_id = theaterId;
            booking.Date = date;


            _context1.bookings.Add(booking);
            await _context1.SaveChangesAsync();

            return Ok("Upload successful");
        }

        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
    [HttpPut("/cancelseats")]

    public async Task<IActionResult> Cancelseats(int id)
    {
        var res = await _context1.bookings.Where(x => x.seatId == id).ExecuteUpdateAsync(setters => setters.SetProperty(x => x.is_available, true));
        _context1.SaveChangesAsync(true);
        if (res == null)
        {
            return BadRequest(res);
        }
        else
        {
            return Ok(res);
        }

    }
    [HttpPut("/deleteticket")]
    public async Task<IActionResult> DeleteTickets(int tid)
    {
        var res = await _context1.ticketBookings.Where(x => x.ticketsid == tid).ExecuteUpdateAsync(setters => setters.SetProperty(x => x.is_deleted, true));
        _context1.SaveChangesAsync();
        if (res == null)
        {
            return BadRequest(res);
        }
        else
        {
            return Ok(res);
        }
    }
     [HttpGet("/languagesearch")]
    public async Task<IActionResult> SearchlanguageMovie(string moviename,string lang)
    {
        try
        {
            var searchItem = await _context1.movies.Where(x => x.movieName.Contains(moviename) && x.EndDate>=DateTime.Now.Date && x.language==lang).ToListAsync();

            if (searchItem.Count == 0)
            {
                return Ok(searchItem);
            }
            else
            {
                return Ok(searchItem);
            }

        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
    [HttpGet("/ticketid")]
    public async Task<IActionResult> GetByTicketId(int id)
    {
        var res=await _db.getbyticketid(id);
        if(res==null)
        {
            return NotFound();
        }
        else{
            return Ok(res);
        }
    }
    [HttpGet("/movieid")]
     public async Task<IActionResult> Getbymovieid(int id)
     {
        var res=await _db.getbymovieid(id);
        if(res==null)
        {
            return NotFound();
        }
        else{
            return Ok(res);
        }

     }
     [HttpGet("/nameandaddress")]
     public async Task<IActionResult> GetBynameAndaddress(string searchItem,string moviename, string language)
     {
        var res=await _db.GetByNameAndAddress(searchItem,moviename,language);
        if(res==null)
        {
            return NotFound();
        }
        else{
            return Ok(res);
        }
     }

     [HttpGet("/checkpassword")]

     public async Task<IActionResult> PasswordCheck(string email,string pass)
     {
        var res=await _db.CheckPassword(email,pass);
        if(res==null)

        {
            return NotFound();
        }
        else{
            return Ok();
        }
     }

     [HttpPost("/postreview")]
       public async Task<IActionResult> AddReview(int rID, int mId,string email,string comment)
    {
        try
        {
         var res=await _db.PostReview(rID,mId,email,comment);
         if(res==null)
         {
            return BadRequest();
         }
         else{
            return Ok(res);
         }

          
        }

        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
    [HttpGet("/review")]
      public async Task<IActionResult> getreviews(int mId)
    {
        try
        {
         var res=await _db.GetReviews(mId);
         if(res==null)
         {
            return BadRequest();
         }
         else{
            return Ok(res);
         }

          
        }

        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }


[HttpGet("/email")]
public void SendEmail(string to, string subject, string html, string from = null)
{
    // create message
    var email = new MimeMessage();
    email.From.Add(MailboxAddress.Parse(from));
    email.To.Add(MailboxAddress.Parse(to));
    email.Subject = subject;
    email.Body = new TextPart(TextFormat.Html) { Text = html };

    // send email
    using var smtp = new SmtpClient();
    smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
    smtp.Authenticate("[USERNAME]", "[PASSWORD]");
    smtp.Send(email);
    smtp.Disconnect(true);

}
}
//http://localhost:5134/postreview?rID=0&mId=4&email=sivasai2511%40gmail.com&comment=Movie%20was%20good.%20Go%20and%20watch%20it



