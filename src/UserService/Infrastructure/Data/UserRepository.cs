using Microsoft.EntityFrameworkCore;
using UserService.Domain.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _db;

        public UserRepository(UsersDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            return await _db.Users
                .OrderBy(u => u.Id)
                .ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
