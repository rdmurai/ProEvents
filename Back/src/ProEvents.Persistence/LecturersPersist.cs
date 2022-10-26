using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;
using ProEvents.Persistence.Context;
using ProEvents.Persistence.Interfaces;

namespace ProEvents.Persistence
{
    public class LecturersPersist : ILecturersPersist
    {
        private readonly ProEventsContext _context;

        public LecturersPersist(ProEventsContext context)
        {
            _context = context;
        }

        public async Task<Lecturer[]> GetAllLecturersByNameAsync(string name, bool includeEvents = false)
        {
            IQueryable<Lecturer> query = _context.Lecturers.AsNoTracking().Include(e => e.SocialNetworks);

            if (includeEvents)
            {
                query = query.Include(e => e.LecturerEvents).ThenInclude(le => le.Event);
            }

            query = query.OrderBy(e => e.Id)
                        .Where(e => e.Name.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Lecturer[]> GetAllLecturerssAsync(bool includeEvents = false)
        {
            IQueryable<Lecturer> query = _context.Lecturers.AsNoTracking().Include(l => l.SocialNetworks);

            if (includeEvents)
            {
                query = query.Include(l => l.LecturerEvents).ThenInclude(e => e.Event);
            }

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Lecturer> GetLecturerByIdAsync(int lecturerId, bool includeEvents = false)
        {
            IQueryable<Lecturer> query = _context.Lecturers.AsNoTracking().Include(e => e.SocialNetworks);

            if (includeEvents)
            {
                query = query.Include(l => l.LecturerEvents).ThenInclude(e => e.Event);
            }

            query = query.OrderBy(l => l.Id)
                        .Where(l => l.Id == lecturerId);

            return await query.FirstOrDefaultAsync();
        }

    }
}