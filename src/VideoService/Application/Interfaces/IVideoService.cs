using VideoEntity = VideoService.Domain.Entities.Video;

namespace VideoService.Application.Interfaces
{
    public interface IVideoService
    {
        Task<VideoEntity> CreateVideoAsync(string title, string description, string url, int ownerUserId);
        Task<VideoEntity?> GetVideoByIdAsync(int id);
        Task<IReadOnlyList<VideoEntity>> GetAllVideosAsync();
        Task<VideoEntity?> UpdateVideoAsync(int id, string? title, string? description);
        Task<bool> UnpublishVideoAsync(int id);
    }
}
