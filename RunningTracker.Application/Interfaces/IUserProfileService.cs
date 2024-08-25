using RunningTracker.Domain.Entities;
using System.Runtime.CompilerServices;

namespace RunningTracker.Application.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfile?> GetUserProfileServiceAsync(int id, [CallerMemberName] string methodName = "");
        Task AddUserProfileAsync(UserProfile profile, [CallerMemberName] string methodName = "");
        Task UpdateUserProfileAsync(UserProfile profile, [CallerMemberName] string methodName = "");
    }
}
