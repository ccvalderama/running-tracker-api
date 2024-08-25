using RunningTracker.Application.Interfaces;
using RunningTracker.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace RunningTracker.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _repository;
        private readonly ILogger _logger;

        public UserProfileService(IUserProfileRepository userProfileRepository, ILogger<UserProfileService> logger) 
        { 
            _repository = userProfileRepository;
            _logger = logger;
        }

        public async Task AddUserProfileAsync(UserProfile profile, [CallerMemberName] string methodName = "")
        {
            try
            {
                await _repository.AddAsync(profile);
                _logger.LogInformation("[{MethodName}] User Profile created successfully.", methodName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{MethodName}] Unhandled exception:", methodName);
                throw;
            }
        }

        public async Task<UserProfile?> GetUserProfileServiceAsync(int id, [CallerMemberName] string methodName = "")
        {
            _logger.LogInformation("[{methodName}] Retreiving User Profile with Id {id}", methodName, id);
            try
            {
                var userProfile = await _repository.GetByIdAsync(id);
                if (userProfile == null) 
                {
                    _logger.LogWarning("[{methodName}] User Profile with {id} not found.", methodName, id);
                    return null;
                }
                return userProfile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{MethodName}] Unhandled exception:", methodName);
                throw;
            }
        }

        public async Task UpdateUserProfileAsync(UserProfile profile, [CallerMemberName] string methodName = "")
        {
            try
            {
                await _repository.UpdateAsync(profile);
                _logger.LogInformation("[{MethodName}] User Profile updated successfully.", methodName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{MethodName}] Unhandled exception:", methodName);
                throw;
            }
        }
    }
}
