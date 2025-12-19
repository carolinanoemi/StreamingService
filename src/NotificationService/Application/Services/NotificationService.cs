using NotificationService.Application.Interfaces;
using NotificationService.Domain.Interfaces;
using NotificationEntity = NotificationService.Domain.Entities.Notification;

namespace NotificationService.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;

        public NotificationService(INotificationRepository repo)
        {
            _repo = repo;
        }

        public async Task<NotificationEntity> SendAsync(int userId, string message)
        {
            var notification = new NotificationEntity(userId, message);

            // "Send" = demo: skriv til log + markér sendt
            Console.WriteLine($"[NOTIFICATION] -> UserId={userId} | {message}");
            notification.MarkSent();

            await _repo.AddAsync(notification);
            await _repo.SaveChangesAsync();

            return notification;
        }

        public async Task<NotificationEntity?> GetByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task<IReadOnlyList<NotificationEntity>> GetAllAsync()
            => await _repo.GetAllAsync();
    }
}
