
using System.Threading.Tasks;
using ProEvents.Persistence.Context;
using ProEvents.Persistence.Interfaces;

namespace ProEvents.Persistence
{
    public class ProEventsPersist : IProEventsPersist
    {
        private readonly ProEventsContext _context;

        public ProEventsPersist(ProEventsContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

    }
}