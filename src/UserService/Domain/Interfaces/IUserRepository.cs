using System;
using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<IReadOnlyList<User>> GetAllAsync();
        Task AddAsync(User user);
        Task SaveChangesAsync();
    }

    // Vi holder det simpelt: én SaveChangesAsync, ingen delete-metode – vi bruger Deactivate() i domain i stedet.
}
