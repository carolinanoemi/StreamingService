namespace UserService.Domain.Entities
{
    public class User
    {
        // PK – EF Core bruger denne
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        // OBS: kun til demo, normalt skal det være hashed password
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public bool IsActive { get; private set; }

        // EF Core kræver parameterløs constructor (må gerne være private)
        private User() { }

        public User(string userName, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Brugernavn er påkrævet", nameof(userName));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email er påkrævet", nameof(email));

            if (!email.Contains("@"))
                throw new ArgumentException("Email er ikke gyldig", nameof(email));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password er påkrævet", nameof(password));

            UserName = userName.Trim();
            Email = email.Trim();
            Password = password; // demo-only
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public void ChangeEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Email er påkrævet", nameof(newEmail));

            if (!newEmail.Contains("@"))
                throw new ArgumentException("Email er ikke gyldig", nameof(newEmail));

            Email = newEmail.Trim();
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangePassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException("Password er påkrævet", nameof(newPassword));

            Password = newPassword;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        //her undgår vi “primitive obsession” ved at have regler i konstruktør/metoder, i stedet for at alle frit kan sætte Email = "lol".
    }

}
