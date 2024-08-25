using RunningTracker.Domain.Entities;
using System.Runtime.CompilerServices;

namespace RunningTracker.Application.Interfaces
{
    public interface IRunningActivityService
    {
        Task<RunningActivity?> GetRunningActivityAsync(int id, [CallerMemberName] string methodName = "");
        Task AddRunningActivityAsync(RunningActivity runningActivity, [CallerMemberName] string methodName = "");
        Task UpdateRunningActivityAsync(RunningActivity runningActivity, [CallerMemberName] string methodName = "");
    }
}
