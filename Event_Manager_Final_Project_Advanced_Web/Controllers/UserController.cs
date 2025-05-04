using Event_Manager_Final_Project_Advanced_Web.Models.ViewModels;
using Event_Manager_Final_Project_Advanced_Web.Models.Entities;
using Event_Manager_Final_Project_Advanced_Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Event_Manager_Final_Project_Advanced_Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;

        public UserController(IUserRepository userRepo, IEventRepository eventRepo)
        {
            _userRepository = userRepo;
            _eventRepository = eventRepo;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.ReadAllAsync();

            var vm = users.Select(b => new UserDetailsVM
            {
                Id = b.Id,
                Username = b.Username,
                UserEmail = b.UserEmail,
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
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            return View("Update");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(string username, string password, string newEmail, string newUsername)
        {
            var user = await _userRepository.GetUserByCredentialsAsync(username, password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View("Update");
            }

            user.UserEmail = newEmail;
            user.Username = newUsername;
            await _userRepository.UpdateAsync(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View(); 
        }


        // Source: https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/examining-the-details-and-delete-methods
        [HttpPost]
        public async Task<IActionResult> DeleteAsyncConfirm(string username, string password)
        {
            var user = await _userRepository.GetUserByCredentialsAsync(username, password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View();
            }

            var allEvents = await _eventRepository.ReadAllAsync();
            var userEvents = allEvents.Where(ev => ev.CreatedByUser == user.Id);
            foreach (var ev in userEvents)
            {
                await _eventRepository.DeleteAsync(ev.Id);
            }

            await _userRepository.DeleteAsync(user.Id);
            return RedirectToAction("Index", "Home");
        }
    }
}
