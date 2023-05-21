using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using Vidly.Utilities;

namespace Vidly.Controllers
{
    [Route("Customers")]
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;
 
        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new CustomersViewModel { Customers = Constants.Customers });
        }

        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            var customer = Constants.Customers.Find(customer => customer.Id == id);
            return View("CustomerDetails",new CustomerDetailsViewModel { Name = customer.Name});
        }
    }
}

