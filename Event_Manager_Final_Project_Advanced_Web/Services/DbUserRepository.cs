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

        public async Task CreateAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var existingUser = await _db.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.UserEmail = user.UserEmail;

                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUserByCredentialsAsync(string username, string password)
        {
            return await _db.Users
                .FirstOrDefaultAsync(user => user.Username == username && user.Password == password);
        }
    }
}
