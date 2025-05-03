using Event_Manager_Final_Project_Advanced_Web.Models.Entities;

namespace Event_Manager_Final_Project_Advanced_Web.Services
{
    public interface IEventRepository
    {
        Task<ICollection<Event>> ReadAllAsync();
        Task<Event?> ReadAsync(int id);
    }
}
