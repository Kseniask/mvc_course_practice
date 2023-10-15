using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vidly.Controllers
{
    [Authorize]
    public class RentalsController : Controller
    {
        public IActionResult New()
        {
            return View();
        }
    }
}
