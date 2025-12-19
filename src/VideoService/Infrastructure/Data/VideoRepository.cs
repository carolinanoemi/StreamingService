using Microsoft.EntityFrameworkCore;
using VideoService.Domain.Interfaces;
using VideoEntity = VideoService.Domain.Entities.Video;

namespace VideoService.Infrastructure.Data
{
    public class VideoRepository : IVideoRepository
    {
        private readonly VideosDbContext _db;

        public VideoRepository(VideosDbContext db)
        {
            _db = db;
        }

        public async Task<VideoEntity?> GetByIdAsync(int id)
        {
            return await _db.Videos.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IReadOnlyList<VideoEntity>> GetAllAsync()
        {
            return await _db.Videos
                .OrderBy(v => v.Id)
                .ToListAsync();
        }

        public async Task AddAsync(VideoEntity video)
        {
            await _db.Videos.AddAsync(video);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
