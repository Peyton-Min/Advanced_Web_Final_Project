using Event_Manager_Final_Project_Advanced_Web.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Event_Manager_Final_Project_Advanced_Web.Services
{
    public class DbEventParticipantRepository : IEventParticipantRepository
    {
        private readonly ApplicationDbContext _db;

        public DbEventParticipantRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(EventParticipant participant)
        {
            _db.EventParticipants.Add(participant);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userId, int eventId)
        {
            var record = await _db.EventParticipants
                .FirstOrDefaultAsync(p => p.Userint == userId && p.EventId == eventId);

            if (record != null)
            {
                _db.EventParticipants.Remove(record);
                await _db.SaveChangesAsync();
            }
        }
        public async Task<List<EventParticipant>> GetParticipantsByEventIdAsync(int eventId)
        {
            return await _db.EventParticipants
                .Include(p => p.User)
                .Where(p => p.EventId == eventId)
                .ToListAsync();
        }
    }
}