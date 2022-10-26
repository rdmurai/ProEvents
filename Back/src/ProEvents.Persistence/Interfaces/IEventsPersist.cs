using System.Threading.Tasks;
using ProEvents.Domain;

namespace ProEvents.Persistence.Interfaces
{
    public interface IEventsPersist
    {
        Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeLecturers = false);
        Task<Event[]> GetAllEventsAsync(bool includeLecturers = false);
        Task<Event> GetEventByIdAsync(int eventId, bool includeLecturers = false);
    }
}