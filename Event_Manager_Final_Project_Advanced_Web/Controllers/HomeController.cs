using Event_Manager_Final_Project_Advanced_Web.Models;
using Event_Manager_Final_Project_Advanced_Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Event_Manager_Final_Project_Advanced_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEventRepository _eventRepository;

        public HomeController(ILogger<HomeController> logger, IEventRepository eventRepository)
        {
            _logger = logger;
            _eventRepository = eventRepository;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventRepository.ReadAllAsync();
            var orderedEvents = events.OrderBy(ev => ev.EventTime);
            return View(orderedEvents);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}