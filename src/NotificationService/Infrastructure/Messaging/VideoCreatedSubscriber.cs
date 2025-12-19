using StreamingService.Contracts.VideoEvents;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace NotificationService.Infrastructure.Messaging;

public class VideoCreatedSubscriber
{
    private readonly IBus _bus;
    private readonly IServiceScopeFactory _scopeFactory;

    public VideoCreatedSubscriber(IBus bus, IServiceScopeFactory scopeFactory)
    {
        _bus = bus;
        // Vi har brug for en "fabrik" til at lave midlertidige scopes,
        // fordi vi ikke må injicere en database direkte i en service, der lever for evigt (Singleton).
        _scopeFactory = scopeFactory;
    }

    public async Task StartAsync()
    {
        Console.WriteLine("[SUBSCRIBER] Starting VideoCreated subscriber...");

        // --- RETRY POLICY (Prøv igen hvis det fejler) ---
        // Hvis RabbitMQ ikke er klar endnu, venter vi og prøver igen.
        // Vi bruger "Lineær Backoff", så vi venter længere og længere (3s, 6s, 9s...)
        var retryPolicy = Polly.Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: 10,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Min(3 * attempt, 15)),
                onRetry: (ex, delay, attempt, _) =>
                {
                    Console.WriteLine($"[SUBSCRIBER] Attempt {attempt}/10 failed. Retrying in {delay.TotalSeconds}s...");
                });

        // --- FALLBACK POLICY (Hvad gør vi, hvis det går helt galt?) ---
        // Hvis RabbitMQ stadig er nede efter 10 forsøg, giver vi op.
        // I stedet for at crashe hele programmet, logger vi bare en fejl.
        // Det sikrer, at servicen stadig kører, selvom den ikke kan modtage beskeder lige nu.
        var fallbackPolicy = Polly.Policy
            .Handle<Exception>()
            .FallbackAsync(async ct =>
            {
                Console.WriteLine("[SUBSCRIBER] Fallback: Could not subscribe. Service running without messaging.");
                await Task.CompletedTask;
            });

        // Pakker de to strategier sammen: Først Retry, og hvis alt fejler -> Fallback.
        var policyWrap = Policy.WrapAsync(fallbackPolicy, retryPolicy);

        await policyWrap.ExecuteAsync(async () =>
        {
            Console.WriteLine("[SUBSCRIBER] SubscribeAsync called.");

            // Opretter abonnement på "VideoCreatedEvent" via EasyNetQ
            await _bus.PubSub.SubscribeAsync<VideoCreatedEvent>(
                subscriptionId: "notificationservice.video-created",
                onMessage: async evt =>
                {
                    Console.WriteLine($"[SUBSCRIBER] Received Event: {evt.Title}");

                    // --- VIGTIGT: SCIPING (Midlertidig arbejds-zone) ---
                    // Subscriberen kører hele tiden (Singleton), men vores Database lever kun kort (Scoped).
                    // Derfor laver vi manuelt et "Scope" her. Det svarer til at åbne en
                    // midlertidig forbindelse, gøre arbejdet færdigt, og lukke den igen med det samme.
                    using var scope = _scopeFactory.CreateScope();

                    // Nu kan vi hente NotificationService (som bruger databasen) inde i vores sikre scope.
                    var notificationService = scope.ServiceProvider.GetRequiredService<Application.Interfaces.INotificationService>();

                    await notificationService.SendAsync(
                        evt.OwnerUserId,
                        $"Ny video uploaded: '{evt.Title}' (VideoId={evt.VideoId})"
                    );
                });

            Console.WriteLine("[SUBSCRIBER] Subscription registered OK");
        });
    }
}