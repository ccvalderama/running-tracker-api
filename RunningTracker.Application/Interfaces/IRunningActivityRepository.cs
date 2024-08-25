using RunningTracker.Domain.Entities;

namespace RunningTracker.Application.Interfaces
{
    public interface IRunningActivityRepository
    {
        Task<RunningActivity?> GetByIdAsync(int id);
        Task AddAsync(RunningActivity activity);
        Task UpdateAsync(RunningActivity activity);
        Task DeleteAsync(int id);
    }
}
