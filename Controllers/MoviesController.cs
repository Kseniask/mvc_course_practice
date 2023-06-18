using Microsoft.AspNetCore.Mvc;
using Vidly.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? pageIndex, string sortby)
        {
            var viewModel = new MoviesViewModel
            {
                Movies = _context.Movies.Include(movie => movie.Genre).ToList(),
            };
            return View(viewModel);
        }

        public IActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1"},
                new Customer { Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            return Content("id = " + id);
        }


        // custom route using MapRoute
        public IActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        [Route("Movies/Released/{year}")]
        public IActionResult ByReleaseYear(int year)
        {
            return Content("release year: " + year);
        }

        public IActionResult Details(int id)
        {
            var movie = _context.Movies.Include(movie => movie.Genre).SingleOrDefault(movie => movie.Id == id);
            if (movie == null) return NotFound();
            return View("MovieDetails", new MovieDetailsViewModel { Movie = movie });
        }
    }
}

