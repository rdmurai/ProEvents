namespace ProEvents.Application.Dtos
{
    public class LecturerEventDto
    {
        public int LecturerId { get; set; }
        public LecturerDto Lecturer { get; set; }
        public int EventId { get; set; }
        public EventDto Event { get; set; }
    }
}