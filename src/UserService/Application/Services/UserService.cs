using UserService.Application.Interfaces;
using UserService.Domain.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(string userName, string email, string password)
        {
            // Domænemodellen validerer selv i konstruktøren
            var user = new User(userName, email, password);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<IReadOnlyList<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User?> UpdateUserAsync(int userId, string? newEmail, string? newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return null;

            if (!string.IsNullOrWhiteSpace(newEmail))
            {
                user.ChangeEmail(newEmail);
            }

            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                user.ChangePassword(newPassword);
            }

            await _userRepository.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeactivateUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return false;

            user.Deactivate();
            await _userRepository.SaveChangesAsync();

            return true;
        }
    }

    
}
