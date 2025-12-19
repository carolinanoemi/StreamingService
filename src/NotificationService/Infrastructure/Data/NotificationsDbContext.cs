using Microsoft.EntityFrameworkCore;
using NotificationEntity = NotificationService.Domain.Entities.Notification;

namespace NotificationService.Infrastructure.Data
{
    public class NotificationsDbContext : DbContext
    {
        public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options)
            : base(options)
        {
        }

        public DbSet<NotificationEntity> Notifications => Set<NotificationEntity>();
    }
}
