using Microsoft.EntityFrameworkCore;
using VideoService.Application.Interfaces;
using VideoService.Application.Services;
using VideoService.Domain.Interfaces;
using VideoService.Infrastructure.Data;
using EasyNetQ;
using VideoService.Infrastructure.Messaging;


namespace VideoService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // RabbitMQ Bus
        var rabbitConn = builder.Configuration["RabbitMq:Connection"];
        builder.Services.AddSingleton(RabbitHutch.CreateBus(rabbitConn));

        // Controllers + Swagger
        builder.Services.AddScoped<VideoEventPublisher>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<VideosDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("VideosDb")));

        builder.Services.AddScoped<IVideoRepository, VideoRepository>();
        builder.Services.AddScoped<IVideoService, VideoService.Application.Services.VideoService>();

        var app = builder.Build();

       /* // Auto-create DB + tables (DEV ONLY)
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<VideosDbContext>();
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
