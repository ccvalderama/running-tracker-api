using Microsoft.EntityFrameworkCore;
using RunningTracker.Domain.Entities;

namespace RunningTracker.Infrastructure.Persistence
{
    public class RunningTrackerDbContext : DbContext
    {
        public RunningTrackerDbContext( DbContextOptions<RunningTrackerDbContext> options ) 
            : base( options ) 
        { 
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<RunningActivity> RunningActivities { get; set; }
    }
}
