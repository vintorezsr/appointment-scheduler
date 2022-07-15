using AppointmentScheduler.Controllers;
using AppointmentScheduler.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Xunit;

namespace AppointmentScheduler.Test
{
    public class MainTest : BaseUnitTest
    {
        [Fact]
        public async Task Test()
        {
            using var dbContext = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var controller = new AppointmentApiController(dbContext);
            var claims = new[]
            {
                new Claim("Id", "")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };
            var viewResult = await controller.Create(new AppointmentSchedule { }) as ViewResult;
        }
    }
}