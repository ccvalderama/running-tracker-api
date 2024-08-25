using RunningTracker.Application.Interfaces;
using RunningTracker.Domain.Entities;
using RunningTracker.Infrastructure.Persistence;

namespace RunningTracker.Infrastructure.Repositories
{
    public class RunningActivityRepository : IRunningActivityRepository
    {
        private readonly RunningTrackerDbContext _context;
        public RunningActivityRepository(RunningTrackerDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RunningActivity activity)
        {
            await _context.AddAsync(activity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var activity = await _context.RunningActivities.FindAsync(id);
            if (activity != null)
            {
                _context.RunningActivities.Remove(activity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RunningActivity?> GetByIdAsync(int id)
        {
            return await _context.RunningActivities.FindAsync(id);
        }

        public async Task UpdateAsync(RunningActivity activity)
        {
            _context.RunningActivities.Update(activity);
            await _context.SaveChangesAsync();
        }
    }

}
