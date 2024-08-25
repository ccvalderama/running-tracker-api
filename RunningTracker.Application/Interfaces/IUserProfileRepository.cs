using RunningTracker.Domain.Entities;

namespace RunningTracker.Application.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<UserProfile?> GetByIdAsync(int id);
        Task AddAsync(UserProfile userProfile);
        Task UpdateAsync(UserProfile userProfile);
        Task DeleteAsnyc(int id);
    }
}
