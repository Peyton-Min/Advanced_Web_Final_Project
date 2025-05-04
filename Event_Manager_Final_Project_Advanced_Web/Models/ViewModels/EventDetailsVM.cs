namespace Event_Manager_Final_Project_Advanced_Web.Models.ViewModels
{
    public class EventDetailsVM
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime EventTime { get; set; }
        public string? Location { get; set; }
        public int CreatedByUser { get; set; }
        public string? CreatorUsername { get; set; }
        public int UserCount {  get; set; }

    }
}
