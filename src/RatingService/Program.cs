using Microsoft.EntityFrameworkCore;
using RatingService.Infrastructure.Data;
using RatingService.Infrastructure;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // add Controllers

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // DbContext + connectionstring
        var cs = builder.Configuration.GetConnectionString("RatingDb");

        if (string.IsNullOrWhiteSpace(cs))
            throw new InvalidOperationException("Missing connection string: ConnectionStrings:RatingDb");

        builder.Services.AddDbContext<RatingDbContext>(opt => opt.UseSqlServer(cs));

        // redis cache
        var redisConn = builder.Configuration["Redis:ConnectionString"];

        if (string.IsNullOrWhiteSpace(redisConn))
            throw new InvalidOperationException("Missing Redis connection string: Redis:ConnectionString");

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConn;
        });


        var app = builder.Build();


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

