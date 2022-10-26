using AutoMapper;
using ProEvents.Application.Dtos;
using ProEvents.Domain;

namespace ProEvents.Application.Helpers
{
    public class ProEventsProfile : Profile
    {
        public ProEventsProfile()
        {
            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>();
            CreateMap<Batch, BatchDto>().ReverseMap();
            CreateMap<SocialNetwork, SocialNetworkDto>().ReverseMap();
            CreateMap<Lecturer, LecturerDto>().ReverseMap();

        }

    }
}