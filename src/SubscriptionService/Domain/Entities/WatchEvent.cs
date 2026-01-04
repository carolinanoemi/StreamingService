using System;
using System.Collections.Generic;

namespace SubscriptionService.Domain.Entities;

public partial class WatchEvent
{
    public int WatchEventId { get; set; }

    public int UserId { get; set; }

    public int VideoId { get; set; }

    public DateTime WatchedAt { get; set; }

    public int SecondsWatched { get; set; }

    public int PositionSeconds { get; set; }

    public string DeviceType { get; set; } = null!;
}
