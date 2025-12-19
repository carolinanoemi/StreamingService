using VideoEntity = VideoService.Domain.Entities.Video;

namespace VideoService.Domain.Interfaces
{
    public interface IVideoRepository
    {
        Task<VideoEntity?> GetByIdAsync(int id);
        Task<IReadOnlyList<VideoEntity>> GetAllAsync();
        Task AddAsync(VideoEntity video);
        Task SaveChangesAsync();
    }
}
