using Event_Manager_Final_Project_Advanced_Web.Models.ViewModels;
using Event_Manager_Final_Project_Advanced_Web.Models.Entities;
using Event_Manager_Final_Project_Advanced_Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Event_Manager_Final_Project_Advanced_Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;

        public EventController(IEventRepository eventRepo, IUserRepository userRepo)
        {
            _eventRepository = eventRepo;
            _userRepository = userRepo;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventRepository.ReadAllAsync();

            var vm = events.Select(b => new EventDetailsVM
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                EventTime = b.EventTime,
                Location = b.Location,
                CreatedByUser = b.CreatedByUser,
                UserCount = b.EventParticipants?.Count ?? 0
            });
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event ev, string username, string password)
        {
            if (!ModelState.IsValid)
            {
                return View(ev);
            }

            var user = await _userRepository.GetUserByCredentialsAsync(username, password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(ev);
            }

            ev.CreatedByUser = user.Id;
            await _eventRepository.CreateAsync(ev);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var ev = await _eventRepository.ReadAsync(id);
            if (ev == null)
            {
                return NotFound();
            }

            var user = await _userRepository.ReadAsync(ev.CreatedByUser);
            var vm = new EventDetailsVM
            {
                Id = ev.Id,
                Title = ev.Title,
                Description = ev.Description,
                EventTime = ev.EventTime,
                Location = ev.Location,
                CreatedByUser = ev.CreatedByUser,
                UserCount = ev.EventParticipants?.Count ?? 0,
                CreatorUsername = user?.Username ?? "Unknown"
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var ev = await _eventRepository.ReadAsync(id);

            if (ev == null)
            {
                return NotFound();
            }

            return View(ev);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Event ev)
        {
            if (!ModelState.IsValid)
            {
                return View(ev);
            }

            await _eventRepository.UpdateAsync(ev);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ev = await _eventRepository.ReadAsync(id);
            if (ev == null)
            {
                return NotFound();
            }

            return View(ev);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, string username, string password)
        {
            var user = await _userRepository.GetUserByCredentialsAsync(username, password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials.");
                var ev = await _eventRepository.ReadAsync(id);
                return View("Delete", ev);
            }

            var evToDelete = await _eventRepository.ReadAsync(id);
            if (evToDelete == null)
            {
                return NotFound();
            }

            if (evToDelete.CreatedByUser != user.Id)
            {
                ModelState.AddModelError("", "You are not authorized to delete this event.");
                return View("Delete", evToDelete);
            }

            await _eventRepository.DeleteAsync(id);
            return RedirectToAction("Index", "Home");
        }
    }
}