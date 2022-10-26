using System.Collections.Generic;

namespace ProEvents.Domain
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Resume { get; set; }
        public string IamgeUrl { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IEnumerable<SocialNetwork> SocialNetworks { get; set; }
        public IEnumerable<LecturerEvent> LecturerEvents { get; set; }



    }
}