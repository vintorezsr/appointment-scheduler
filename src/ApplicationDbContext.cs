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

    }
}
