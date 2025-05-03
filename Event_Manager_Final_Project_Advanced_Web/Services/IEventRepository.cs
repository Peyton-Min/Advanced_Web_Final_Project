using Event_Manager_Final_Project_Advanced_Web.Models.Entities;

namespace Event_Manager_Final_Project_Advanced_Web.Services
{
    public interface IEventRepository
    {
        Task<ICollection<Event>> ReadAllAsync();
        Task<Event?> ReadAsync(int id);
        Task CreateAsync(Event ev);
        Task UpdateAsync(Event ev);
        Task DeleteAsync(int id);
    }
}
