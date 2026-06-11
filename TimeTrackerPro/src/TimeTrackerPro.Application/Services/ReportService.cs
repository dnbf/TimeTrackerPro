using Microsoft.Extensions.Logging;
using TimeTrackerPro.Application.DTOs;
using TimeTrackerPro.Domain.Enums;
using TimeTrackerPro.Infrastructure.Persistence;

namespace TimeTrackerPro.Application.Services
{
    public interface IReportService
    {
        Task<List<TimeByCategoryReportResponse>> GetTimeByCategoryReportAsync(
            Guid userId,
            DateOnly startDate,
            DateOnly endDate,
            ActivityCategory? category = null);

        Task<List<DailySummaryReportResponse>> GetDailySummaryReportAsync(
            Guid userId,
            DateOnly startDate,
            DateOnly endDate);
    }

    public class ReportService : IReportService
    {
        private readonly TimeTrackerDbContext _context;
        private readonly ILogger<ReportService> _logger;

        public ReportService(TimeTrackerDbContext context, ILogger<ReportService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<TimeByCategoryReportResponse>> GetTimeByCategoryReportAsync(
            Guid userId,
            DateOnly startDate,
            DateOnly endDate,
            ActivityCategory? category = null)
        {
            // Validate dates
            if (startDate > endDate)
                throw new ArgumentException("Start date must be before or equal to end date");

            var query = _context.ActivityEntries
                .Where(a => a.UserId == userId && a.Date >= startDate && a.Date <= endDate);

            // Apply category filter if specified
            if (category.HasValue)
                query = query.Where(a => a.Category == category.Value);

            var activities = await Task.Run(() => query.ToList());

            // Group by category and calculate totals
            var report = activities
                .GroupBy(a => a.Category)
                .Select(g => new TimeByCategoryReportResponse
                {
                    Category = g.Key.ToString(),
                    TotalMinutes = g.Sum(a => a.DurationMinutes),
                    TotalHours = Math.Round(g.Sum(a => a.DurationMinutes) / 60.0, 2),
                    ActivityCount = g.Count()
                })
                .OrderBy(r => r.Category)
                .ToList();

            _logger.LogInformation($"Time by category report generated for user {userId} from {startDate} to {endDate}");

            return report;
        }

        public async Task<List<DailySummaryReportResponse>> GetDailySummaryReportAsync(
            Guid userId,
            DateOnly startDate,
            DateOnly endDate)
        {
            // Validate dates
            if (startDate > endDate)
                throw new ArgumentException("Start date must be before or equal to end date");

            var query = _context.ActivityEntries
                .Where(a => a.UserId == userId && a.Date >= startDate && a.Date <= endDate);

            var activities = await Task.Run(() => query.ToList());

            // Group by date and calculate totals
            var report = activities
                .GroupBy(a => a.Date)
                .Select(g => new DailySummaryReportResponse
                {
                    Date = g.Key,
                    TotalMinutes = g.Sum(a => a.DurationMinutes),
                    TotalHours = Math.Round(g.Sum(a => a.DurationMinutes) / 60.0, 2),
                    ActivityCount = g.Count()
                })
                .OrderBy(r => r.Date)
                .ToList();

            _logger.LogInformation($"Daily summary report generated for user {userId} from {startDate} to {endDate}");

            return report;
        }
    }
}
