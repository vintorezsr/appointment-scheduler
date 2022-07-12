using AppointmentScheduler.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppointmentScheduler.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AppointmentController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create([FromBody] AppointmentSchedule appointmentSchedule)
        {
            appointmentSchedule.StartTime = appointmentSchedule.StartTime.ToUniversalTime();
            appointmentSchedule.EndTime = appointmentSchedule.EndTime.ToUniversalTime();
            _applicationDbContext.Schedules?.Add(appointmentSchedule);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(appointmentSchedule);
        }

        public async Task<IActionResult> Update([FromBody] AppointmentSchedule appointmentSchedule)
        {
            appointmentSchedule.StartTime = appointmentSchedule.StartTime.ToUniversalTime();
            appointmentSchedule.EndTime = appointmentSchedule.EndTime.ToUniversalTime();
            _applicationDbContext.Schedules?.Update(appointmentSchedule);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(appointmentSchedule);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var appointmentSchedule = _applicationDbContext.Schedules?.FirstOrDefault(x => x.Id == id);
            if (appointmentSchedule == null) return BadRequest();
            _applicationDbContext.Schedules?.Remove(appointmentSchedule);
            await _applicationDbContext.SaveChangesAsync();
            return Ok();
        }

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