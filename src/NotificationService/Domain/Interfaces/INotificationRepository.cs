using NotificationEntity = NotificationService.Domain.Entities.Notification;

namespace NotificationService.Domain.Interfaces
{
    public interface INotificationRepository
    {
        Task<NotificationEntity?> GetByIdAsync(int id);
        Task<IReadOnlyList<NotificationEntity>> GetAllAsync();
        Task AddAsync(NotificationEntity notification);
        Task SaveChangesAsync();
    }
}
