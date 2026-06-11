using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimeTrackerPro.Application.DTOs;
using TimeTrackerPro.Application.Services;
using TimeTrackerPro.Domain.Enums;

namespace TimeTrackerPro.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(IReportService reportService, ILogger<ReportsController> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        [HttpGet("time-by-category")]
        public async Task<ActionResult<List<TimeByCategoryReportResponse>>> GetTimeByCategoryReport(
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate,
            [FromQuery] ActivityCategory? category = null)
        {
            try
            {
                // Validate dates
                if (startDate == default || endDate == default)
                    return BadRequest(new { message = "startDate and endDate are required" });

                var userId = GetUserIdFromClaims();
                var result = await _reportService.GetTimeByCategoryReportAsync(userId, startDate, endDate, category);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"Report validation error: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error generating report: {ex.Message}");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        [HttpGet("daily-summary")]
        public async Task<ActionResult<List<DailySummaryReportResponse>>> GetDailySummaryReport(
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate)
        {
            try
            {
                // Validate dates
                if (startDate == default || endDate == default)
                    return BadRequest(new { message = "startDate and endDate are required" });

                var userId = GetUserIdFromClaims();
                var result = await _reportService.GetDailySummaryReportAsync(userId, startDate, endDate);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"Report validation error: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error generating report: {ex.Message}");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        private Guid GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                _logger.LogWarning("Invalid user ID in token claims");
                throw new UnauthorizedAccessException("Invalid token");
            }
            return userId;
        }
    }
}
