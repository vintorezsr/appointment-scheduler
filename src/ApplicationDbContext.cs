using AppointmentScheduler.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentScheduler
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppointmentSchedule>? Schedules { get; set; }
        public virtual DbSet<UserAccount>? UserAccounts { get; set; }
    }
}
