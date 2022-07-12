using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduler.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}