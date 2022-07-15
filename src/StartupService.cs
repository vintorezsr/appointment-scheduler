using AppointmentScheduler.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

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
            var wasExist = dbContext.Database.GetService<IRelationalDatabaseCreator>().Exists();

            if (wasExist) return;

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            var userAccount = new UserAccount
            {
                Id = Guid.Parse("b5fbcf73-35f6-4e0e-bbed-eb82ccea667d"),
                Username = "vintorezsr",
                Password = BCrypt.Net.BCrypt.HashPassword("123456789"),
                Email = "vintorezsr@gmail.com"
            };

            dbContext.UserAccounts?.Add(userAccount);

            await dbContext.SaveChangesAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
