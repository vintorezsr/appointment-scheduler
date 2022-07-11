using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduler.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            var isAuthenticated = User.Identity?.IsAuthenticated ?? false;
            if (isAuthenticated)
            {
                return Redirect("~/home");
            }

            return View();
        }

        [Authorize]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}