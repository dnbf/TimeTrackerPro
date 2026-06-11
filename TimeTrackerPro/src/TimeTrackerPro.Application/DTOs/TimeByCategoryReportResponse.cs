namespace TimeTrackerPro.Application.DTOs
{
    public class TimeByCategoryReportResponse
    {
        public string Category { get; set; } = string.Empty;
        public int TotalMinutes { get; set; }
        public double TotalHours { get; set; }
        public int ActivityCount { get; set; }
    }
}
