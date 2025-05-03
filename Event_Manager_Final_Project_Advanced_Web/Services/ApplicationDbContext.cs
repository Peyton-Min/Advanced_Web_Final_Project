using Event_Manager_Final_Project_Advanced_Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event_Manager_Final_Project_Advanced_Web.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Event> Events => Set<Event>();
    }
}
