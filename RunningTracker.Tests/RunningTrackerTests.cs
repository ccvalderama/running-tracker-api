using Moq;
using RunningTracker.Application.Interfaces;
using RunningTracker.Application.Services;
using RunningTracker.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace RunningTracker.Tests
{
    public class RunningTrackerTests
    {
        private readonly Mock<IUserProfileRepository> _mockUserProfileRepo;
        private readonly Mock<ILogger<UserProfileService>> _mockUserProfileLogger;
        private readonly IUserProfileService _userProfileService;

        private readonly Mock<IRunningActivityRepository> _mockRunningActivityRepo;
        private readonly Mock<ILogger<RunningActivityService>> _mockRunningServiceLogger;
        private readonly IRunningActivityService _runningActivityService;

        public RunningTrackerTests()
        { 
            _mockUserProfileRepo = new Mock<IUserProfileRepository>();
            _mockUserProfileLogger = new Mock<ILogger<UserProfileService>>();
            _userProfileService = new UserProfileService(_mockUserProfileRepo.Object, _mockUserProfileLogger.Object);

            _mockRunningActivityRepo = new Mock<IRunningActivityRepository>();
            _mockRunningServiceLogger = new Mock<ILogger<RunningActivityService>>();
            _runningActivityService = new RunningActivityService(_mockRunningActivityRepo.Object, _mockRunningServiceLogger.Object);
        }

        [Fact]
        public async Task AddUserProfileAsync_ShouldCallRepository()
        {
            var profile = new UserProfile { Name = "Christian Valderama" };
            await _userProfileService.AddUserProfileAsync(profile);
            _mockUserProfileRepo.Verify(r => r.AddAsync(It.IsAny<UserProfile>()), Times.Once);
        }

        [Fact]
        public async Task AddRunningActivityAsync_ShouldCallRepository()
        {
            var activity = new RunningActivity { Location = "Stadium", UserProfileId = 1 };
            await _runningActivityService.AddRunningActivityAsync(activity);
            _mockRunningActivityRepo.Verify(r => r.AddAsync(It.IsAny<RunningActivity>()), Times.Once);
        }

        [Fact]
        public async Task RunningActivity_LogAndCalculationsTest()
        {
            // Arrange
            var userId = 1;
            var userProfile = new UserProfile
            {
                Id = userId,
                Name = "Christian Valderama",
                Height = 178, // cm
                Weight = 98, // kg
                BirthDate = new DateTime(1985, 12, 26)
            };

            var runningActivity = new RunningActivity
            {
                Location = "School Tracking Field",
                StartDateTime = DateTime.Now.AddHours(-1),
                EndDateTime = DateTime.Now,
                Distance = 5.0, // km
                UserProfileId = userId
            };

            userProfile.RunningActivities.Add(runningActivity);

            // Setup initial data
            _mockUserProfileRepo.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(userProfile);
            _mockRunningActivityRepo.Setup(r => r.AddAsync(runningActivity)).Returns(Task.CompletedTask);

            // Act
            await _runningActivityService.AddRunningActivityAsync(runningActivity);

            // Assert: check if the running activity was added to user profile
            Assert.Contains(runningActivity, userProfile.RunningActivities);

            // Assert: check if service calculated the duration and average pace
            Assert.Equal(1.00, Math.Round(runningActivity.Duration.TotalHours, 2));
            Assert.Equal(12.00, Math.Round(runningActivity.AveragePace, 2));
            Assert.Equal(30.93, Math.Round(userProfile.BMI, 2));
            Assert.Equal(39, userProfile.Age);

            // check if log messages was generated
            _mockRunningServiceLogger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v,t) => v.ToString().Contains("Running activity created successfully")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)),
                Times.Once);

            // check if the AddAsync method was called on running activity repository
            _mockRunningActivityRepo.Verify(r => r.AddAsync(runningActivity), Times.Once);
        }

    }

}
