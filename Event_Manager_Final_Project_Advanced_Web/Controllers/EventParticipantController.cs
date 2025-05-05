using Microsoft.AspNetCore.Mvc;
using Event_Manager_Final_Project_Advanced_Web.Models.Entities;
using Event_Manager_Final_Project_Advanced_Web.Services;

public class EventParticipantController : Controller
{
    private readonly IUserRepository _userRepo;
    private readonly IEventParticipantRepository _participantRepo;

    public EventParticipantController(IUserRepository userRepo, IEventParticipantRepository participantRepo)
    {
        _userRepo = userRepo;
        _participantRepo = participantRepo;
    }

    // Source: https://stackify.com/viewbag/
    // Source: https://www.c-sharpcorner.com/UploadFile/54db21/what-is-viewbag/
    [HttpGet]
    public IActionResult Join(int id) // joins on event id maybe change
    {
        ViewBag.EventId = id;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Join(int eventId, string username, string password, DateTime arrivalTime)
    {
        var user = await _userRepo.GetUserByCredentialsAsync(username, password);
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid credentials.");
            ViewBag.EventId = eventId;
            return View();
        }

        var participant = new EventParticipant
        {
            EventId = eventId,
            Userint = user.Id,
            User = user,
            ArrivalTime = arrivalTime // Use user-input time instead of DateTime.Now
        };

        await _participantRepo.CreateAsync(participant);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Leave(int id)
    {
        ViewBag.EventId = id;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Leave(int eventId, string username, string password)
    {
        var user = await _userRepo.GetUserByCredentialsAsync(username, password);
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid credentials.");
            ViewBag.EventId = eventId;
            return View();
        }

        await _participantRepo.DeleteAsync(user.Id, eventId);
        return RedirectToAction("Index", "Home");
    }
}
