using AppointmentScheduler.Abstractions;
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
    public class AppointmentApiController : ApiControllerBase
    {
        private const string HolidayApiUrl = "https://date.nager.at/api/v3/PublicHolidays/2022/SG";

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="options">Instance of <see cref="ApplicationDbContext"/>.</param>
        public AppointmentApiController(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        /// <summary>
        /// API to create new appointment.
        /// </summary>
        /// <param name="appointmentSchedule">This instance of <see cref="AppointmentSchedule"/>.</param>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentScheduleDto input)
        {
            var holidays = await GetHolidays();
            var isInHoliday = holidays.Any(x => x.Date >= input.StartTime.Date && x.Date <= input.EndTime.Date);
            if (isInHoliday)
            {
                return BadRequest(new
                {
                    Message = string.Format("There is some holiday between {0:dd-MM-yyyy HH:mm} and {1:dd-MM-yyyy HH:mm}", input.StartTime, input.EndTime)
                });
            }
            var hasConflict = CheckSchedule(input.StartTime, input.EndTime);
            if (hasConflict)
            {
                return BadRequest(new
                {
                    Message = string.Format("There is some schedule conflict between {0:dd-MM-yyyy HH:mm} and {1:dd-MM-yyyy HH:mm}", input.StartTime, input.EndTime)
                });
            }
            var userAccountId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var appointmentSchedule = AppointmentSchedule.CreateFrom(userAccountId, input);
            _applicationDbContext.Schedules?.Add(appointmentSchedule);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(input);
        }

        /// <summary>
        /// API to update the given appointment.
        /// </summary>
        /// <param name="appointmentSchedule">This instance of <see cref="AppointmentSchedule"/>.</param>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AppointmentScheduleDto input)
        {
            var userAccountId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var appointmentSchedule = AppointmentSchedule.CreateFrom(userAccountId, input);
            _applicationDbContext.Schedules?.Update(appointmentSchedule);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(input);
        }

        /// <summary>
        /// API to delete given appointment by id.
        /// </summary>
        /// <param name="id">The <see cref="AppointmentSchedule"/> id.</param>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userAccountId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var appointmentSchedule = _applicationDbContext.Schedules?.FirstOrDefault(x => x.Id == id);
            if (appointmentSchedule == null || userAccountId != appointmentSchedule.UserAccountId)
            {
                return BadRequest();
            }
            _applicationDbContext.Schedules?.Remove(appointmentSchedule);
            await _applicationDbContext.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// API to check conflicted appointments by given schedule.
        /// </summary>
        /// <param name="schedule">This instance of <see cref="Schedule"/>.</param>
        [HttpPost("check")]
        public IActionResult CheckConflict([FromBody] Schedule schedule)
        {
            var hasConflict = CheckSchedule(schedule.StartTime, schedule.EndTime);

            return Ok(new
            {
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                HasConflict = hasConflict
            });
        }

        private async Task<IEnumerable<Holiday>> GetHolidays()
        {
            var client = new HttpClient();
            var httpResponseMessage = await client.GetAsync(HolidayApiUrl);
            var holidays = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<Holiday>>();
            return holidays;
        }

        private bool CheckSchedule(DateTime startTime, DateTime endTime)
        {
            var userAccountId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var startTimeUtc = startTime.ToUniversalTime();
            var endTimeUtc = endTime.ToUniversalTime();
            var hasConflict = _applicationDbContext.Schedules?.Any(x => x.UserAccountId == userAccountId && endTimeUtc >= x.StartTime && startTimeUtc <= x.EndTime);
            return hasConflict ?? false;
        }
    }
}