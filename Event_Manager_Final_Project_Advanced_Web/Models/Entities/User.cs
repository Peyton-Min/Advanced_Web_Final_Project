namespace Event_Manager_Final_Project_Advanced_Web.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? UserEmail { get; set; }
        public string? Password { get; set; }
        public ICollection<EventParticipant>? EventParticipants { get; set; }
    }
}
