using Microsoft.EntityFrameworkCore;
using WatchHistoryService.Infrastructure;
using WatchHistoryService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WatchHistoryService.Infrastructure.Data;



public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // add Controllers

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddDbContext<WatchHistoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WatchHistoryDb")));

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

