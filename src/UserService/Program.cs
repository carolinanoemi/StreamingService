using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Application.Services;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Data;

namespace UserService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Controllers + Swagger
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // DbContext – connectionstring fra configuration (appsettings / env vars)
        builder.Services.AddDbContext<UsersDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("UsersDb")));

        // Dependency Injection
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService.Application.Services.UserService>();

        var app = builder.Build();

        // Auto-create DB + tables (DEV ONLY)
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
            db.Database.EnsureCreated();
        }


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            // Redirect root (/) to Swagger UI
            app.MapGet("/", () => Results.Redirect("/swagger"));
        }


        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
