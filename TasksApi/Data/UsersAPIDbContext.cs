using Microsoft.EntityFrameworkCore;
using TasksApi.Models;

namespace TasksApi.Data
{
    public class UsersAPIDbContext : DbContext
    {
        public UsersAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}

  