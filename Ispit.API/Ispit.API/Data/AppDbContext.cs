using Ispit.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ispit.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TodoList> todoLists { get; set; }
    }
}
