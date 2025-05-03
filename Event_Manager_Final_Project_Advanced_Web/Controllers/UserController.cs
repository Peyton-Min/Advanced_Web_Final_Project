using Event_Manager_Final_Project_Advanced_Web.Models.ViewModels;
using Event_Manager_Final_Project_Advanced_Web.Models.Entities;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            await _userRepository.CreateAsync(user);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _userRepository.ReadAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            await _userRepository.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepository.ReadAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // 
        //
        // Source: https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/examining-the-details-and-delete-methods
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
