namespace ProEvents.Domain
{
    public class LecturerEvent
    {
        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}