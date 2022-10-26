using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Persistence.Context;
using ProEvents.Persistence.Interfaces;

namespace ProEvents.Persistence
{
    public class EventsPersist : IEventsPersist
    {
        private readonly ProEventsContext _context;

        public EventsPersist(ProEventsContext context)
        {
            _context = context;
        }

        public async Task<Event[]> GetAllEventsAsync(bool includeLecturers = false)
        {
            IQueryable<Event> query = _context.Events.AsNoTracking().Include(e => e.Batchs).Include(e => e.SocialNetworks);

            if (includeLecturers)
            {
                query = query.Include(e => e.LecturerEvents).ThenInclude(le => le.Lecturer);

            }

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Event[]> GetAllEventsByThemeAsync(string theme, bool includeLecturers = false)
        {
            IQueryable<Event> query = _context.Events.AsNoTracking().Include(e => e.Batchs).Include(e => e.SocialNetworks);

            if (includeLecturers)
            {
                query = query.Include(e => e.LecturerEvents).ThenInclude(le => le.Lecturer);
            }

            query = query.OrderBy(e => e.Id)
                        .Where(e => e.Theme.ToLower().Contains(theme.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Event> GetEventByIdAsync(int eventId, bool includeLecturers = false)
        {
            IQueryable<Event> query = _context.Events.AsNoTracking().Include(e => e.Batchs).Include(e => e.SocialNetworks);

            if (includeLecturers)
            {
                query = query.Include(e => e.LecturerEvents).ThenInclude(le => le.Lecturer);
            }

            query = query.OrderBy(e => e.Id)
                        .Where(e => e.Id == eventId);

            return await query.FirstOrDefaultAsync();
        }

    }
}