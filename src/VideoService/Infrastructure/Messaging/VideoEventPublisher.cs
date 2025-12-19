using EasyNetQ;
using StreamingService.Contracts;
using StreamingService.Contracts.VideoEvents;

namespace VideoService.Infrastructure.Messaging;

public class VideoEventPublisher
{
    private readonly IBus _bus;

    public VideoEventPublisher(IBus bus)
    {
        _bus = bus;
    }

    public Task PublishVideoCreatedAsync(VideoCreatedEvent evt)
    {

        // pub/sub event
        return _bus.PubSub.PublishAsync(evt);
    }
}
