using Event_Manager_Final_Project_Advanced_Web.Models.ViewModels;
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
    }
}