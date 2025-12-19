using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Data
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
    }
}
