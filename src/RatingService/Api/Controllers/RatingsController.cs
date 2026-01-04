using Microsoft.AspNetCore.Mvc;
using RatingService.Api.Dtos;
using RatingService.Domain.Interfaces;

namespace RatingService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingsController : ControllerBase
{
    private readonly IRatingRepository _repository;

    public RatingsController(IRatingRepository repository)
    {
        _repository = repository;
    }

    // POST: Opretter eller opdaterer rating
    // Kalder din repository metode, som kører "EXEC AddOrUpdateRating..."
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRatingDto dto)
    {
        await _repository.AddOrUpdateRatingAsync(dto.UserId, dto.VideoId, dto.Score, dto.Comment);
        return Ok(new { message = "Rating registered successfully" });
    }

    // GET: Henter statistik for en video
    // Kalder din repository metode, som kører "EXEC GetRatingSummaryForVideo..." med manuel mapping
    [HttpGet("video/{videoId:int}/stats")]
    public async Task<ActionResult<VideoRatingStatsDto>> GetStatsForVideo(int videoId)
    {
        // 1. Hent data fra repository (Din DTO fra den manuelle mapping)
        var summary = await _repository.GetRatingSummaryAsync(videoId);

        // 2. Map det over til API'ets DTO, så JSON-svaret ser rigtigt ud
        return Ok(new VideoRatingStatsDto
        {
            VideoId = videoId,
            AvgScore = summary.AverageScore,   // Kommer fra Reo reader.GetDouble(1)
            RatingCount = summary.TotalRatings // Kommer fra Repo reader.GetInt32(0)
        });
    }

    // GET: Henter historik for en bruger
    // Kalder din repository metode, som bruger LINQ og Index-optimering
    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<IEnumerable<RatingResponseDto>>> GetUserHistory(int userId)
    {
        var ratings = await _repository.GetUserRatingsAsync(userId);

        var response = ratings.Select(r => new RatingResponseDto
        {
            RatingId = r.RatingId,
            UserId = r.UserId,
            VideoId = r.VideoId,
            Score = r.Score,
            Comment = r.Comment,
            CreatedAt = r.CreatedAt
        });

        return Ok(response);
    }
}