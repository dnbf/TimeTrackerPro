using TimeTrackerPro.Domain.Enums;

namespace TimeTrackerPro.Application.DTOs
{
    public class UpdateActivityEntryRequest
    {
        public DateOnly Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public ActivityCategory Category { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
