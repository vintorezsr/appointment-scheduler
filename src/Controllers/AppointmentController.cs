using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduler.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}