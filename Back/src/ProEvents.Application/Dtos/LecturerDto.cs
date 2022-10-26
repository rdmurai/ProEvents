using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEvents.Application.Dtos
{
    public class LecturerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Resume { get; set; }
        public string IamgeUrl { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IEnumerable<SocialNetworkDto> SocialNetworks { get; set; }
        public IEnumerable<LecturerEventDto> LecturerEvents { get; set; }
    }
}