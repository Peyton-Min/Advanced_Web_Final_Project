using System;

namespace Event_Manager_Final_Project_Advanced_Web.Services
{
    public class EventParticipantRepository
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
