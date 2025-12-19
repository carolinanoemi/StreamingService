namespace StreamingService.Contracts.VideoEvents;

public class VideoCreatedEvent
{
    public int VideoId { get; set; }
    public int OwnerUserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
}
