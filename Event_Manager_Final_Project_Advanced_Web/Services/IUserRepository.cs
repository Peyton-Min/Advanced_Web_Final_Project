using Event_Manager_Final_Project_Advanced_Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event_Manager_Final_Project_Advanced_Web.Services
{
    public interface IUserRepository 
    {
        Task<ICollection<User>> ReadAllAsync();
        Task<User?> ReadAsync(int id);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<User?> GetUserByCredentialsAsync(string username, string password);
    }
}
