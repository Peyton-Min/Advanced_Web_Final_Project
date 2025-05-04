using Event_Manager_Final_Project_Advanced_Web.Models.Entities;

namespace Event_Manager_Final_Project_Advanced_Web.Services
{
    public class Initializer
    {
        private readonly ApplicationDbContext _db;

        public Initializer(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task SeedDatabaseAsync()
        {
            _db.Database.EnsureCreated();

            // If there are no users then seed some.
            if (_db.Users.Any() == false)
            {
                var user = new List<User>
            {
               new() { Username = "Fred Flinstone", UserEmail = "FredFlinstone@RockMail.com", Password = "EatFruityPebbles" },
               new() { Username = "Tom Cat", UserEmail = "MouseCatcher@yahoo.com", Password = "Mousecapades" },
               new() { Username = "Dale Gribble", UserEmail = "NotDaleGribble@gmail.com", Password = "SquirrelTactics!" }
            };

                await _db.Users.AddRangeAsync(user);
                await _db.SaveChangesAsync();
            }

            // If there are no events then seed some.
            if (_db.Events.Any() == false)
            {
                var events = new List<Event>
            {
               new() { Title = "Backyard Dino BBQ", Description = "We're grilling some Pterodactyls on Barney's new grill. Come join us!", EventTime = new DateTime(2026,5,6, 18, 30, 0), Location = "Barney's Backyard", CreatedByUser = 1 },
               new() { Title = "Mouse Catching Party", Description = "I need help catching Jerry. He usually comes out of his house around 3 pm.", EventTime = new DateTime(2024,4,3, 15, 0, 0), Location = "Tom's House", CreatedByUser = 2 },
               new() { Title = "Monthly Hunting Club Meeting", Description = "This month's meeting is at my house after the club building got repossessed. Make sure your safety is on before coming to the meeting. We don't want any repeat incidents. Nancy is making cookies.", EventTime = new DateTime(2025,5,4, 0, 22, 0), Location = "82 Rainey Street", CreatedByUser = 3 }
            };

                await _db.Events.AddRangeAsync(events);
                await _db.SaveChangesAsync();
            }
        }
    }
}
