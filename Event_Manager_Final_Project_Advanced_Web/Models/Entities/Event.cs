namespace Event_Manager_Final_Project_Advanced_Web.Models.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime EventTime { get; set; }
        public string? Location { get; set; }
        public int CreatedByUser { get; set; }
        public ICollection<EventParticipant>? EventParticipants { get; set; }
    }
}
