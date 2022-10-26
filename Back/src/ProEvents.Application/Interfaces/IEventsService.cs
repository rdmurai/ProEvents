using System.Threading.Tasks;
using ProEvents.Application.Dtos;
using ProEvents.Domain;

namespace ProEvents.Application.Interfaces
{
    public interface IEventsService
    {
        Task<EventDto> AddEvent(EventDto model);
        Task<EventDto> UpdateEvent(int eventId, EventDto model);
        Task<bool> DeleteEvent(int eventId);
        Task<EventDto[]> GetAllEventsAsync(bool includeLecturers = false);
        Task<EventDto[]> GetAllEventsByThemeAsync(string theme, bool includeLecturers = false);
        Task<EventDto> GetEventByIdAsync(int eventId, bool includeLecturers = false);
    }
}