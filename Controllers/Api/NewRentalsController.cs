using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vidly.DTOs;

namespace Vidly.Controllers.Api
{
    [Route("api/newrentals")]
    [ApiController]
    [Authorize]
    public class NewRentalsController : ControllerBase
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        public NewRentalsController(IConfiguration configuration, IMapper mapper)
        {
            _context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>(), configuration);
            _mapper = mapper;
        }
        // POST /api/newrentals
        [HttpPost]
        public ActionResult CreateNewRental(NewRentalDTO newRental)
        {
            if(newRental.MoviesIds.Count == 0)
                return BadRequest("No Movie IDs are given.");

            var customer = _context.Customers.SingleOrDefault(customer => customer.Id == newRental.CustomerId);
            if (customer == null)
                return BadRequest("CustomerID is not valid.");

            var movies = _context.Movies.Where(movie => newRental.MoviesIds.Contains(movie.Id)).ToList();

            if (movies.Count != newRental.MoviesIds.Count)
                return BadRequest("One or more movie IDs are not valid.");

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable--;
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now,
                };

                _context.Rentals.Add(rental);
            }
            _context.SaveChanges();
            return Ok();
        }

    }
}
