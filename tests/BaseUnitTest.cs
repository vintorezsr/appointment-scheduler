using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AppointmentScheduler.Test
{
    public class BaseUnitTest
    {
        public IServiceProvider ServiceProvider { get; }

        public BaseUnitTest()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("appointment"));
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}