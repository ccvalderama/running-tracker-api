using Microsoft.AspNetCore.Mvc;
using RunningTracker.Application.Interfaces;
using RunningTracker.Domain.Entities;
using RunningTracker.Domain.ViewModel;

namespace RunningTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunningActivityController : ControllerBase
    {
        private readonly IRunningActivityService _runningActivityService;
        public RunningActivityController(IRunningActivityService runningActivityService)
        { 
            _runningActivityService = runningActivityService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRunningActvity(int id)
        {
            var activity = await _runningActivityService.GetRunningActivityAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRunningActivity([FromBody] RunningActivityViewModel activityViewModel)
        {
            var activity = new RunningActivity
            {
                Location = activityViewModel.Location,
                UserProfileId = activityViewModel.UserProfileId,
                StartDateTime = activityViewModel.StartDateTime,
                EndDateTime = activityViewModel.EndDateTime,
                Distance = activityViewModel.Distance,
            };
            await _runningActivityService.AddRunningActivityAsync(activity);
            return CreatedAtAction(nameof(GetRunningActvity), activity);
        }
    }
}
