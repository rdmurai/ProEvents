using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEvents.Application.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }
        [Required]
        public string Local { get; set; }
        [Required]
        public string EventDate { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]

        public string Theme { get; set; }

        [Range(1, 120000)]
        public int QtyPeople { get; set; }
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Image must be gif, jpeg, bmp or png")]
        public string ImageURL { get; set; }
        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<BatchDto> Batchs { get; set; }

        public IEnumerable<SocialNetworkDto> SocialNetworks { get; set; }
        public IEnumerable<LecturerEventDto> LecturerEvents { get; set; }


    }
}