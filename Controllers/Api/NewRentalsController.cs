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
            var customer = _context.Customers.Single(customer => customer.Id == newRental.CustomerId);
            if (customer == null)
            {

            }
            var movies = _context.Movies.Where(movie => newRental.MoviesIds.Contains(movie.Id));

            foreach (var movie in movies)
            {
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
