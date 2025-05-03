namespace Event_Manager_Final_Project_Advanced_Web.Models.Entities
{
    public class EventParticipant
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public Event? Event { get; set; }

        public int Userint { get; set; }
        public User? User { get; set; }

        public DateTime ArrivalTime { get; set; }
    }
}
