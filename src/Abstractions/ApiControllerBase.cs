using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduler.Abstractions
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// Field that hold <see cref="ApplicationDbContext"/> instance.
        /// </summary>
        protected readonly ApplicationDbContext _applicationDbContext;

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="applicationDbContext">This instance of <see cref="ApplicationDbContext"/>.</param>
        public ApiControllerBase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
    }
}
