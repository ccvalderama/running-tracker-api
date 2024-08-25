using Microsoft.EntityFrameworkCore;
using RunningTracker.Application.Interfaces;
using RunningTracker.Domain.Entities;
using RunningTracker.Infrastructure.Persistence;

namespace RunningTracker.Infrastructure.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly RunningTrackerDbContext _context;
        public UserProfileRepository(RunningTrackerDbContext context) 
        { 
            _context = context;
        }

        public async Task<UserProfile?> GetByIdAsync(int id)
        {
            return await _context.UserProfiles.Include(x => x.RunningActivities).FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task AddAsync(UserProfile profile)
        {
            await _context.UserProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task GetTaskAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Update(userProfile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Update(userProfile);
            await _context.AddRangeAsync();
        }

        public async Task DeleteAsnyc(int id)
        {
            var profile = await _context.UserProfiles.FindAsync(id);
            if (profile != null) 
            {
                _context.UserProfiles.Remove(profile);
                await _context.SaveChangesAsync();
            }
        }
    }

}
