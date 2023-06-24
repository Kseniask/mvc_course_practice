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

        public IActionResult Index()
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

        public ActionResult New()
        {
            var grenres = _context.Genre.ToList();
            var viewModel = new MovieFormViewModel { Genres = grenres };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genre.ToList()
                };

                return View("MovieForm", viewModel);
            }
            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.GenreId = movie.GenreId;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null) return NotFound();
            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genre.ToList()
            };
            return View("MovieForm", viewModel);
        }
    }
}

