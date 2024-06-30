using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectAviation.Controllers
{
    public class PilotController : Controller
    {
        [Authorize(Roles = "Pilot")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
