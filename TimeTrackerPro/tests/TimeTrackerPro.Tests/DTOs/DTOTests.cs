using TimeTrackerPro.Application.DTOs;
using TimeTrackerPro.Domain.Enums;

namespace TimeTrackerPro.Tests.DTOs
{
    public class DTOTests
    {
        [Fact]
        public void RegisterUserRequest_HasAllProperties()
        {
            // Arrange & Act
            var request = new RegisterUserRequest
            {
                Name = "João Silva",
                Email = "joao@example.com",
                Password = "Password123"
            };

            // Assert
            Assert.Equal("João Silva", request.Name);
            Assert.Equal("joao@example.com", request.Email);
            Assert.Equal("Password123", request.Password);
        }

        [Fact]
        public void LoginRequest_HasAllProperties()
        {
            // Arrange & Act
            var request = new LoginRequest
            {
                Email = "joao@example.com",
                Password = "Password123"
            };

            // Assert
            Assert.Equal("joao@example.com", request.Email);
            Assert.Equal("Password123", request.Password);
        }

        [Fact]
        public void UserResponse_HasAllProperties()
        {
            // Arrange & Act
            var userId = Guid.NewGuid();
            var response = new UserResponse
            {
                Id = userId,
                Name = "João Silva",
                Email = "joao@example.com",
                Token = "jwt_token_12345"
            };

            // Assert
            Assert.Equal(userId, response.Id);
            Assert.Equal("João Silva", response.Name);
            Assert.Equal("joao@example.com", response.Email);
            Assert.Equal("jwt_token_12345", response.Token);
        }

        [Fact]
        public void CreateActivityEntryRequest_CalculatesDuration()
        {
            // Arrange
            var request = new CreateActivityEntryRequest
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(11, 30, 0),
                Category = ActivityCategory.Development,
                Description = "Test activity"
            };

            // Act
            var duration = request.EndTime - request.StartTime;

            // Assert
            Assert.Equal(150, duration.TotalMinutes);
        }

        [Fact]
        public void UpdateActivityEntryRequest_HasAllProperties()
        {
            // Arrange & Act
            var request = new UpdateActivityEntryRequest
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(10, 0, 0),
                Category = ActivityCategory.Meeting,
                Description = "Updated activity"
            };

            // Assert
            Assert.NotEqual(default, request.Date);
            Assert.NotEqual(default, request.StartTime);
            Assert.NotEqual(default, request.EndTime);
            Assert.Equal(ActivityCategory.Meeting, request.Category);
            Assert.Equal("Updated activity", request.Description);
        }

        [Fact]
        public void ActivityEntryResponse_HasAllProperties()
        {
            // Arrange
            var activityId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            // Act
            var response = new ActivityEntryResponse
            {
                Id = activityId,
                UserId = userId,
                Date = DateOnly.FromDateTime(DateTime.Now),
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(11, 0, 0),
                DurationMinutes = 120,
                Category = ActivityCategory.Development,
                Description = "Test activity",
                CreatedAt = DateTime.UtcNow
            };

            // Assert
            Assert.Equal(activityId, response.Id);
            Assert.Equal(userId, response.UserId);
            Assert.Equal(120, response.DurationMinutes);
            Assert.Equal(ActivityCategory.Development, response.Category);
        }

        [Fact]
        public void TimeByCategoryReportResponse_HasAllProperties()
        {
            // Arrange & Act
            var response = new TimeByCategoryReportResponse
            {
                Category = "Development",
                TotalMinutes = 300,
                TotalHours = 5.0,
                ActivityCount = 3
            };

            // Assert
            Assert.Equal("Development", response.Category);
            Assert.Equal(300, response.TotalMinutes);
            Assert.Equal(5.0, response.TotalHours);
            Assert.Equal(3, response.ActivityCount);
        }

        [Fact]
        public void DailySummaryReportResponse_HasAllProperties()
        {
            // Arrange
            var date = DateOnly.FromDateTime(DateTime.Now);

            // Act
            var response = new DailySummaryReportResponse
            {
                Date = date,
                TotalMinutes = 480,
                TotalHours = 8.0,
                ActivityCount = 5
            };

            // Assert
            Assert.Equal(date, response.Date);
            Assert.Equal(480, response.TotalMinutes);
            Assert.Equal(8.0, response.TotalHours);
            Assert.Equal(5, response.ActivityCount);
        }

        [Fact]
        public void TimeByCategoryReportResponse_HoursCalculationIsCorrect()
        {
            // Arrange & Act
            var response = new TimeByCategoryReportResponse
            {
                Category = "Meeting",
                TotalMinutes = 90,
                TotalHours = Math.Round(90 / 60.0, 2),
                ActivityCount = 1
            };

            // Assert
            Assert.Equal(1.5, response.TotalHours);
        }

        [Fact]
        public void DailySummaryReportResponse_HoursCalculationIsCorrect()
        {
            // Arrange & Act
            var response = new DailySummaryReportResponse
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                TotalMinutes = 195,
                TotalHours = Math.Round(195 / 60.0, 2),
                ActivityCount = 2
            };

            // Assert
            Assert.Equal(3.25, response.TotalHours);
        }
    }
}
