using Microsoft.Extensions.Logging;
using RunningTracker.Application.Interfaces;
using RunningTracker.Domain.Entities;
using System.Runtime.CompilerServices;

namespace RunningTracker.Application.Services
{
    public class RunningActivityService : IRunningActivityService
    {
        private readonly IRunningActivityRepository _repository;
        private readonly ILogger _logger;

        public RunningActivityService(IRunningActivityRepository runningActivityRepository, ILogger<RunningActivityService> logger)
        {
            _repository = runningActivityRepository;
            _logger = logger;
        }

        public async Task AddRunningActivityAsync(RunningActivity runningActivity, [CallerMemberName] string methodName = "")
        {
            try
            {
                await _repository.AddAsync(runningActivity);
                _logger.LogInformation("[{MethodName}] Running activity created successfully.", methodName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{MethodName}] Unhandled exception:", methodName);
                throw;
            }
        }

        public async Task<RunningActivity?> GetRunningActivityAsync(int id, [CallerMemberName] string methodName = "")
        {
            _logger.LogInformation("[{methodName}] Retreiving Running Activity with Id {id}", methodName, id);
            try
            {
                var runningActivity = await _repository.GetByIdAsync(id);
                if (runningActivity == null)
                {
                    _logger.LogWarning("[{methodName}] Running Activity with {id} not found.", methodName, id);
                    return null;
                }
                return runningActivity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{MethodName}] Unhandled exception:", methodName);
                throw;
            }
        }

        public async Task UpdateRunningActivityAsync(RunningActivity runningActivity, [CallerMemberName] string methodName = "")
        {
            try
            {
                await _repository.UpdateAsync(runningActivity);
                _logger.LogInformation("[{MethodName}] Running activity updated successfully.", methodName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{MethodName}] Unhandled exception:", methodName);
                throw;
            }
        }
    }
}
