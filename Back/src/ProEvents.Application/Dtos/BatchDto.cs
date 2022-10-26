using System;

namespace ProEvents.Application.Dtos
{
    public class BatchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public int Quantity { get; set; }

        public int EventId { get; set; }
        public EventDto Event { get; set; }
    }
}