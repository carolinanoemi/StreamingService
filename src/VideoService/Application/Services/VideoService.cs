using VideoService.Application.Interfaces;
using VideoService.Domain.Interfaces;
using VideoEntity = VideoService.Domain.Entities.Video;

namespace VideoService.Application.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;

        public VideoService(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public async Task<VideoEntity> CreateVideoAsync(string title, string description, string url, int ownerUserId)
        {
            var video = new VideoEntity(title, description, url, ownerUserId);

            await _videoRepository.AddAsync(video);
            await _videoRepository.SaveChangesAsync();

            return video;
        }

        public async Task<VideoEntity?> GetVideoByIdAsync(int id)
        {
            return await _videoRepository.GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<VideoEntity>> GetAllVideosAsync()
        {
            return await _videoRepository.GetAllAsync();
        }

        public async Task<VideoEntity?> UpdateVideoAsync(int id, string? title, string? description)
        {
            var video = await _videoRepository.GetByIdAsync(id);
            if (video == null)
                return null;

            video.UpdateDetails(title, description);
            await _videoRepository.SaveChangesAsync();

            return video;
        }

        public async Task<bool> UnpublishVideoAsync(int id)
        {
            var video = await _videoRepository.GetByIdAsync(id);
            if (video == null)
                return false;

            video.Unpublish();
            await _videoRepository.SaveChangesAsync();

            return true;
        }
    }
}

