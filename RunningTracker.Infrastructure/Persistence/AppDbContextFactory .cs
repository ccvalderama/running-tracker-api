using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RunningTracker.Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<RunningTrackerDbContext>
    {
        public RunningTrackerDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<RunningTrackerDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new RunningTrackerDbContext(optionsBuilder.Options);
        }
    }
}
