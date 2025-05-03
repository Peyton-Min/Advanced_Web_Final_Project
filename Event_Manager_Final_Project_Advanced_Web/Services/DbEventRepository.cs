using Event_Manager_Final_Project_Advanced_Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event_Manager_Final_Project_Advanced_Web.Services
{
    public class DbEventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _db;

        public DbEventRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<Event>> ReadAllAsync()
        {
            return await _db.Events
            .Include(a => a.EventParticipants)
            .ToListAsync();
        }

        public async Task<Event?> ReadAsync(int id)
        {
            return await _db.Events
                .Include(a => a.EventParticipants)
                .ThenInclude(ba => ba.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task CreateAsync(Event ev)
        {
            _db.Events.Add(ev);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Event ev)
        {
            _db.Events.Update(ev);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var ev = await _db.Events.FindAsync(id);
            if (ev != null)
            {
                _db.Events.Remove(ev);
                await _db.SaveChangesAsync();
            }
        }
    }
}
