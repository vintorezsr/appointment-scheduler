using AppointmentScheduler.Models;
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
        /// <param name="options">Instance of <see cref="ApplicationDbContext"/>.</param>
        public AppointmentController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Appointment main page.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// API to create new appointment.
        /// </summary>
        /// <param name="appointmentSchedule">This instance of <see cref="AppointmentSchedule"/>.</param>
        public async Task<IActionResult> Create([FromBody] AppointmentSchedule appointmentSchedule)
        {
            appointmentSchedule.StartTime = appointmentSchedule.StartTime.ToUniversalTime();
            appointmentSchedule.EndTime = appointmentSchedule.EndTime.ToUniversalTime();
            _applicationDbContext.Schedules?.Add(appointmentSchedule);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(appointmentSchedule);
        }

        /// <summary>
        /// API to update the given appointment.
        /// </summary>
        /// <param name="appointmentSchedule">This instance of <see cref="AppointmentSchedule"/>.</param>
        public async Task<IActionResult> Update([FromBody] AppointmentSchedule appointmentSchedule)
        {
            appointmentSchedule.StartTime = appointmentSchedule.StartTime.ToUniversalTime();
            appointmentSchedule.EndTime = appointmentSchedule.EndTime.ToUniversalTime();
            _applicationDbContext.Schedules?.Update(appointmentSchedule);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(appointmentSchedule);
        }

        /// <summary>
        /// API to delete given appointment by id.
        /// </summary>
        /// <param name="id">The <see cref="AppointmentSchedule"/> id.</param>
        public async Task<IActionResult> Delete(Guid id)
        {
            var appointmentSchedule = _applicationDbContext.Schedules?.FirstOrDefault(x => x.Id == id);
            if (appointmentSchedule == null) return BadRequest();
            _applicationDbContext.Schedules?.Remove(appointmentSchedule);
            await _applicationDbContext.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// API to check conflicted appointments by given schedule.
        /// </summary>
        /// <param name="schedule">This instance of <see cref="Schedule"/>.</param>
        public IActionResult CheckConflict([FromBody] Schedule schedule)
        {
            var userAccountId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value!);
            var startTimeUtc = schedule.StartTime.ToUniversalTime();
            var endTimeUtc = schedule.EndTime.ToUniversalTime();
            var hasConflict = _applicationDbContext.Schedules?.Any(x => x.UserAccountId == userAccountId && endTimeUtc >= x.StartTime && startTimeUtc <= x.EndTime);

            return Ok(new
            {
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                HasConflict = hasConflict
            });
        }
    }
}