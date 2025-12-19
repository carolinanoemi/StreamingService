namespace VideoService.Api.Dtos
{
    public class CreateVideoDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = null!;
        public int OwnerUserId { get; set; }
    }

    public class UpdateVideoDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class VideoResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = null!;
        public int OwnerUserId { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
