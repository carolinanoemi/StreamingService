using RatingService.Domain.Entities;
using RatingService.Infrastructure.DbResultModels;

namespace RatingService.Domain.Interfaces
{
    public interface IRatingRepository
    {
        // Bruger Composite Index (UserId, VideoId) - hurtigt opslag
        Task<Rating?> GetRatingAsync(int userId, int videoId);

        // Henter ALLE ratings for en bruger
        // (Udnytter at vores Unikke Composite Index fra Constraints starter med UserId, så det virker også til dette opslag)
        Task<IEnumerable<Rating>> GetUserRatingsAsync(int userId);

        // Kalder vores Stored Procedure: AddOrUpdateRating
        Task AddOrUpdateRatingAsync(int userId, int videoId, int score, string? comment);

        // Kalder vores Stored Procedure: GetRatingSummaryForVideo
        Task<RatingSummary> GetRatingSummaryAsync(int videoId);
    }
}