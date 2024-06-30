using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectAviation.Controllers
{
    public class PassengerController : Controller
    {
        [Authorize(Roles = "Passenger")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
