using Microsoft.EntityFrameworkCore;
using VideoEntity = VideoService.Domain.Entities.Video;

namespace VideoService.Infrastructure.Data
{
    public class VideosDbContext : DbContext
    {
        public VideosDbContext(DbContextOptions<VideosDbContext> options)
            : base(options)
        {
        }

        public DbSet<VideoEntity> Videos => Set<VideoEntity>();
    }
}
