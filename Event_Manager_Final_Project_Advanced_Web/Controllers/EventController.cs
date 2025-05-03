using Event_Manager_Final_Project_Advanced_Web.Models.ViewModels;
using Event_Manager_Final_Project_Advanced_Web.Models.Entities;
using Event_Manager_Final_Project_Advanced_Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Event_Manager_Final_Project_Advanced_Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepo)
        {
            _eventRepository = eventRepo;
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
        public async Task<IActionResult> Create(Event ev)
        {
            if (!ModelState.IsValid)
            {
                return View(ev);
            }

            await _eventRepository.CreateAsync(ev);
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eventRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}