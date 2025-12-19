using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IUserService
    {
        // Create
        Task<User> CreateUserAsync(string userName, string email, string password);

        // Read
        Task<User?> GetUserByIdAsync(int userId);
        Task<IReadOnlyList<User>> GetAllUsersAsync();

        // Update
        Task<User?> UpdateUserAsync(int userId, string? newEmail, string? newPassword);

        // Delete (soft delete / deactivate)
        Task<bool> DeactivateUserAsync(int userId);
    }
}
