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
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityEntryService _activityService;
        private readonly ILogger<ActivitiesController> _logger;

        public ActivitiesController(IActivityEntryService activityService, ILogger<ActivitiesController> logger)
        {
            _activityService = activityService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ActivityEntryResponse>> CreateActivity([FromBody] CreateActivityEntryRequest request)
        {
            try
            {
                var userId = GetUserIdFromClaims();
                var result = await _activityService.CreateAsync(userId, request);
                return CreatedAtAction(nameof(GetActivityById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"Activity creation validation error: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error creating activity: {ex.Message}");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ActivityEntryResponse>> UpdateActivity(Guid id, [FromBody] UpdateActivityEntryRequest request)
        {
            try
            {
                var userId = GetUserIdFromClaims();
                var result = await _activityService.UpdateAsync(userId, id, request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"Activity update validation error: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning($"Activity update error: {ex.Message}");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error updating activity: {ex.Message}");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            try
            {
                var userId = GetUserIdFromClaims();
                await _activityService.DeleteAsync(userId, id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning($"Activity delete error: {ex.Message}");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting activity: {ex.Message}");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityEntryResponse>> GetActivityById(Guid id)
        {
            try
            {
                var userId = GetUserIdFromClaims();
                var result = await _activityService.GetByIdAsync(userId, id);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning($"Activity retrieval error: {ex.Message}");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error retrieving activity: {ex.Message}");
                return StatusCode(500, new { message = "An unexpected error occurred" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ActivityEntryResponse>>> GetActivities(
            [FromQuery] DateOnly? startDate = null,
            [FromQuery] DateOnly? endDate = null,
            [FromQuery] ActivityCategory? category = null)
        {
            try
            {
                var userId = GetUserIdFromClaims();
                var result = await _activityService.GetActivitiesAsync(userId, startDate, endDate, category);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error retrieving activities: {ex.Message}");
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
