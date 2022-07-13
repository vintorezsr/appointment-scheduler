using AppointmentScheduler.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentScheduler
{
    /// <summary>
    /// Main application <see cref="DbContext"/> class.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="options">Instance of <see cref="DbContextOptions"/>.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="AppointmentSchedule"/> repository instance.
        /// </summary>
        public virtual DbSet<AppointmentSchedule>? Schedules { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="UserAccount"/> repository instance.
        /// </summary>
        public virtual DbSet<UserAccount>? UserAccounts { get; set; }
    }
}
