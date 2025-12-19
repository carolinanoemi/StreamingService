

namespace VideoService.Domain.Entities
{
    public class Video
    {
        public int Id { get; private set; }

        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string Url { get; private set; } = string.Empty;

        // FK til UserService – bare som int her
        public int OwnerUserId { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public bool IsPublished { get; private set; }

        private Video() { }

        public Video(string title, string description, string url, int ownerUserId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Titel er påkrævet", nameof(title));

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Url er påkrævet", nameof(url));

            if (ownerUserId <= 0)
                throw new ArgumentException("OwnerUserId skal være positiv", nameof(ownerUserId));

            Title = title.Trim();
            Description = description?.Trim() ?? string.Empty;
            Url = url.Trim();
            OwnerUserId = ownerUserId;

            CreatedAt = DateTime.UtcNow;
            IsPublished = true;
        }

        public void UpdateDetails(string? title, string? description)
        {
            if (!string.IsNullOrWhiteSpace(title))
                Title = title.Trim();

            if (description != null)
                Description = description.Trim();

            UpdatedAt = DateTime.UtcNow;
        }

        public void Unpublish()
        {
            IsPublished = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
