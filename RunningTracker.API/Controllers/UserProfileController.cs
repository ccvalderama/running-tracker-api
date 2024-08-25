using Microsoft.AspNetCore.Mvc;
using RunningTracker.Application.Interfaces;
using RunningTracker.Domain.Entities;

namespace RunningTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfile(int id)
        {
            var profile = await _userProfileService.GetUserProfileServiceAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfile profile)
        {
            await _userProfileService.AddUserProfileAsync(profile);
            return CreatedAtAction(nameof(GetUserProfile), new { id = profile.Id });
        }
    }
}
