using Microsoft.AspNetCore.Mvc;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
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

        [Route("Customers/Details/{id}")]
        public IActionResult Details(int id)
        {
            var customer = _context.Customers.SingleOrDefault(customer => customer.Id == id);
            var membership = _context.MembershipType.SingleOrDefault(membership => membership.Id == customer.MembershipTypeId);
            if (customer == null) return NotFound();
            return View("CustomerDetails",new CustomerDetailsViewModel { CustomerData = customer, Membership = membership });
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipType.ToList();
            var viewModel = new CustomerFormViewModel { MembershipTypes = membershipTypes };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipType.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            if(customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
            }
           
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if( customer == null ) return NotFound();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipType.ToList()
            };
            return View("CustomerForm", viewModel);
        }

    }
}

