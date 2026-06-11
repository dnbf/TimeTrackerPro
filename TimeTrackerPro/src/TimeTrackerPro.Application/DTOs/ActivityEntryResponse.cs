using TimeTrackerPro.Domain.Enums;

namespace TimeTrackerPro.Application.DTOs
{
    public class ActivityEntryResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateOnly Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int DurationMinutes { get; set; }
        public ActivityCategory Category { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
