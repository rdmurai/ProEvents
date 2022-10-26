using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEvents.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Local { get; set; }

        public DateTime? EventDate { get; set; }

        [MaxLength(50)]
        public string Theme { get; set; }
        public int QtyPeople { get; set; }
        public string ImageURL { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        public IEnumerable<Batch> Batchs { get; set; }

        public IEnumerable<SocialNetwork> SocialNetworks { get; set; }
        public IEnumerable<LecturerEvent> LecturerEvents { get; set; }


    }
}