using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Interfaces;
using NotificationEntity = NotificationService.Domain.Entities.Notification;

namespace NotificationService.Infrastructure.Data
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationsDbContext _db;

        public NotificationRepository(NotificationsDbContext db)
        {
            _db = db;
        }

        public async Task<NotificationEntity?> GetByIdAsync(int id)
        {
            return await _db.Notifications.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IReadOnlyList<NotificationEntity>> GetAllAsync()
        {
            return await _db.Notifications
                .OrderByDescending(n => n.Id)
                .ToListAsync();
        }

        public async Task AddAsync(NotificationEntity notification)
        {
            await _db.Notifications.AddAsync(notification);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
