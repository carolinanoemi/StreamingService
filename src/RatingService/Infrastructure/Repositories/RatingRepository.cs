using Microsoft.EntityFrameworkCore;
using RatingService.Domain.Entities;
using RatingService.Domain.Interfaces;
using RatingService.Infrastructure.Data;
using RatingService.Infrastructure.DbResultModels;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;


namespace RatingService.Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly RatingDbContext _context;
        private readonly IDistributedCache _cache;

        public RatingRepository(RatingDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // Udnytter Unique Constraint Composite Index)
        public async Task<Rating?> GetRatingAsync(int userId, int videoId)
        {
            return await _context.Ratings
                .FirstOrDefaultAsync(r => r.UserId == userId && r.VideoId == videoId);
        }

        // Udnytter igen at UserId står først i indexet)
        public async Task<IEnumerable<Rating>> GetUserRatingsAsync(int userId)
        {
            return await _context.Ratings
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        // Stored Procedure (UPSERT Logik)
        // Her bruger vi den SP, vi oprettede i EF migrations tidligere
        public async Task AddOrUpdateRatingAsync(int userId, int videoId, int score, string? comment)
        {
            // Vi sender parametre ned til databasen
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC AddOrUpdateRating @p0, @p1, @p2, @p3",
                userId, videoId, score, comment ?? (object)DBNull.Value);

            // Hvis rating ændrer sig, så skal summary-cache for den video væk
            await _cache.RemoveAsync($"rating:summary:{videoId}");
        }

        // Stored Procedure (Calculation)
        public async Task<RatingSummary> GetRatingSummaryAsync(int videoId)
        {
           

            // Cache-key pr video
            var cacheKey = $"rating:summary:{videoId}";

            // Prøv at hente fra redis cache først
            var cachedJson = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrWhiteSpace(cachedJson))
            {
                return JsonSerializer.Deserialize<RatingSummary>(cachedJson)!;
            }


            // Hvis intet i cachen, så hent fra stored procedure
            // Da dette returnerer tal og ikke en Entity, skal vi mappe det manuelt

            var result = new RatingSummary();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "EXEC GetRatingSummaryForVideo @p0";
                var param = command.CreateParameter();
                param.ParameterName = "@p0";
                param.Value = videoId;
                command.Parameters.Add(param);

                await _context.Database.OpenConnectionAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        // Læs kolonnerne fra SP'en
                        result.TotalRatings = reader.GetInt32(0); // Første kolonne
                        // Håndter null hvis ingen ratings findes
                        result.AverageScore = reader.IsDBNull(1) ? 0 : reader.GetDouble(1);
                    }
                }
            }

            //  Gem i cache i 120 sek (så vi ikke viser gamle tal i for lang tid)

            var json = JsonSerializer.Serialize(result);

            await _cache.SetStringAsync(
                cacheKey,
                json,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120)
                });

            return result;
        }
    }
}