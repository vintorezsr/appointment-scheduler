using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppointmentScheduler.Controllers
{
    /// <summary>
    /// Appointment controller class.
    /// </summary>
    [Authorize]
    public class AppointmentController : Controller
    {
        /// <summary>
        /// Field that hold <see cref="ApplicationDbContext"/> instance.
        /// </summary>
        private readonly ApplicationDbContext _applicationDbContext;

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="applicationDbContext">This instance of <see cref="ApplicationDbContext"/>.</param>
        public AppointmentController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Appointment main page.
        /// </summary>
        public IActionResult Index()
        {
            var userAccountId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value!);
            var appointmentSchedules = _applicationDbContext?.Schedules?.Where(x => x.UserAccountId == userAccountId);

            return View(appointmentSchedules);
        }

        /// <summary>
        /// Appointment creation page.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }
    }
}