using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: /<controller>/
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

        public IActionResult Index(int? pageIndex, string sortby)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (String.IsNullOrEmpty(sortby))
                sortby = "Name";

            return Content(String.Format("pageIndex={0},&sortby={1}", pageIndex, sortby));
        }

        // custom route using MapRoute
        public IActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        [Route("movies/released/{year}")]
        public IActionResult ByReleaseYear(int year)
        {
            return Content("release year: " + year);
        }

    }
}

