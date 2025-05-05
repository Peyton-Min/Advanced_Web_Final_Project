using Event_Manager_Final_Project_Advanced_Web.Models.Entities;

namespace Event_Manager_Final_Project_Advanced_Web.Services
{
    public interface IEventParticipantRepository
    {
        Task<List<EventParticipant>> GetParticipantsByEventIdAsync(int eventId);
        Task CreateAsync(EventParticipant participant);
        Task DeleteAsync(int userId, int eventId);
    }
}
