using Microsoft.EntityFrameworkCore;

namespace AppointmentScheduler
{
    public class StartupService : IHostedService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public StartupService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
