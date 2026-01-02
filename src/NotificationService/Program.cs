using EasyNetQ;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Interfaces;
using NotificationService.Infrastructure.Data;
using NotificationService.Infrastructure.Messaging;


namespace NotificationService;

public class Program
{
    public static async Task Main(string[] args)
    {
       

        var builder = WebApplication.CreateBuilder(args);

        // RabbitMQ Bus
        var rabbitConn = builder.Configuration["RabbitMq:Connection"];
        Console.WriteLine($"[RABBIT] RabbitMq:Connection = '{rabbitConn}'");
        builder.Services.AddSingleton(RabbitHutch.CreateBus(rabbitConn));

        
        builder.Services.AddSingleton<VideoCreatedSubscriber>();


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<NotificationsDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("NotificationsDb")));

        builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
        builder.Services.AddScoped<INotificationService, NotificationService.Application.Services.NotificationService>();

        var app = builder.Build();

        // Start subscriber

        Console.WriteLine("[APP] NotificationService starting...");

        
        using (var scope = app.Services.CreateScope())
        {
            var sub = scope.ServiceProvider.GetRequiredService<VideoCreatedSubscriber>();
            await sub.StartAsync();
        }

        Console.WriteLine("[APP] Subscriber started.");



        
        // Auto-create DB + tables (DEV ONLY)
        /*
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<NotificationsDbContext>();
            db.Database.EnsureCreated();
        } */


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapGet("/", () => Results.Redirect("/swagger"));
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
