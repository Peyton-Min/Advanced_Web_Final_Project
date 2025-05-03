using Event_Manager_Final_Project_Advanced_Web.Models.ViewModels;
using Event_Manager_Final_Project_Advanced_Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Event_Manager_Final_Project_Advanced_Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepo)
        {
            _userRepository = userRepo;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.ReadAllAsync();

            var vm = users.Select(b => new UserDetailsVM
            {
                Id = b.Id,
                Username = b.Username,
                Password = b.Password,
                EventCount = b.EventParticipants?.Count ?? 0
            });
            return View(vm);
        }
    }
}
