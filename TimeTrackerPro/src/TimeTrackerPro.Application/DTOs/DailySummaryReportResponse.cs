namespace TimeTrackerPro.Application.DTOs
{
    public class DailySummaryReportResponse
    {
        public DateOnly Date { get; set; }
        public int TotalMinutes { get; set; }
        public double TotalHours { get; set; }
        public int ActivityCount { get; set; }
    }
}
