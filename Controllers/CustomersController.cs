using Microsoft.AspNetCore.Mvc;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [Route("Customers")]
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;
        private ApplicationDbContext _context;
 
        public CustomersController(ILogger<CustomersController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>(), configuration);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public IActionResult Index()
        {
            var customers = _context.Customers.Include(customer => customer.MembershipType).ToList();
            return View(new CustomersViewModel { Customers = customers });
        }

        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            var customer = _context.Customers.SingleOrDefault(customer => customer.Id == id);
            var membership = _context.MembershipType.SingleOrDefault(membership => membership.Id == customer.MembershipTypeId);
            if (customer == null) return NotFound();
            return View("CustomerDetails",new CustomerDetailsViewModel { CustomerData = customer, Membership = membership });
        }
    }
}

