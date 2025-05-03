using Event_Manager_Final_Project_Advanced_Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event_Manager_Final_Project_Advanced_Web.Services
{
    public class DbUserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public DbUserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<User>> ReadAllAsync()
        {
            return await _db.Users
            .Include(a => a.EventParticipants)
            .ToListAsync();
        }

        public async Task<User?> ReadAsync(int id)
        {
            return await _db.Users
                .Include(a => a.EventParticipants)
                .ThenInclude(ba => ba.Event)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
