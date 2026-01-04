namespace WatchHistoryService.Api.Dtos;

public class WatchEventDto
{
    public int UserId { get; set; }
    public int VideoId { get; set; }
    public DateTime WatchedAt { get; set; }
    public int SecondsWatched { get; set; }
    public int PositionSeconds { get; set; }
    public string DeviceType { get; set; } = string.Empty;
}

