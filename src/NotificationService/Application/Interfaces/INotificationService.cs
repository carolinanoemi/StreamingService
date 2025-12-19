using NotificationEntity = NotificationService.Domain.Entities.Notification;

namespace NotificationService.Application.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationEntity> SendAsync(int userId, string message);
        Task<NotificationEntity?> GetByIdAsync(int id);
        Task<IReadOnlyList<NotificationEntity>> GetAllAsync();
    }
}
