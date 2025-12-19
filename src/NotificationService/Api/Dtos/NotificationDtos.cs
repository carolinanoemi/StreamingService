namespace NotificationService.Api.Dtos
{
    public class SendNotificationDto
    {
        public int UserId { get; set; }
        public string Message { get; set; } = null!;
    }

    public class NotificationResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = null!;
        public bool IsSent { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
