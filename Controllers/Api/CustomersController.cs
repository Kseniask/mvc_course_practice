using AutoMapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Vidly.DTOs;

namespace Vidly.Controllers.Api
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;
        public CustomersController(IConfiguration configuration, IMapper mapper)
        {
            _context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>(), configuration);
            _mapper = mapper;
        }
        //GET /api/customers
        public IEnumerable<CustomerDTO> GetCustomers()
        {
            return _context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(_mapper.Map<Customer, CustomerDTO>);
        }

        //GET /api/customers
        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> GetCustomer(int id)
        {
            var customer = _context.Customers
                .Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();

            return Ok(_mapper.Map<Customer, CustomerDTO>(customer));
        }

        // POST /api/customer
        [HttpPost]
        public ActionResult<CustomerDTO> CreateCustomer(CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid) return BadRequest();
            var customer = _mapper.Map<CustomerDTO, Customer>(customerDTO);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDTO.Id = customer.Id; 
            return Created(new Uri(Request.GetDisplayUrl() + "/" + customer.Id), customerDTO);
        }

        // PUT /api/customers/1
        [HttpPut("{id}")]
        public void UpdateCustomer(int id, CustomerDTO customerDTO) {
            if (!ModelState.IsValid) throw new BadHttpRequestException("Invalid entry");

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null) throw new BadHttpRequestException("Customer not found");

            _mapper.Map(customerDTO, customerInDb);
            _context.SaveChanges();
        }

        // DELETE /api/customers/1
        [HttpDelete("{id}")]
        public void DeleteCustomer(int id)
        {
            if (!ModelState.IsValid) throw new BadHttpRequestException("Invalid entry");

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null) throw new BadHttpRequestException("Customer not found");

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }

    }
}
