namespace NotificationService.Domain.Entities
{
    public class Notification
    {
        public int Id { get; private set; }

        public int UserId { get; private set; }
        public string Message { get; private set; } = string.Empty;

        public DateTime CreatedAt { get; private set; }
        public bool IsSent { get; private set; }

        private Notification() { }

        public Notification(int userId, string message)
        {
            if (userId <= 0)
                throw new ArgumentException("UserId must be positive", nameof(userId));

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message is required", nameof(message));

            UserId = userId;
            Message = message.Trim();
            CreatedAt = DateTime.UtcNow;
            IsSent = false;
        }

        public void MarkSent()
        {
            IsSent = true;
        }
    }
}
